using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using XPaint.Cheats.Core;

namespace XPaint.Cheats;

public static class ImageProcessor
{
    // Square shape is the only shape used — matches original forza-painter behaviour.
    // Fill in the real in-game shape ID once found via RE.
    public const int SquareShapeId = 0; // TODO: replace with real shape ID

    // Max canvas dimension so total pixels never exceed the 3000-layer limit.
    private const int MaxDim = 54; // 54*54 = 2916 < 3000

    public static List<VinylLayer> ConvertImageToVinyls(string imagePath, int maxLayers = 3000)
    {
        using var src = new Bitmap(imagePath);

        int targetW = src.Width;
        int targetH = src.Height;

        // Scale down proportionally so neither dimension exceeds MaxDim
        // and total pixel count stays within maxLayers.
        if (targetW > MaxDim || targetH > MaxDim || targetW * targetH > maxLayers)
        {
            float scale = Math.Min((float)MaxDim / targetW, (float)MaxDim / targetH);
            // Also clamp by total pixel budget
            float scaleBudget = MathF.Sqrt((float)maxLayers / (targetW * targetH));
            scale = Math.Min(scale, scaleBudget);
            targetW = Math.Max(1, (int)(targetW * scale));
            targetH = Math.Max(1, (int)(targetH * scale));
        }

        using var resized = new Bitmap(targetW, targetH, PixelFormat.Format32bppArgb);
        using (var g = Graphics.FromImage(resized))
        {
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
            g.DrawImage(src, 0, 0, targetW, targetH);
        }

        float stepX = 2f / targetW;
        float stepY = 2f / targetH;

        var vinyls = new List<VinylLayer>(targetW * targetH);

        for (int y = 0; y < targetH; y++)
        {
            for (int x = 0; x < targetW; x++)
            {
                if (vinyls.Count >= maxLayers) break;

                var pixel = resized.GetPixel(x, y);
                if (pixel.A < 128) continue;

                vinyls.Add(new VinylLayer
                {
                    ShapeId  = SquareShapeId,
                    PositionX = -1f + (x + 0.5f) * stepX,
                    PositionY = -1f + (y + 0.5f) * stepY,
                    ScaleX   = stepX,
                    ScaleY   = stepY,
                    Rotation = 0f,
                    Color    = (uint)pixel.ToArgb(),
                    Layer    = vinyls.Count,
                });
            }
            if (vinyls.Count >= maxLayers) break;
        }

        return vinyls;
    }
}
