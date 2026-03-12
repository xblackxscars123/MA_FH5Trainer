<<<<<<< C:/Users/GAMING/Documents/GitHub/MA_FH5Trainer/MA_FH5Trainer/MA_FH5Trainer/Converters/AndBooleanMultiConverter.cs
<<<<<<< C:/Users/GAMING/Documents/GitHub/MA_FH5Trainer/MA_FH5Trainer/MA_FH5Trainer/Converters/AndBooleanMultiConverter.cs
<<<<<<< C:/Users/GAMING/Documents/GitHub/MA_FH5Trainer/MA_FH5Trainer/MA_FH5Trainer/Converters/AndBooleanMultiConverter.cs
<<<<<<< C:/Users/GAMING/Documents/GitHub/MA_FH5Trainer/MA_FH5Trainer/MA_FH5Trainer/Converters/AndBooleanMultiConverter.cs
<<<<<<< C:/Users/GAMING/Documents/GitHub/MA_FH5Trainer/MA_FH5Trainer/MA_FH5Trainer/Converters/AndBooleanMultiConverter.cs
<<<<<<< C:/Users/GAMING/Documents/GitHub/MA_FH5Trainer/MA_FH5Trainer/MA_FH5Trainer/Converters/AndBooleanMultiConverter.cs
<<<<<<< C:/Users/GAMING/Documents/GitHub/MA_FH5Trainer/MA_FH5Trainer/MA_FH5Trainer/Converters/AndBooleanMultiConverter.cs
<<<<<<< C:/Users/GAMING/Documents/GitHub/MA_FH5Trainer/MA_FH5Trainer/MA_FH5Trainer/Converters/AndBooleanMultiConverter.cs
<<<<<<< C:/Users/GAMING/Documents/GitHub/MA_FH5Trainer/MA_FH5Trainer/MA_FH5Trainer/Converters/AndBooleanMultiConverter.cs
﻿namespace MA_FH5Trainer.Converters;
=======
﻿namespace XPaint.Converters;
>>>>>>> C:/Users/GAMING/.windsurf/worktrees/MA_FH5Trainer/MA_FH5Trainer-4284e8b3/MA_FH5Trainer/MA_FH5Trainer/Converters/AndBooleanMultiConverter.cs
=======
﻿namespace XPaint.Converters;
>>>>>>> C:/Users/GAMING/.windsurf/worktrees/MA_FH5Trainer/MA_FH5Trainer-4284e8b3/MA_FH5Trainer/MA_FH5Trainer/Converters/AndBooleanMultiConverter.cs
=======
﻿namespace XPaint.Converters;
>>>>>>> C:/Users/GAMING/.windsurf/worktrees/MA_FH5Trainer/MA_FH5Trainer-4284e8b3/MA_FH5Trainer/MA_FH5Trainer/Converters/AndBooleanMultiConverter.cs
=======
﻿namespace XPaint.Converters;
>>>>>>> C:/Users/GAMING/.windsurf/worktrees/MA_FH5Trainer/MA_FH5Trainer-4284e8b3/MA_FH5Trainer/MA_FH5Trainer/Converters/AndBooleanMultiConverter.cs
=======
﻿namespace XPaint.Converters;
>>>>>>> C:/Users/GAMING/.windsurf/worktrees/MA_FH5Trainer/MA_FH5Trainer-4284e8b3/MA_FH5Trainer/MA_FH5Trainer/Converters/AndBooleanMultiConverter.cs
=======
﻿namespace XPaint.Converters;
>>>>>>> C:/Users/GAMING/.windsurf/worktrees/MA_FH5Trainer/MA_FH5Trainer-4284e8b3/MA_FH5Trainer/MA_FH5Trainer/Converters/AndBooleanMultiConverter.cs
=======
﻿namespace XPaint.Converters;
>>>>>>> C:/Users/GAMING/.windsurf/worktrees/MA_FH5Trainer/MA_FH5Trainer-4284e8b3/MA_FH5Trainer/MA_FH5Trainer/Converters/AndBooleanMultiConverter.cs
=======
﻿namespace XPaint.Converters;
>>>>>>> C:/Users/GAMING/.windsurf/worktrees/MA_FH5Trainer/MA_FH5Trainer-90665ca8/MA_FH5Trainer/MA_FH5Trainer/Converters/AndBooleanMultiConverter.cs
=======
﻿namespace XPaint.Converters;
>>>>>>> C:/Users/GAMING/.windsurf/worktrees/MA_FH5Trainer/MA_FH5Trainer-90665ca8/MA_FH5Trainer/MA_FH5Trainer/Converters/AndBooleanMultiConverter.cs

using System;
using System.Globalization;
using System.Windows.Data;

public class AndBooleanMultiConverter : IMultiValueConverter
{
    public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
    {
        if (values.All(v => v is bool) && values.Length == 2)
        {
            return (bool)values[0] && (bool)values[1];
        }

        return false;
    }

    public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}