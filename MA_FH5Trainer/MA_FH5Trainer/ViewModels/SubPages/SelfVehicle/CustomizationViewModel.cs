<<<<<<< C:/Users/GAMING/Documents/GitHub/MA_FH5Trainer/MA_FH5Trainer/MA_FH5Trainer/ViewModels/SubPages/SelfVehicle/CustomizationViewModel.cs
<<<<<<< C:/Users/GAMING/Documents/GitHub/MA_FH5Trainer/MA_FH5Trainer/MA_FH5Trainer/ViewModels/SubPages/SelfVehicle/CustomizationViewModel.cs
﻿using CommunityToolkit.Mvvm.ComponentModel;
<<<<<<< C:/Users/GAMING/Documents/GitHub/MA_FH5Trainer/MA_FH5Trainer/MA_FH5Trainer/ViewModels/SubPages/SelfVehicle/CustomizationViewModel.cs
<<<<<<< C:/Users/GAMING/Documents/GitHub/MA_FH5Trainer/MA_FH5Trainer/MA_FH5Trainer/ViewModels/SubPages/SelfVehicle/CustomizationViewModel.cs
<<<<<<< C:/Users/GAMING/Documents/GitHub/MA_FH5Trainer/MA_FH5Trainer/MA_FH5Trainer/ViewModels/SubPages/SelfVehicle/CustomizationViewModel.cs
<<<<<<< C:/Users/GAMING/Documents/GitHub/MA_FH5Trainer/MA_FH5Trainer/MA_FH5Trainer/ViewModels/SubPages/SelfVehicle/CustomizationViewModel.cs
<<<<<<< C:/Users/GAMING/Documents/GitHub/MA_FH5Trainer/MA_FH5Trainer/MA_FH5Trainer/ViewModels/SubPages/SelfVehicle/CustomizationViewModel.cs
<<<<<<< C:/Users/GAMING/Documents/GitHub/MA_FH5Trainer/MA_FH5Trainer/MA_FH5Trainer/ViewModels/SubPages/SelfVehicle/CustomizationViewModel.cs
<<<<<<< C:/Users/GAMING/Documents/GitHub/MA_FH5Trainer/MA_FH5Trainer/MA_FH5Trainer/ViewModels/SubPages/SelfVehicle/CustomizationViewModel.cs
using MA_FH5Trainer.Models;
=======
using XPaint.Models;
>>>>>>> C:/Users/GAMING/.windsurf/worktrees/MA_FH5Trainer/MA_FH5Trainer-4284e8b3/MA_FH5Trainer/MA_FH5Trainer/ViewModels/SubPages/SelfVehicle/CustomizationViewModel.cs
=======
using XPaint.Models;
>>>>>>> C:/Users/GAMING/.windsurf/worktrees/MA_FH5Trainer/MA_FH5Trainer-4284e8b3/MA_FH5Trainer/MA_FH5Trainer/ViewModels/SubPages/SelfVehicle/CustomizationViewModel.cs
=======
using XPaint.Models;
>>>>>>> C:/Users/GAMING/.windsurf/worktrees/MA_FH5Trainer/MA_FH5Trainer-4284e8b3/MA_FH5Trainer/MA_FH5Trainer/ViewModels/SubPages/SelfVehicle/CustomizationViewModel.cs
=======
using XPaint.Models;
>>>>>>> C:/Users/GAMING/.windsurf/worktrees/MA_FH5Trainer/MA_FH5Trainer-4284e8b3/MA_FH5Trainer/MA_FH5Trainer/ViewModels/SubPages/SelfVehicle/CustomizationViewModel.cs
=======
using XPaint.Models;
>>>>>>> C:/Users/GAMING/.windsurf/worktrees/MA_FH5Trainer/MA_FH5Trainer-4284e8b3/MA_FH5Trainer/MA_FH5Trainer/ViewModels/SubPages/SelfVehicle/CustomizationViewModel.cs
=======
using XPaint.Models;
>>>>>>> C:/Users/GAMING/.windsurf/worktrees/MA_FH5Trainer/MA_FH5Trainer-4284e8b3/MA_FH5Trainer/MA_FH5Trainer/ViewModels/SubPages/SelfVehicle/CustomizationViewModel.cs
=======
using XPaint.Models;
>>>>>>> C:/Users/GAMING/.windsurf/worktrees/MA_FH5Trainer/MA_FH5Trainer-4284e8b3/MA_FH5Trainer/MA_FH5Trainer/ViewModels/SubPages/SelfVehicle/CustomizationViewModel.cs
=======
using CommunityToolkit.Mvvm.ComponentModel;
using XPaint.Models;
>>>>>>> C:/Users/GAMING/.windsurf/worktrees/MA_FH5Trainer/MA_FH5Trainer-90665ca8/MA_FH5Trainer/MA_FH5Trainer/ViewModels/SubPages/SelfVehicle/CustomizationViewModel.cs
=======
using CommunityToolkit.Mvvm.ComponentModel;
using XPaint.Models;
>>>>>>> C:/Users/GAMING/.windsurf/worktrees/MA_FH5Trainer/MA_FH5Trainer-90665ca8/MA_FH5Trainer/MA_FH5Trainer/ViewModels/SubPages/SelfVehicle/CustomizationViewModel.cs

namespace XPaint.ViewModels.SubPages.SelfVehicle;

public partial class CustomizationViewModel : ObservableObject
{
    public bool IsDefault => GameVerPlat.GetInstance().Type == GameVerPlat.GameType.Default;
    
    [ObservableProperty]
    private bool _areMainUiElementsEnabled = true;
    
    [ObservableProperty]
    private bool _areHeadlightUiElementsEnabled = false;
    
    [ObservableProperty]
    private bool _areBackfireUiElementsEnabled = false;
    
    [ObservableProperty]
    private bool _dirtEnabled;
    
    [ObservableProperty]
    private float _dirtValue;

    [ObservableProperty]
    private bool _mudEnabled;
    
    [ObservableProperty]
    private float _mudValue;

    [ObservableProperty]
    private bool _glowingPaintIsOn;
    
    [ObservableProperty]
    private bool _glowingPaintEnabled;
    
    [ObservableProperty]
    private float _glowingPaintValue;

    [ObservableProperty]
    private bool _forceLodEnabled;
    
    [ObservableProperty]
    private int _forceLodValue;
}