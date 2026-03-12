<<<<<<< C:/Users/GAMING/Documents/GitHub/MA_FH5Trainer/MA_FH5Trainer/MA_FH5Trainer/Cheats/PhotomodeCheats.cs
<<<<<<< C:/Users/GAMING/Documents/GitHub/MA_FH5Trainer/MA_FH5Trainer/MA_FH5Trainer/Cheats/PhotomodeCheats.cs
<<<<<<< C:/Users/GAMING/Documents/GitHub/MA_FH5Trainer/MA_FH5Trainer/MA_FH5Trainer/Cheats/PhotomodeCheats.cs
<<<<<<< C:/Users/GAMING/Documents/GitHub/MA_FH5Trainer/MA_FH5Trainer/MA_FH5Trainer/Cheats/PhotomodeCheats.cs
<<<<<<< C:/Users/GAMING/Documents/GitHub/MA_FH5Trainer/MA_FH5Trainer/MA_FH5Trainer/Cheats/PhotomodeCheats.cs
<<<<<<< C:/Users/GAMING/Documents/GitHub/MA_FH5Trainer/MA_FH5Trainer/MA_FH5Trainer/Cheats/PhotomodeCheats.cs
<<<<<<< C:/Users/GAMING/Documents/GitHub/MA_FH5Trainer/MA_FH5Trainer/MA_FH5Trainer/Cheats/PhotomodeCheats.cs
<<<<<<< C:/Users/GAMING/Documents/GitHub/MA_FH5Trainer/MA_FH5Trainer/MA_FH5Trainer/Cheats/PhotomodeCheats.cs
<<<<<<< C:/Users/GAMING/Documents/GitHub/MA_FH5Trainer/MA_FH5Trainer/MA_FH5Trainer/Cheats/PhotomodeCheats.cs
<<<<<<< C:/Users/GAMING/Documents/GitHub/MA_FH5Trainer/MA_FH5Trainer/MA_FH5Trainer/Cheats/PhotomodeCheats.cs
<<<<<<< C:/Users/GAMING/Documents/GitHub/MA_FH5Trainer/MA_FH5Trainer/MA_FH5Trainer/Cheats/PhotomodeCheats.cs
﻿using static MA_FH5Trainer.Resources.Cheats;
using static MA_FH5Trainer.Resources.Memory;
=======
﻿using static XPaint.Resources.Cheats;
using static XPaint.Resources.Memory;
>>>>>>> C:/Users/GAMING/.windsurf/worktrees/MA_FH5Trainer/MA_FH5Trainer-4284e8b3/MA_FH5Trainer/MA_FH5Trainer/Cheats/PhotomodeCheats.cs
=======
﻿using static XPaint.Resources.Cheats;
using static XPaint.Resources.Memory;
>>>>>>> C:/Users/GAMING/.windsurf/worktrees/MA_FH5Trainer/MA_FH5Trainer-4284e8b3/MA_FH5Trainer/MA_FH5Trainer/Cheats/PhotomodeCheats.cs
=======
﻿using static XPaint.Resources.Cheats;
using static XPaint.Resources.Memory;
>>>>>>> C:/Users/GAMING/.windsurf/worktrees/MA_FH5Trainer/MA_FH5Trainer-4284e8b3/MA_FH5Trainer/MA_FH5Trainer/Cheats/PhotomodeCheats.cs
=======
﻿using static XPaint.Resources.Cheats;
using static XPaint.Resources.Memory;
>>>>>>> C:/Users/GAMING/.windsurf/worktrees/MA_FH5Trainer/MA_FH5Trainer-4284e8b3/MA_FH5Trainer/MA_FH5Trainer/Cheats/PhotomodeCheats.cs
=======
﻿using static XPaint.Resources.Cheats;
using static XPaint.Resources.Memory;
>>>>>>> C:/Users/GAMING/.windsurf/worktrees/MA_FH5Trainer/MA_FH5Trainer-4284e8b3/MA_FH5Trainer/MA_FH5Trainer/Cheats/PhotomodeCheats.cs
=======
﻿using static XPaint.Resources.Cheats;
using static XPaint.Resources.Memory;
>>>>>>> C:/Users/GAMING/.windsurf/worktrees/MA_FH5Trainer/MA_FH5Trainer-4284e8b3/MA_FH5Trainer/MA_FH5Trainer/Cheats/PhotomodeCheats.cs
=======
﻿using static XPaint.Resources.Cheats;
using static XPaint.Resources.Memory;
using static XPaint.Resources.StringCipher;
>>>>>>> C:/Users/GAMING/.windsurf/worktrees/MA_FH5Trainer/MA_FH5Trainer-4284e8b3/MA_FH5Trainer/MA_FH5Trainer/Cheats/PhotomodeCheats.cs

namespace XPaint.Cheats.ForzaHorizon5;
=======
=======
>>>>>>> C:/Users/GAMING/.windsurf/worktrees/MA_FH5Trainer/MA_FH5Trainer-90665ca8/MA_FH5Trainer/MA_FH5Trainer/Cheats/PhotomodeCheats.cs
using static XPaint.Resources.Cheats;
using static XPaint.Resources.Memory;
using static XPaint.Resources.StringCipher;

