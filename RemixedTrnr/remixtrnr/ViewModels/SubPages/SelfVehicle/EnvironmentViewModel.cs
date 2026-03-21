using CommunityToolkit.Mvvm.ComponentModel;

namespace XPaint.ViewModels.SubPages.SelfVehicle;

public partial class EnvironmentViewModel : ObservableObject
{
    [ObservableProperty]
    private bool _areSunRgbUiElementsEnabled = true;
    
    [ObservableProperty]
    private bool _areManualTimeUiElementsEnabled = true;
}