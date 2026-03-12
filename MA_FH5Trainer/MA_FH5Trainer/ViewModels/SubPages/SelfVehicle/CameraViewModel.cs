<<<<<<< C:/Users/GAMING/Documents/GitHub/MA_FH5Trainer/MA_FH5Trainer/MA_FH5Trainer/ViewModels/SubPages/SelfVehicle/CameraViewModel.cs
<<<<<<< C:/Users/GAMING/Documents/GitHub/MA_FH5Trainer/MA_FH5Trainer/MA_FH5Trainer/ViewModels/SubPages/SelfVehicle/CameraViewModel.cs
﻿using CommunityToolkit.Mvvm.ComponentModel;
<<<<<<< C:/Users/GAMING/Documents/GitHub/MA_FH5Trainer/MA_FH5Trainer/MA_FH5Trainer/ViewModels/SubPages/SelfVehicle/CameraViewModel.cs
<<<<<<< C:/Users/GAMING/Documents/GitHub/MA_FH5Trainer/MA_FH5Trainer/MA_FH5Trainer/ViewModels/SubPages/SelfVehicle/CameraViewModel.cs
<<<<<<< C:/Users/GAMING/Documents/GitHub/MA_FH5Trainer/MA_FH5Trainer/MA_FH5Trainer/ViewModels/SubPages/SelfVehicle/CameraViewModel.cs
<<<<<<< C:/Users/GAMING/Documents/GitHub/MA_FH5Trainer/MA_FH5Trainer/MA_FH5Trainer/ViewModels/SubPages/SelfVehicle/CameraViewModel.cs
<<<<<<< C:/Users/GAMING/Documents/GitHub/MA_FH5Trainer/MA_FH5Trainer/MA_FH5Trainer/ViewModels/SubPages/SelfVehicle/CameraViewModel.cs
<<<<<<< C:/Users/GAMING/Documents/GitHub/MA_FH5Trainer/MA_FH5Trainer/MA_FH5Trainer/ViewModels/SubPages/SelfVehicle/CameraViewModel.cs
<<<<<<< C:/Users/GAMING/Documents/GitHub/MA_FH5Trainer/MA_FH5Trainer/MA_FH5Trainer/ViewModels/SubPages/SelfVehicle/CameraViewModel.cs
using MA_FH5Trainer.Models;
=======
using XPaint.Models;
>>>>>>> C:/Users/GAMING/.windsurf/worktrees/MA_FH5Trainer/MA_FH5Trainer-4284e8b3/MA_FH5Trainer/MA_FH5Trainer/ViewModels/SubPages/SelfVehicle/CameraViewModel.cs
=======
using XPaint.Models;
>>>>>>> C:/Users/GAMING/.windsurf/worktrees/MA_FH5Trainer/MA_FH5Trainer-4284e8b3/MA_FH5Trainer/MA_FH5Trainer/ViewModels/SubPages/SelfVehicle/CameraViewModel.cs
=======
using XPaint.Models;
>>>>>>> C:/Users/GAMING/.windsurf/worktrees/MA_FH5Trainer/MA_FH5Trainer-4284e8b3/MA_FH5Trainer/MA_FH5Trainer/ViewModels/SubPages/SelfVehicle/CameraViewModel.cs
=======
using XPaint.Models;
>>>>>>> C:/Users/GAMING/.windsurf/worktrees/MA_FH5Trainer/MA_FH5Trainer-4284e8b3/MA_FH5Trainer/MA_FH5Trainer/ViewModels/SubPages/SelfVehicle/CameraViewModel.cs
=======
using XPaint.Models;
>>>>>>> C:/Users/GAMING/.windsurf/worktrees/MA_FH5Trainer/MA_FH5Trainer-4284e8b3/MA_FH5Trainer/MA_FH5Trainer/ViewModels/SubPages/SelfVehicle/CameraViewModel.cs
=======
using XPaint.Models;
>>>>>>> C:/Users/GAMING/.windsurf/worktrees/MA_FH5Trainer/MA_FH5Trainer-4284e8b3/MA_FH5Trainer/MA_FH5Trainer/ViewModels/SubPages/SelfVehicle/CameraViewModel.cs
=======
using XPaint.Models;
>>>>>>> C:/Users/GAMING/.windsurf/worktrees/MA_FH5Trainer/MA_FH5Trainer-4284e8b3/MA_FH5Trainer/MA_FH5Trainer/ViewModels/SubPages/SelfVehicle/CameraViewModel.cs
=======
using CommunityToolkit.Mvvm.ComponentModel;
using XPaint.Models;
>>>>>>> C:/Users/GAMING/.windsurf/worktrees/MA_FH5Trainer/MA_FH5Trainer-90665ca8/MA_FH5Trainer/MA_FH5Trainer/ViewModels/SubPages/SelfVehicle/CameraViewModel.cs
=======
using CommunityToolkit.Mvvm.ComponentModel;
using XPaint.Models;
>>>>>>> C:/Users/GAMING/.windsurf/worktrees/MA_FH5Trainer/MA_FH5Trainer-90665ca8/MA_FH5Trainer/MA_FH5Trainer/ViewModels/SubPages/SelfVehicle/CameraViewModel.cs

namespace XPaint.ViewModels.SubPages.SelfVehicle;

public partial class CameraViewModel : ObservableObject
{
    public bool IsDefault => GameVerPlat.GetInstance().Type == GameVerPlat.GameType.Default;
    
    [ObservableProperty]
    private bool _areScanPromptLimiterUiElementsVisible = true;
    
    [ObservableProperty]
    private bool _areScanningLimiterUiElementsVisible;
    
    [ObservableProperty]
    private bool _areLimiterUiElementsVisible;
    
    [ObservableProperty]
    private bool _areCameraHookUiElementsEnabled = false;
    
    [ObservableProperty]
    private bool _areCameraOffsetUiElementsEnabled = false;

    [ObservableProperty] 
    private bool _fovEnabled = false;
    
    [ObservableProperty] 
    private bool _offsetEnabled = false;
    
}