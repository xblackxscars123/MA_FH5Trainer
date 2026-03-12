using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using XPaint.Models;
using XPaint.Views.Windows;
using static XPaint.Resources.Cheats;

namespace XPaint.ViewModels.Pages;

public partial class AutoshowViewModel : ObservableObject
{
    [ObservableProperty]
    private bool _uiElementsEnabled = true;

    [ObservableProperty]
    private bool _allCarsEnabled;

    [ObservableProperty]
    private bool _rareCarsEnabled;
    
    [ObservableProperty]
    private bool _freeCarsEnabled;
    
    private static Cheats.Core.Sql SqlDefault => GetClass<Cheats.Core.Sql>();

    [RelayCommand]
    private async Task ExecuteSql(object parameter)
    {
        if (MainWindow.Instance == null)
        {
            return;
        }
        
        if (parameter is not string sParam || !MainWindow.Instance.ViewModel.Attached)
        {
            return;
        }
        
        UiElementsEnabled = false;
        await Query(sParam);
        UiElementsEnabled = true;
    }

    private static async Task Query(string command)
    {
        if (!SqlDefault.WereScansSuccessful)
        {
            await SqlDefault.SqlExecAobScan();
        }

        if (SqlDefault.WereScansSuccessful)
        {
            await Task.Run(() => SqlDefault.Query(command));
        }
    }
}