using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using XPaint.Views.Windows;
using static XPaint.Resources.Cheats;

namespace XPaint.ViewModels.Pages;

public partial class GarageViewModel : ObservableObject
{
    [ObservableProperty]
    private bool _uiElementsEnabled = true;

    [ObservableProperty]
    private string _selectedCarType = "muscle";

    [ObservableProperty]
    private int _auctionMinPrice = 5000;

    [ObservableProperty]
    private int _auctionMaxPrice = 20000000;

    [ObservableProperty]
    private bool _auctionPricesLoaded = false;

    private static Cheats.Core.Sql SqlDefault => GetClass<Cheats.Core.Sql>();

    [RelayCommand]
    private async Task ExecuteSql(object parameter)
    {
        if (parameter is not string sParam || MainWindow.Instance?.ViewModel.Attached != true)
            return;

        UiElementsEnabled = false;
        await Query(sParam);
        UiElementsEnabled = true;
    }

    [RelayCommand]
    private async Task ExecuteRankSql(object parameter)
    {
        if (parameter is not string sParam || MainWindow.Instance?.ViewModel.Attached != true)
            return;

        UiElementsEnabled = false;
        var canEdit = await SqlDefault.PullAob();
        if (canEdit)
            await Query(sParam);

        UiElementsEnabled = true;
    }

    [RelayCommand]
    private async Task AddCarsByType(string carType)
    {
        if (MainWindow.Instance?.ViewModel.Attached != true || string.IsNullOrEmpty(carType))
            return;

        UiElementsEnabled = false;
        await Query($"INSERT INTO Profile0_Career_Garage(CarId, IsFavorite, HasCurrentOwnerViewedCar, VersionedTuneId, VersionedLiveryId) SELECT Id, 0, 1, '00000000-0000-0000-0000-000000000000', '00000000-0000-0000-0000-000000000000' FROM Data_Car WHERE CarType = '{carType}' AND Id NOT IN (SELECT CarId FROM Profile0_Career_Garage);");
        UiElementsEnabled = true;
    }

    [RelayCommand]
    private void LoadAuctionPrices()
    {
        if (MainWindow.Instance?.ViewModel.Attached != true)
            return;

        AuctionMinPrice = 5000;
        AuctionMaxPrice = 20000000;
        AuctionPricesLoaded = true;
    }

    [RelayCommand]
    private async Task SetAuctionPrices()
    {
        if (MainWindow.Instance?.ViewModel.Attached != true)
            return;

        UiElementsEnabled = false;
        await Query($"UPDATE Profile0_Career_Garage SET AuctionMinPrice = {AuctionMinPrice}, AuctionMaxPrice = {AuctionMaxPrice};");
        UiElementsEnabled = true;
    }

    [RelayCommand]
    private async Task FixCorruptedStats(object parameter)
    {
        if (MainWindow.Instance?.ViewModel.Attached != true)
            return;

        if (parameter is not string sParam)
            return;

        UiElementsEnabled = false;
        await Query(sParam);
        UiElementsEnabled = true;
    }

    private static async Task Query(string command)
    {
        if (!SqlDefault.WereScansSuccessful)
            await SqlDefault.SqlExecAobScan();

        if (SqlDefault.WereScansSuccessful)
            await Task.Run(() => SqlDefault.Query(command));
    }
}
