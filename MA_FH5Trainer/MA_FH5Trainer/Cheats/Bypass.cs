<<<<<<< C:/Users/GAMING/Documents/GitHub/MA_FH5Trainer/MA_FH5Trainer/MA_FH5Trainer/Cheats/Bypass.cs
<<<<<<< C:/Users/GAMING/Documents/GitHub/MA_FH5Trainer/MA_FH5Trainer/MA_FH5Trainer/Cheats/Bypass.cs
<<<<<<< C:/Users/GAMING/Documents/GitHub/MA_FH5Trainer/MA_FH5Trainer/MA_FH5Trainer/Cheats/Bypass.cs
<<<<<<< C:/Users/GAMING/Documents/GitHub/MA_FH5Trainer/MA_FH5Trainer/MA_FH5Trainer/Cheats/Bypass.cs
<<<<<<< C:/Users/GAMING/Documents/GitHub/MA_FH5Trainer/MA_FH5Trainer/MA_FH5Trainer/Cheats/Bypass.cs
<<<<<<< C:/Users/GAMING/Documents/GitHub/MA_FH5Trainer/MA_FH5Trainer/MA_FH5Trainer/Cheats/Bypass.cs
<<<<<<< C:/Users/GAMING/Documents/GitHub/MA_FH5Trainer/MA_FH5Trainer/MA_FH5Trainer/Cheats/Bypass.cs
<<<<<<< C:/Users/GAMING/Documents/GitHub/MA_FH5Trainer/MA_FH5Trainer/MA_FH5Trainer/Cheats/Bypass.cs
<<<<<<< C:/Users/GAMING/Documents/GitHub/MA_FH5Trainer/MA_FH5Trainer/MA_FH5Trainer/Cheats/Bypass.cs
<<<<<<< C:/Users/GAMING/Documents/GitHub/MA_FH5Trainer/MA_FH5Trainer/MA_FH5Trainer/Cheats/Bypass.cs
<<<<<<< C:/Users/GAMING/Documents/GitHub/MA_FH5Trainer/MA_FH5Trainer/MA_FH5Trainer/Cheats/Bypass.cs
﻿using static MA_FH5Trainer.Resources.Memory;
=======
﻿using static XPaint.Resources.Memory;
>>>>>>> C:/Users/GAMING/.windsurf/worktrees/MA_FH5Trainer/MA_FH5Trainer-4284e8b3/MA_FH5Trainer/MA_FH5Trainer/Cheats/Bypass.cs
=======
﻿using static XPaint.Resources.Memory;
>>>>>>> C:/Users/GAMING/.windsurf/worktrees/MA_FH5Trainer/MA_FH5Trainer-4284e8b3/MA_FH5Trainer/MA_FH5Trainer/Cheats/Bypass.cs
=======
﻿using static XPaint.Resources.Memory;
using static XPaint.Resources.StringCipher;
>>>>>>> C:/Users/GAMING/.windsurf/worktrees/MA_FH5Trainer/MA_FH5Trainer-4284e8b3/MA_FH5Trainer/MA_FH5Trainer/Cheats/Bypass.cs
=======
﻿using static XPaint.Resources.Memory;
using static XPaint.Resources.StringCipher;
>>>>>>> C:/Users/GAMING/.windsurf/worktrees/MA_FH5Trainer/MA_FH5Trainer-4284e8b3/MA_FH5Trainer/MA_FH5Trainer/Cheats/Bypass.cs
=======
﻿using static XPaint.Resources.Memory;
using static XPaint.Resources.StringCipher;
>>>>>>> C:/Users/GAMING/.windsurf/worktrees/MA_FH5Trainer/MA_FH5Trainer-4284e8b3/MA_FH5Trainer/MA_FH5Trainer/Cheats/Bypass.cs
=======
﻿using static XPaint.Resources.Memory;
using static XPaint.Resources.StringCipher;
>>>>>>> C:/Users/GAMING/.windsurf/worktrees/MA_FH5Trainer/MA_FH5Trainer-4284e8b3/MA_FH5Trainer/MA_FH5Trainer/Cheats/Bypass.cs
=======
﻿using static XPaint.Resources.Memory;
using static XPaint.Resources.StringCipher;
>>>>>>> C:/Users/GAMING/.windsurf/worktrees/MA_FH5Trainer/MA_FH5Trainer-4284e8b3/MA_FH5Trainer/MA_FH5Trainer/Cheats/Bypass.cs

namespace XPaint.Cheats.ForzaHorizon5;
=======
using static XPaint.Resources.Memory;
using static XPaint.Resources.StringCipher;

namespace XPaint.Cheats.Core;
>>>>>>> C:/Users/GAMING/.windsurf/worktrees/MA_FH5Trainer/MA_FH5Trainer-90665ca8/MA_FH5Trainer/MA_FH5Trainer/Cheats/Bypass.cs
=======
using static XPaint.Resources.Memory;
using static XPaint.Resources.StringCipher;

namespace XPaint.Cheats.Core;
>>>>>>> C:/Users/GAMING/.windsurf/worktrees/MA_FH5Trainer/MA_FH5Trainer-90665ca8/MA_FH5Trainer/MA_FH5Trainer/Cheats/Bypass.cs
=======
using static XPaint.Resources.Memory;
using static XPaint.Resources.StringCipher;

namespace XPaint.Cheats.Core;
>>>>>>> C:/Users/GAMING/.windsurf/worktrees/MA_FH5Trainer/MA_FH5Trainer-4284e8b3/MA_FH5Trainer/MA_FH5Trainer/Cheats/Bypass.cs
=======
using static XPaint.Resources.Memory;
using static XPaint.Resources.StringCipher;

namespace XPaint.Cheats.Core;
>>>>>>> C:/Users/GAMING/.windsurf/worktrees/MA_FH5Trainer/MA_FH5Trainer-4284e8b3/MA_FH5Trainer/MA_FH5Trainer/Cheats/Bypass.cs

public class Bypass : CheatsUtilities, ICheatsBase
{
    public UIntPtr CallAddress;
    
    private bool m_applied;
    private bool m_scanning;
    private static readonly object s_Lock = new();
    
    public async Task DisableCrcChecks()
    {
        lock (s_Lock)
        {
            if (m_scanning || m_applied)
            {
                return;
            }

            m_scanning = true;
        }
        
        var callAddress = await SmartAobScan(D("k3jRb8oO60mXfdFlvQ7rSZd90WW8"));
        if (callAddress == 0)
        {
            ShowError("CRC", "CallAddress == 0");
            lock (s_Lock)
            {
                m_scanning = false;
            }
            return;
        }

        lock (s_Lock)
        {
            CallAddress = callAddress;
            byte[] patch = [0x48, 0x39, 0xFF];
            GetInstance().WriteArrayMemory(CallAddress, patch);
            m_scanning = false;
            m_applied = true;
        }
    }
    
    public void Cleanup()
    {
        lock (s_Lock)
        {
            byte[] orig = [0x4C, 0x3B, 0xEF];
            GetInstance().WriteArrayMemory(CallAddress, orig);
        }

        Reset();
    }

    public void Reset()
    {
        lock (s_Lock)
        {
            m_scanning = false;
            m_applied = false;
            CallAddress = 0;
        }
    }
}