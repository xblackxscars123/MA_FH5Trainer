using System.Numerics;
using System.Windows;
using System.Windows.Media;
using ControlzEx.Standard;
using XPaint.Cheats.Core;
using XPaint.Models;
using XPaint.ViewModels.SubPages.SelfVehicle;
using XPaint.Views.Windows;
using MahApps.Metro.Controls;
using static XPaint.Resources.Cheats;
using static XPaint.Resources.Memory;

namespace XPaint.Views.SubPages.SelfVehicle;

public partial class Environment
{
    public Environment()
    {
        MainWindow = MainWindow.Instance ?? new MainWindow();
        ViewModel =new EnvironmentViewModel();
        DataContext = this;
        
        InitializeComponent();
    }

    public MainWindow MainWindow { get; }
    public EnvironmentViewModel ViewModel { get; }
    private static EnvironmentCheats EnvironmentCheatsInst => GetClass<EnvironmentCheats>();
    private static CarCheats CarCheatsInst => GetClass<CarCheats>();
    
    private static Vector4 ConvertUiColorToGameValues(Color uiColor, double intensity)
    {
        var fIntensity = Convert.ToSingle(intensity);
        var alpha = uiColor.A / 255f;
        var red = uiColor.R / 255f * alpha * fIntensity;
        var green = uiColor.G / 255f * alpha * fIntensity;
        var blue = uiColor.B / 255f * alpha * fIntensity;
        return new Vector4(1 + red, 1 + green, 1 + blue, 1);        
    }

    private async void RgbSwitch_OnToggled(object sender, RoutedEventArgs e)
    {
        if (sender is not ToggleSwitch toggleSwitch)
        {
            return;
        }

        ViewModel.AreSunRgbUiElementsEnabled = false;
        if (EnvironmentCheatsInst.SunRgbDetourAddress == 0)
        {
            await EnvironmentCheatsInst.CheatSunRgb();
        }
        ViewModel.AreSunRgbUiElementsEnabled = true;
        if (EnvironmentCheatsInst.SunRgbDetourAddress == 0)
        {
            toggleSwitch.Toggled -= RgbSwitch_OnToggled;
            toggleSwitch.IsOn = false;
            toggleSwitch.Toggled += RgbSwitch_OnToggled;
            return;
        }
        
        GetInstance().WriteMemory(EnvironmentCheatsInst.SunRgbDetourAddress + 0x32, toggleSwitch.IsOn ? (byte)1 : (byte)0);

        var color = Picker.SelectedColor.GetValueOrDefault();
        var intensity = IntensityBox.Value.GetValueOrDefault();
        var gameValues = ConvertUiColorToGameValues(color, intensity);
        GetInstance().WriteMemory(EnvironmentCheatsInst.SunRgbDetourAddress + 0x33, gameValues);
    }

    private void Picker_OnSelectedColorChanged(object sender, RoutedPropertyChangedEventArgs<Color?> e)
    {
        var color = Picker.SelectedColor.GetValueOrDefault();
        var intensity = IntensityBox?.Value.GetValueOrDefault() ?? 5;
        UpdateSunSwatch(color, intensity);

        if (EnvironmentCheatsInst.SunRgbDetourAddress == 0)
        {
            return;
        }
        
        var gameValues = ConvertUiColorToGameValues(color, intensity);
        GetInstance().WriteMemory(EnvironmentCheatsInst.SunRgbDetourAddress + 0x33, gameValues);
    }

    private void UpdateSunSwatch(Color c, double intensity)
    {
        if (SunSwatch == null) return;
        var a = c.A / 255f;
        var i = (float)intensity;
        SunSwatch.Background = new SolidColorBrush(Color.FromRgb(
            (byte)Math.Clamp((int)MathF.Round(c.R / 255f * a * i * 255), 0, 255),
            (byte)Math.Clamp((int)MathF.Round(c.G / 255f * a * i * 255), 0, 255),
            (byte)Math.Clamp((int)MathF.Round(c.B / 255f * a * i * 255), 0, 255)));
    }

    private async void PullButton_OnClick(object sender, RoutedEventArgs e)
    {
        ViewModel.AreManualTimeUiElementsEnabled = false;
        if (EnvironmentCheatsInst.TimeDetourAddress == 0)
        {
            await EnvironmentCheatsInst.CheatTime();
        }
        ViewModel.AreManualTimeUiElementsEnabled = true;

        if (EnvironmentCheatsInst.TimeDetourAddress == 0)
        {
            return;
        }
        
        TimeBox.Value = Math.Round(GetInstance().ReadMemory<double>(EnvironmentCheatsInst.TimeDetourAddress + 0x2C));
    }

    private void TimeBox_OnValueChanged(object sender, RoutedPropertyChangedEventArgs<double?> e)
    {
        if (EnvironmentCheatsInst.TimeDetourAddress == 0)
        {
            return;
        }
        
        GetInstance().WriteMemory(EnvironmentCheatsInst.TimeDetourAddress + 0x24, e.NewValue.GetValueOrDefault());
    }

    private async void TimeSwitch_OnToggled(object sender, RoutedEventArgs e)
    {
        if (sender is not ToggleSwitch toggleSwitch)
        {
            return;
        }
        
        ViewModel.AreManualTimeUiElementsEnabled = false;
        if (EnvironmentCheatsInst.TimeDetourAddress == 0)
        {
            await EnvironmentCheatsInst.CheatTime();
        }
        
        ViewModel.AreManualTimeUiElementsEnabled = true;
        if (EnvironmentCheatsInst.TimeDetourAddress == 0)
        {
            toggleSwitch.Toggled -= TimeSwitch_OnToggled;
            toggleSwitch.IsOn = false;
            toggleSwitch.Toggled += TimeSwitch_OnToggled;
            return;
        }
        
        GetInstance().WriteMemory(EnvironmentCheatsInst.TimeDetourAddress + 0x23, toggleSwitch.IsOn ? (byte)1 : (byte)0);
        GetInstance().WriteMemory(EnvironmentCheatsInst.TimeDetourAddress + 0x24, TimeBox.Value.GetValueOrDefault());
    }

    private async void FreezeAiSwitch_OnToggled(object sender, RoutedEventArgs e)
    {
        if (sender is not ToggleSwitch toggleSwitch)
        {
            return;
        }

        toggleSwitch.IsEnabled = false;
        if (CarCheatsInst.FreezeAiDetourAddress == 0)
        {
            await CarCheatsInst.CheatFreezeAi();
        }
        toggleSwitch.IsEnabled = true;

        if (CarCheatsInst.FreezeAiDetourAddress == 0)
        {
            toggleSwitch.Toggled -= FreezeAiSwitch_OnToggled;
            toggleSwitch.IsOn = false;
            toggleSwitch.Toggled += FreezeAiSwitch_OnToggled;
            return;
        }
        
        GetInstance().WriteMemory(CarCheatsInst.FreezeAiDetourAddress + 0x4F, toggleSwitch.IsOn ? (byte)1 : (byte)0);
    }

    private void NumericUpDown_OnValueChanged(object sender, RoutedPropertyChangedEventArgs<double?> e)
    {
        var color = Picker.SelectedColor.GetValueOrDefault();
        var intensity = e.NewValue.GetValueOrDefault();
        UpdateSunSwatch(color, intensity);

        if (EnvironmentCheatsInst.SunRgbDetourAddress == 0)
        {
            return;
        }
        
        GetInstance().WriteMemory(EnvironmentCheatsInst.SunRgbDetourAddress + 0x33, ConvertUiColorToGameValues(color, intensity));
    }
}
