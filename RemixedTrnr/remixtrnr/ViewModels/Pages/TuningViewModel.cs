using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using XPaint.Cheats.Core;

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
