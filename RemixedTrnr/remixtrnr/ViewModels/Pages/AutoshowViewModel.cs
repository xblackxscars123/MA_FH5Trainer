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

    [ObservableProperty]
    private string _selectedCarType = "muscle";

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

    [RelayCommand]
    private async Task AddCarsByType(string carType)
    {
        if (MainWindow.Instance?.ViewModel.Attached != true || string.IsNullOrEmpty(carType))
            return;

        UiElementsEnabled = false;
        await Query($"UPDATE Data_Car SET NotAvailableInAutoshow = 0, BaseCost = 0 WHERE CarType = '{carType}';");
        UiElementsEnabled = true;
    }

    [RelayCommand]
    private async Task AddAllCarsToAutoshow()
    {
        if (MainWindow.Instance?.ViewModel.Attached != true)
            return;

        UiElementsEnabled = false;
        await Query("DROP TABLE IF EXISTS CostBackup; UPDATE Data_Car SET NotAvailableInAutoshow = 0, BaseCost = 0;");
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