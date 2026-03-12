using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
<<<<<<< C:/Users/GAMING/Documents/GitHub/MA_FH5Trainer/MA_FH5Trainer/MA_FH5Trainer/ViewModels/Pages/TuningViewModel.cs
<<<<<<< C:/Users/GAMING/Documents/GitHub/MA_FH5Trainer/MA_FH5Trainer/MA_FH5Trainer/ViewModels/Pages/TuningViewModel.cs
using XPaint.Cheats.ForzaHorizon5;
=======
using XPaint.Cheats.Core;
>>>>>>> C:/Users/GAMING/.windsurf/worktrees/MA_FH5Trainer/MA_FH5Trainer-90665ca8/MA_FH5Trainer/MA_FH5Trainer/ViewModels/Pages/TuningViewModel.cs
=======
using XPaint.Cheats.Core;
>>>>>>> C:/Users/GAMING/.windsurf/worktrees/MA_FH5Trainer/MA_FH5Trainer-90665ca8/MA_FH5Trainer/MA_FH5Trainer/ViewModels/Pages/TuningViewModel.cs

namespace XPaint.ViewModels.Pages;

public partial class TuningViewModel : ObservableObject
{
    [ObservableProperty]
    private bool _areUiElementsEnabled;
    
    [ObservableProperty]
    private bool _areScanPromptUiElementsEnabled = true;
    
    [ObservableProperty]
    private bool _areScanningUiElementsVisible;

    private static TuningCheats TuningCheatsInst => Resources.Cheats.GetClass<TuningCheats>();
    
    [RelayCommand]
    private async Task Scan()
    {
        AreScanPromptUiElementsEnabled = false;
        AreScanningUiElementsVisible = true;

        if (!TuningCheatsInst.WasScanSuccessful)
        {
            await TuningCheatsInst.Scan();
        }

        if (!TuningCheatsInst.WasScanSuccessful)
        {
            AreScanPromptUiElementsEnabled = true;
            AreScanningUiElementsVisible = false;
            return;
        }

        AreScanningUiElementsVisible = false;
        AreUiElementsEnabled = true;
    }
}