namespace XPaint.Cheats.Core;
<<<<<<< C:/Users/GAMING/Documents/GitHub/MA_FH5Trainer/MA_FH5Trainer/MA_FH5Trainer/Cheats/PhotomodeCheats.cs
>>>>>>> C:/Users/GAMING/.windsurf/worktrees/MA_FH5Trainer/MA_FH5Trainer-90665ca8/MA_FH5Trainer/MA_FH5Trainer/Cheats/PhotomodeCheats.cs
=======
>>>>>>> C:/Users/GAMING/.windsurf/worktrees/MA_FH5Trainer/MA_FH5Trainer-90665ca8/MA_FH5Trainer/MA_FH5Trainer/Cheats/PhotomodeCheats.cs
=======
using static XPaint.Resources.Cheats;
using static XPaint.Resources.Memory;
using static XPaint.Resources.StringCipher;

namespace XPaint.Cheats.Core;
>>>>>>> C:/Users/GAMING/.windsurf/worktrees/MA_FH5Trainer/MA_FH5Trainer-4284e8b3/MA_FH5Trainer/MA_FH5Trainer/Cheats/PhotomodeCheats.cs
=======
using static XPaint.Resources.Cheats;
using static XPaint.Resources.Memory;
using static XPaint.Resources.StringCipher;

namespace XPaint.Cheats.Core;
>>>>>>> C:/Users/GAMING/.windsurf/worktrees/MA_FH5Trainer/MA_FH5Trainer-4284e8b3/MA_FH5Trainer/MA_FH5Trainer/Cheats/PhotomodeCheats.cs

public class PhotomodeCheats : CheatsUtilities, ICheatsBase, IRevertBase
{
    private UIntPtr _noClipAddress;
    public UIntPtr NoClipDetourAddress;
    private UIntPtr _noHeightLimitAddress;
    public UIntPtr NoHeightLimitDetourAddress;
    private UIntPtr _increasedZoomAddress;
    public UIntPtr IncreasedZoomDetourAddress;
    public UIntPtr MainModifiersAddress;
    public UIntPtr SpeedAddress;
    public bool WasModifiersScanSuccessful;

    public async Task CheatNoClip()
    {
        _noClipAddress = 0;
        NoClipDetourAddress = 0;

        var sig = D("kwPRZLEO60mYG8VkqG3jSZMO0R64DutJmBvOfLcO4FGHA7N8tw7rSZgbzny3DuBRhwPE");
        _noClipAddress = await SmartAobScan(sig);

        if (_noClipAddress > 0)
        {
            _noClipAddress -= 93;
            if (GetClass<Bypass>().CallAddress <= 3)
            {
                await GetClass<Bypass>().DisableCrcChecks();
            }
            
            if (GetClass<Bypass>().CallAddress <= 3) return;

            var asm = new byte[]
            {
                0x80, 0x3D, 0x12, 0x00, 0x00, 0x00, 0x01, 0x75, 0x06, 0x31, 0xC0, 0x89, 0x44, 0x24, 0x50, 0x0F, 0x11,
                0x44, 0x24, 0x54
            };
            NoClipDetourAddress = GetInstance().CreateDetour(_noClipAddress, asm, 5);
            return;
        }
        
        ShowError("Photo mode no clip", sig);
    }

    public async Task CheatNoHeightLimits()
    {
        _noHeightLimitAddress = 0;
        NoHeightLimitDetourAddress = 0;

        var sig = D("4QnRbM4O60mYG858tw7rSZgbx2qoHpJJmBvOfLho9Fvh");
        _noHeightLimitAddress = await SmartAobScan(sig);

        if (_noHeightLimitAddress > 0)
        {
            if (GetClass<Bypass>().CallAddress <= 3)
            {
                await GetClass<Bypass>().DisableCrcChecks();
            }
            
            if (GetClass<Bypass>().CallAddress <= 3) return;

            var asm = new byte[]
            {
                0x80, 0x3D, 0x1D, 0x00, 0x00, 0x00, 0x01, 0x75, 0x0E, 0x68, 0xA5, 0xD4, 0x68, 0x53, 0xF3, 0x0F, 0x10,
                0x14, 0x24, 0x48, 0x83, 0xC4, 0x08, 0xF2, 0x0F, 0x10, 0x9E, 0xC0, 0x05, 0x00, 0x00
            };
            NoHeightLimitDetourAddress = GetInstance().CreateDetour(_noHeightLimitAddress, asm, 8);
            return;
        }
        
        ShowError("Photo mode no height limits", sig);
    }
    
