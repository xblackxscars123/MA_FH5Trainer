using CommunityToolkit.Mvvm.ComponentModel;
<<<<<<< C:/Users/GAMING/Documents/GitHub/MA_FH5Trainer/MA_FH5Trainer/MA_FH5Trainer/ViewModels/SubPages/SelfVehicle/HandlingViewModel.cs
<<<<<<< C:/Users/GAMING/Documents/GitHub/MA_FH5Trainer/MA_FH5Trainer/MA_FH5Trainer/ViewModels/SubPages/SelfVehicle/HandlingViewModel.cs
<<<<<<< C:/Users/GAMING/Documents/GitHub/MA_FH5Trainer/MA_FH5Trainer/MA_FH5Trainer/ViewModels/SubPages/SelfVehicle/HandlingViewModel.cs
<<<<<<< C:/Users/GAMING/Documents/GitHub/MA_FH5Trainer/MA_FH5Trainer/MA_FH5Trainer/ViewModels/SubPages/SelfVehicle/HandlingViewModel.cs
<<<<<<< C:/Users/GAMING/Documents/GitHub/MA_FH5Trainer/MA_FH5Trainer/MA_FH5Trainer/ViewModels/SubPages/SelfVehicle/HandlingViewModel.cs
<<<<<<< C:/Users/GAMING/Documents/GitHub/MA_FH5Trainer/MA_FH5Trainer/MA_FH5Trainer/ViewModels/SubPages/SelfVehicle/HandlingViewModel.cs
<<<<<<< C:/Users/GAMING/Documents/GitHub/MA_FH5Trainer/MA_FH5Trainer/MA_FH5Trainer/ViewModels/SubPages/SelfVehicle/HandlingViewModel.cs
<<<<<<< C:/Users/GAMING/Documents/GitHub/MA_FH5Trainer/MA_FH5Trainer/MA_FH5Trainer/ViewModels/SubPages/SelfVehicle/HandlingViewModel.cs
<<<<<<< C:/Users/GAMING/Documents/GitHub/MA_FH5Trainer/MA_FH5Trainer/MA_FH5Trainer/ViewModels/SubPages/SelfVehicle/HandlingViewModel.cs
using MA_FH5Trainer.Models;
=======
using XPaint.Models;
>>>>>>> C:/Users/GAMING/.windsurf/worktrees/MA_FH5Trainer/MA_FH5Trainer-4284e8b3/MA_FH5Trainer/MA_FH5Trainer/ViewModels/SubPages/SelfVehicle/HandlingViewModel.cs
=======
using XPaint.Models;
>>>>>>> C:/Users/GAMING/.windsurf/worktrees/MA_FH5Trainer/MA_FH5Trainer-4284e8b3/MA_FH5Trainer/MA_FH5Trainer/ViewModels/SubPages/SelfVehicle/HandlingViewModel.cs
=======
using XPaint.Models;
>>>>>>> C:/Users/GAMING/.windsurf/worktrees/MA_FH5Trainer/MA_FH5Trainer-4284e8b3/MA_FH5Trainer/MA_FH5Trainer/ViewModels/SubPages/SelfVehicle/HandlingViewModel.cs
=======
using XPaint.Models;
>>>>>>> C:/Users/GAMING/.windsurf/worktrees/MA_FH5Trainer/MA_FH5Trainer-4284e8b3/MA_FH5Trainer/MA_FH5Trainer/ViewModels/SubPages/SelfVehicle/HandlingViewModel.cs
=======
using XPaint.Models;
>>>>>>> C:/Users/GAMING/.windsurf/worktrees/MA_FH5Trainer/MA_FH5Trainer-4284e8b3/MA_FH5Trainer/MA_FH5Trainer/ViewModels/SubPages/SelfVehicle/HandlingViewModel.cs
=======
using XPaint.Models;
>>>>>>> C:/Users/GAMING/.windsurf/worktrees/MA_FH5Trainer/MA_FH5Trainer-4284e8b3/MA_FH5Trainer/MA_FH5Trainer/ViewModels/SubPages/SelfVehicle/HandlingViewModel.cs
=======
using XPaint.Models;
>>>>>>> C:/Users/GAMING/.windsurf/worktrees/MA_FH5Trainer/MA_FH5Trainer-4284e8b3/MA_FH5Trainer/MA_FH5Trainer/ViewModels/SubPages/SelfVehicle/HandlingViewModel.cs
=======
using XPaint.Models;
>>>>>>> C:/Users/GAMING/.windsurf/worktrees/MA_FH5Trainer/MA_FH5Trainer-90665ca8/MA_FH5Trainer/MA_FH5Trainer/ViewModels/SubPages/SelfVehicle/HandlingViewModel.cs
=======
using XPaint.Models;
>>>>>>> C:/Users/GAMING/.windsurf/worktrees/MA_FH5Trainer/MA_FH5Trainer-90665ca8/MA_FH5Trainer/MA_FH5Trainer/ViewModels/SubPages/SelfVehicle/HandlingViewModel.cs

namespace XPaint.ViewModels.SubPages.SelfVehicle;

public partial class HandlingViewModel : ObservableObject
{
    [ObservableProperty]
    private bool _areUiElementsEnabled = true;

    [ObservableProperty]
    private double _accelValue;
    
    [ObservableProperty]
    private double _gravityValue;

    [ObservableProperty]
    private bool _isAccelEnabled;
    
    [ObservableProperty]
    private bool _isGravityEnabled;
}