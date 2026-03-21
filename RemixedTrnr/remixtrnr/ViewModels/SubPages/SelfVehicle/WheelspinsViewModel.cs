using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using XPaint.Cheats.Core;

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