    public async Task CheatIncreasedZoom()
    {
        _increasedZoomAddress = 0;
        IncreasedZoomDetourAddress = 0;

        var sig = D("4X3RZbgO60mYG858tw6SWocLt3y3DutJmBvOfLcO60nhCNFszg7rSZgbzny3DutJmBvBGqgc7EmYG7dvqB6SSZgbznzOHfRZ4RvOfLcOklqHC7d8tw7rSZgbt2+oHpJJmBvOfLcO60nhCNFszg==");
        _increasedZoomAddress = await SmartAobScan(sig);

        if (_increasedZoomAddress > 0)
        {
            if (GetClass<Bypass>().CallAddress <= 3)
            {
                await GetClass<Bypass>().DisableCrcChecks();
            }
            
            if (GetClass<Bypass>().CallAddress <= 3) return;

            var asm = new byte[]
            {
                0x80, 0x3D, 0x1A, 0x00, 0x00, 0x00, 0x01, 0x75, 0x0D, 0x6A, 0x00, 0xF3, 0x0F, 0x10, 0x04, 0x24, 0x48,
                0x83, 0xC4, 0x08, 0xEB, 0x06, 0xFF, 0x90, 0x78, 0x03, 0x00, 0x00
            };
            IncreasedZoomDetourAddress = GetInstance().CreateDetour(_increasedZoomAddress, asm, 6);
            return;
        }
        
        ShowError("Photo mode increased zoom", sig);
    }
    
    public async Task CheatModifiers()
    {
        MainModifiersAddress = 0;
        SpeedAddress = 0;

        var successCount = 0;
        
        var mainModifiersSig = D("lHnRbcwO60mYG858tw7kL4cPtA==");
        MainModifiersAddress = await SmartAobScan(mainModifiersSig);
        if (MainModifiersAddress <= 0)
        {
            ShowError("Photo mode main modifiers", mainModifiersSig);
            goto skipScans;
        }

        var mainRelative = GetInstance().ReadMemory<int>(MainModifiersAddress + 2);
        MainModifiersAddress = (nuint)((nint)MainModifiersAddress + mainRelative + 6);
        ++successCount;

        var speedSig = D("4QjRbM4O60mYG858tw7rSZgbt2+oHpJJmBvOfLcO60mYG858vBb0UeUbzny3DutJmBvOfLcO5C+HCsF8tw7rSZgbzny3DuQvhwnJ");
        SpeedAddress = await SmartAobScan(speedSig);
        if (SpeedAddress <= 0)
        {
            ShowError("Photo mode speed modifiers", speedSig);
            goto skipScans;
        }

        var speedRelative = GetInstance().ReadMemory<int>(SpeedAddress + 4);
        SpeedAddress = (nuint)((nint)SpeedAddress + speedRelative + 8);
        ++successCount;
        
        skipScans:
        WasModifiersScanSuccessful = successCount == 2;
    }
    
    public void Cleanup()
    {
        var mem = GetInstance();

        if (_noClipAddress > 0)
        {
            mem.WriteArrayMemory(_noClipAddress, new byte[] { 0x0F, 0x11, 0x44, 0x24, 0x54 });
            Free(NoClipDetourAddress);
        }

        if (_noHeightLimitAddress > 0)
        {
            mem.WriteArrayMemory(_noHeightLimitAddress, new byte[] { 0xF2, 0x0F, 0x10, 0x9E, 0xC0, 0x05, 0x00, 0x00 });
            Free(NoHeightLimitDetourAddress);
        }
        
        if (_increasedZoomAddress <= 0) return;
        mem.WriteArrayMemory(_increasedZoomAddress, new byte[] { 0xFF, 0x90, 0x78, 0x03, 0x00, 0x00 });
        Free(IncreasedZoomDetourAddress);
    }

    public void Reset()
    {
        WasModifiersScanSuccessful = false;
        var fields = typeof(PhotomodeCheats).GetFields().Where(f => f.FieldType == typeof(UIntPtr));
        foreach (var field in fields)
        {
            field.SetValue(this, UIntPtr.Zero);
        }
    }

    public void Revert()
    {
        var mem = GetInstance();

        if (_noClipAddress > 0)
        {
            mem.WriteArrayMemory(_noClipAddress, new byte[] { 0x0F, 0x11, 0x44, 0x24, 0x54 });
        }

        if (_noHeightLimitAddress > 0)
        {
            mem.WriteArrayMemory(_noHeightLimitAddress, new byte[] { 0xF2, 0x0F, 0x10, 0x9E, 0xC0, 0x05, 0x00, 0x00 });
        }
        
        if (_increasedZoomAddress <= 0) return;
        mem.WriteArrayMemory(_increasedZoomAddress, new byte[] { 0xFF, 0x90, 0x78, 0x03, 0x00, 0x00 });
    }

    public void Continue()
    {
        var mem = GetInstance();

        if (_noClipAddress > 0)
        {
            mem.WriteArrayMemory(_noClipAddress, CalculateDetour(_noClipAddress, NoClipDetourAddress, 5));
        }

        if (_noHeightLimitAddress > 0)
        {
            mem.WriteArrayMemory(_noHeightLimitAddress, CalculateDetour(_noHeightLimitAddress, NoHeightLimitDetourAddress, 8));
        }
        
        if (_increasedZoomAddress <= 0) return;
        mem.WriteArrayMemory(_increasedZoomAddress, CalculateDetour(_increasedZoomAddress, IncreasedZoomDetourAddress, 6));
    }
}