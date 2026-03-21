using XPaint.Resources;
using Memory;
using static XPaint.Resources.Memory;

namespace XPaint.Cheats.Core;

public class PainterCheats : CheatsUtilities, ICheatsBase, IRevertBase
{
    // ── RE stubs ─────────────────────────────────────────────────────────────
    // Replace every TODO value with the real offset/signature found via RE.

    // AOB that lands on (or near) the livery editor base pointer instruction.
    private const string LiveryBaseSig = "TODO_REPLACE_WITH_REAL_SIG";

    // Byte offsets from the resolved livery base pointer.
    private const int VinylCountOffset = 0x000; // TODO
    private const int VinylArrayOffset = 0x000; // TODO

    // Size of one vinyl layer entry in the game's array (stride).
    private const int VinylStride = 0x20; // TODO — adjust once struct is known

    // Per-field offsets within a single vinyl entry.
    private const int OffShapeId   = 0x00; // int32
    private const int OffPositionX = 0x04; // float
    private const int OffPositionY = 0x08; // float
    private const int OffScaleX    = 0x0C; // float
    private const int OffScaleY    = 0x10; // float
    private const int OffRotation  = 0x14; // float
    private const int OffColor     = 0x18; // uint32 ARGB
    private const int OffLayer     = 0x1C; // int32

    private const int MaxVinyls = 3000;
    // ─────────────────────────────────────────────────────────────────────────

    private UIntPtr _liveryBaseAddress;
    private UIntPtr _vinylCountAddress;
    private UIntPtr _vinylArrayAddress;

    public bool WereScansSuccessful;

    public async Task<bool> InitializePainter()
    {
        WereScansSuccessful = false;
        _liveryBaseAddress = 0;
        _vinylCountAddress = 0;
        _vinylArrayAddress = 0;

        _liveryBaseAddress = await SmartAobScan(LiveryBaseSig);
        if (_liveryBaseAddress == 0)
        {
            ShowError("PainterCheats", "Livery base address not found");
            return false;
        }

        _vinylCountAddress = _liveryBaseAddress + (nuint)VinylCountOffset;
        _vinylArrayAddress = _liveryBaseAddress + (nuint)VinylArrayOffset;

        WereScansSuccessful = true;
        return true;
    }

    public int GetCurrentVinylCount()
    {
        if (_vinylCountAddress == 0) return 0;
        return GetInstance().ReadMemory<int>(_vinylCountAddress);
    }

    public void AddVinylLayer(VinylLayer layer)
    {
        if (!WereScansSuccessful) return;

        var mem = GetInstance();
        int count = GetCurrentVinylCount();
        if (count >= MaxVinyls) return;

        WriteVinylAt(mem, _vinylArrayAddress + (nuint)(count * VinylStride), layer);
        mem.WriteMemory(_vinylCountAddress, count + 1);
    }

    /// <summary>Writes all layers in one pass, then updates the count once.</summary>
    public void AddVinylLayers(IReadOnlyList<VinylLayer> layers)
    {
        if (!WereScansSuccessful || layers.Count == 0) return;

        var mem = GetInstance();
        int startCount = GetCurrentVinylCount();
        int remaining = MaxVinyls - startCount;
        int toWrite = Math.Min(layers.Count, remaining);

        for (int i = 0; i < toWrite; i++)
        {
            WriteVinylAt(mem, _vinylArrayAddress + (nuint)((startCount + i) * VinylStride), layers[i]);
        }

        mem.WriteMemory(_vinylCountAddress, startCount + toWrite);
    }

    public void ClearAllVinyls()
    {
        if (_vinylCountAddress == 0) return;
        GetInstance().WriteMemory(_vinylCountAddress, 0);
    }

    private static void WriteVinylAt(Mem mem, nuint addr, VinylLayer v)
    {
        mem.WriteMemory(addr + (nuint)OffShapeId,   v.ShapeId);
        mem.WriteMemory(addr + (nuint)OffPositionX, v.PositionX);
        mem.WriteMemory(addr + (nuint)OffPositionY, v.PositionY);
        mem.WriteMemory(addr + (nuint)OffScaleX,    v.ScaleX);
        mem.WriteMemory(addr + (nuint)OffScaleY,    v.ScaleY);
        mem.WriteMemory(addr + (nuint)OffRotation,  v.Rotation);
        mem.WriteMemory(addr + (nuint)OffColor,     v.Color);
        mem.WriteMemory(addr + (nuint)OffLayer,     v.Layer);
    }

    public void Cleanup()
    {
        WereScansSuccessful = false;
        _liveryBaseAddress = 0;
        _vinylCountAddress = 0;
        _vinylArrayAddress = 0;
    }

    public void Reset() => Cleanup();

    public void Revert() { }

    public void Continue() { }
}

