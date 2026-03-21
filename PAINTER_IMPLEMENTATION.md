# Forza Painter Implementation

## Status

| Area | Status |
|---|---|
| UI (Painter.xaml) | ✅ Done |
| MVVM (PainterViewModel.cs) | ✅ Done |
| Image processor (ImageProcessor.cs) | ✅ Done |
| Memory layer (PainterCheats.cs) | ✅ Done (stubs for RE values) |
| Reverse engineering | ⚠️ Blocked — needs Cheat Engine work |

---

## What's left: Reverse Engineering

Everything is wired up. The only remaining work is filling in the `TODO` constants in `PainterCheats.cs`.

### Values needed

Open `Cheats/PainterCheats.cs` and replace every `TODO` comment:

```csharp
private const string LiveryBaseSig = "TODO_REPLACE_WITH_REAL_SIG";
private const int VinylCountOffset = 0x000; // TODO
private const int VinylArrayOffset = 0x000; // TODO
private const int VinylStride      = 0x20;  // TODO — size of one vinyl entry
```

Also update `ImageProcessor.SquareShapeId` in `Cheats/ImageProcessor.cs`:

```csharp
public const int SquareShapeId = 0; // TODO: replace with real shape ID
```

### How to find them (Cheat Engine)

1. Attach Cheat Engine to `ForzaHorizon5.exe`
2. Enter the livery editor
3. Scan for the vinyl count (unknown int → add layer → changed → remove → changed → repeat)
4. Once found: right-click → "Find what writes to this address"
5. The instruction that writes the count will be inside a function — create an AOB from it
6. Use ReClass.NET on the base pointer to map the full struct layout
7. Confirm the square shape ID by adding a square vinyl and reading the shapeId field

### Shape note

Only the **square** shape is used (one square per pixel). This matches the original forza-painter approach and stays within the 3000-layer budget. No other shapes are needed.

---

## Architecture

```
ImageProcessor.ConvertImageToVinyls()
    → List<VinylLayer>  (square shape, pixel colour, mapped position/scale)

PainterCheats.AddVinylLayers()
    → writes all layers in one pass, updates count once

PainterViewModel.ImportImage
    → runs both on background thread, reports progress to UI
```
