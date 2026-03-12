<<<<<<< C:/Users/GAMING/Documents/GitHub/MA_FH5Trainer/MA_FH5Trainer/MA_FH5Trainer/Resources/Cheats.cs
<<<<<<< C:/Users/GAMING/Documents/GitHub/MA_FH5Trainer/MA_FH5Trainer/MA_FH5Trainer/Resources/Cheats.cs
<<<<<<< C:/Users/GAMING/Documents/GitHub/MA_FH5Trainer/MA_FH5Trainer/MA_FH5Trainer/Resources/Cheats.cs
<<<<<<< C:/Users/GAMING/Documents/GitHub/MA_FH5Trainer/MA_FH5Trainer/MA_FH5Trainer/Resources/Cheats.cs
<<<<<<< C:/Users/GAMING/Documents/GitHub/MA_FH5Trainer/MA_FH5Trainer/MA_FH5Trainer/Resources/Cheats.cs
<<<<<<< C:/Users/GAMING/Documents/GitHub/MA_FH5Trainer/MA_FH5Trainer/MA_FH5Trainer/Resources/Cheats.cs
<<<<<<< C:/Users/GAMING/Documents/GitHub/MA_FH5Trainer/MA_FH5Trainer/MA_FH5Trainer/Resources/Cheats.cs
<<<<<<< C:/Users/GAMING/Documents/GitHub/MA_FH5Trainer/MA_FH5Trainer/MA_FH5Trainer/Resources/Cheats.cs
<<<<<<< C:/Users/GAMING/Documents/GitHub/MA_FH5Trainer/MA_FH5Trainer/MA_FH5Trainer/Resources/Cheats.cs
﻿namespace MA_FH5Trainer.Resources;
=======
﻿namespace XPaint.Resources;
>>>>>>> C:/Users/GAMING/.windsurf/worktrees/MA_FH5Trainer/MA_FH5Trainer-4284e8b3/MA_FH5Trainer/MA_FH5Trainer/Resources/Cheats.cs
=======
﻿namespace XPaint.Resources;
>>>>>>> C:/Users/GAMING/.windsurf/worktrees/MA_FH5Trainer/MA_FH5Trainer-4284e8b3/MA_FH5Trainer/MA_FH5Trainer/Resources/Cheats.cs
=======
﻿namespace XPaint.Resources;
>>>>>>> C:/Users/GAMING/.windsurf/worktrees/MA_FH5Trainer/MA_FH5Trainer-4284e8b3/MA_FH5Trainer/MA_FH5Trainer/Resources/Cheats.cs
=======
﻿namespace XPaint.Resources;
>>>>>>> C:/Users/GAMING/.windsurf/worktrees/MA_FH5Trainer/MA_FH5Trainer-4284e8b3/MA_FH5Trainer/MA_FH5Trainer/Resources/Cheats.cs
=======
﻿namespace XPaint.Resources;
>>>>>>> C:/Users/GAMING/.windsurf/worktrees/MA_FH5Trainer/MA_FH5Trainer-4284e8b3/MA_FH5Trainer/MA_FH5Trainer/Resources/Cheats.cs
=======
﻿namespace XPaint.Resources;
>>>>>>> C:/Users/GAMING/.windsurf/worktrees/MA_FH5Trainer/MA_FH5Trainer-4284e8b3/MA_FH5Trainer/MA_FH5Trainer/Resources/Cheats.cs
=======
﻿namespace XPaint.Resources;
>>>>>>> C:/Users/GAMING/.windsurf/worktrees/MA_FH5Trainer/MA_FH5Trainer-4284e8b3/MA_FH5Trainer/MA_FH5Trainer/Resources/Cheats.cs
=======
﻿namespace XPaint.Resources;
>>>>>>> C:/Users/GAMING/.windsurf/worktrees/MA_FH5Trainer/MA_FH5Trainer-90665ca8/MA_FH5Trainer/MA_FH5Trainer/Resources/Cheats.cs
=======
﻿namespace XPaint.Resources;
>>>>>>> C:/Users/GAMING/.windsurf/worktrees/MA_FH5Trainer/MA_FH5Trainer-90665ca8/MA_FH5Trainer/MA_FH5Trainer/Resources/Cheats.cs

public static class Cheats
{
    public static readonly Dictionary<Type, object> g_CachedInstances = [];

    public static T GetClass<T>() where T : class
    {
        var classType = typeof(T);
        if (g_CachedInstances.TryGetValue(classType, out var cachedInstance))
        {
            return (T)cachedInstance;
        }
        var newInstance = Activator.CreateInstance(classType) as T;
        g_CachedInstances[classType] = newInstance!;
        return newInstance!;
    }
}