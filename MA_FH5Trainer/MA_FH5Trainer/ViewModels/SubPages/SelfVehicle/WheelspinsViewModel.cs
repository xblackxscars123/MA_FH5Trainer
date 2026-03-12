using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
<<<<<<< C:/Users/GAMING/Documents/GitHub/MA_FH5Trainer/MA_FH5Trainer/MA_FH5Trainer/ViewModels/SubPages/SelfVehicle/WheelspinsViewModel.cs
<<<<<<< C:/Users/GAMING/Documents/GitHub/MA_FH5Trainer/MA_FH5Trainer/MA_FH5Trainer/ViewModels/SubPages/SelfVehicle/WheelspinsViewModel.cs
using XPaint.Cheats.ForzaHorizon5;
=======
using XPaint.Cheats.Core;
>>>>>>> C:/Users/GAMING/.windsurf/worktrees/MA_FH5Trainer/MA_FH5Trainer-90665ca8/MA_FH5Trainer/MA_FH5Trainer/ViewModels/SubPages/SelfVehicle/WheelspinsViewModel.cs
=======
using XPaint.Cheats.Core;
>>>>>>> C:/Users/GAMING/.windsurf/worktrees/MA_FH5Trainer/MA_FH5Trainer-90665ca8/MA_FH5Trainer/MA_FH5Trainer/ViewModels/SubPages/SelfVehicle/WheelspinsViewModel.cs
using System.Security.Policy;

namespace XPaint.ViewModels.SubPages.SelfVehicle;
public partial class WheelspinsViewModel : ObservableObject
{
    [RelayCommand]
    private async Task Wheelspins(object param)
    {
        float value = (float)(double)param;
        UnlocksCheats unl = XPaint.Resources.Cheats.GetClass<UnlocksCheats>();
        await unl.CheatWheelspins(value);
    }

    [RelayCommand]
    private async Task SuperWheelspins(object param)
    {
        float value = (float)(double)param;
        UnlocksCheats unl = XPaint.Resources.Cheats.GetClass<UnlocksCheats>();
        await unl.CheatSuperWheelspins(value);
    }
}