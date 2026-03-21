using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Win32;
using System.Windows;
using XPaint.Cheats;
using XPaint.Cheats.Core;
using XPaint.Views.Windows;
using static XPaint.Resources.Cheats;

namespace XPaint.ViewModels.SubPages;

public partial class PainterViewModel : ObservableObject
{
    [ObservableProperty] private bool _uiElementsEnabled = true;
    [ObservableProperty] private string _selectedImagePath = string.Empty;
    [ObservableProperty] private int _currentVinylCount = 0;
    [ObservableProperty] private int _maxVinylLayers = 3000;
    [ObservableProperty] private bool _painterInitialized = false;
    [ObservableProperty] private double _importProgress = 0;
    [ObservableProperty] private string _importStatus = string.Empty;

    private static Cheats.Core.PainterCheats PainterDefault => GetClass<Cheats.Core.PainterCheats>();

    [RelayCommand]
    private async Task InitializePainter()
    {
        if (MainWindow.Instance?.ViewModel.Attached != true) return;

        UiElementsEnabled = false;
        PainterInitialized = await PainterDefault.InitializePainter();
        if (PainterInitialized)
            CurrentVinylCount = PainterDefault.GetCurrentVinylCount();
        UiElementsEnabled = true;
    }

    [RelayCommand]
    private void SelectImage()
    {
        var dlg = new OpenFileDialog
        {
            Filter = "Image files (*.png;*.jpg;*.jpeg;*.bmp)|*.png;*.jpg;*.jpeg;*.bmp|All files (*.*)|*.*",
            Title = "Select Image for Livery"
        };
        if (dlg.ShowDialog() == true)
            SelectedImagePath = dlg.FileName;
    }

    [RelayCommand]
    private async Task ImportImage()
    {
        if (MainWindow.Instance?.ViewModel.Attached != true) return;
        if (string.IsNullOrEmpty(SelectedImagePath))
        {
            MessageBox.Show("Please select an image first.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
            return;
        }
        if (!PainterInitialized)
        {
            MessageBox.Show("Please initialize the painter first.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
            return;
        }

        UiElementsEnabled = false;
        ImportProgress = 0;
        ImportStatus = "Processing image...";

        List<VinylLayer> vinyls = [];
        await Task.Run(() =>
        {
            vinyls = ImageProcessor.ConvertImageToVinyls(SelectedImagePath, MaxVinylLayers - CurrentVinylCount);
        });

        ImportStatus = $"Writing {vinyls.Count} vinyl layers...";
        ImportProgress = 50;

        await Task.Run(() => PainterDefault.AddVinylLayers(vinyls));

        CurrentVinylCount = PainterDefault.GetCurrentVinylCount();
        ImportProgress = 100;
        ImportStatus = $"Done — {vinyls.Count} layers imported.";

        UiElementsEnabled = true;
    }

    [RelayCommand]
    private void RefreshVinylCount()
    {
        if (!PainterInitialized) return;
        CurrentVinylCount = PainterDefault.GetCurrentVinylCount();
    }

    [RelayCommand]
    private void ClearAllVinyls()
    {
        if (!PainterInitialized) return;
        if (MessageBox.Show("Clear all vinyl layers?", "Confirm", MessageBoxButton.YesNo, MessageBoxImage.Question) != MessageBoxResult.Yes)
            return;
        PainterDefault.ClearAllVinyls();
        RefreshVinylCount();
        ImportStatus = string.Empty;
        ImportProgress = 0;
    }
}
