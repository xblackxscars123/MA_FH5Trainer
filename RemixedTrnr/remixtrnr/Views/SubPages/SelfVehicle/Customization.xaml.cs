using System.Numerics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;
using XPaint.Cheats.Core;
using XPaint.Models;
using XPaint.ViewModels.SubPages.SelfVehicle;
using XPaint.Views.Windows;
using MahApps.Metro.Controls;
using static XPaint.Resources.Cheats;
using static XPaint.Resources.Memory;

namespace XPaint.Views.SubPages.SelfVehicle;

public partial class Customization
{
    public Customization()
    {
        MainWindow = MainWindow.Instance ?? new MainWindow();
        ViewModel = new CustomizationViewModel();
        DataContext = this;
        
        InitializeComponent();
    }
    
    public MainWindow MainWindow { get; }
    public CustomizationViewModel ViewModel { get; }
    private static CustomizationCheats CustomizationCheatsInst => GetClass<CustomizationCheats>();
#if false
    private async void MainSwitch_OnToggled(object sender, RoutedEventArgs e)
    {
        if (sender is not ToggleSwitch toggleSwitch || !MainWindow.ViewModel.Attached)
        {
            return;
        }

        ViewModel.AreMainUiElementsEnabled = false;
        await EnableCheats(toggleSwitch);
        ViewModel.AreMainUiElementsEnabled = true;
    }

    private async Task EnableCheats(ToggleSwitch toggleSwitch)
    {
        switch (MainComboBox.SelectedIndex)
        {
            case 0:
            {
                if (CustomizationCheatsInst.CleanlinessDetourAddress == 0)
                {
                    await CustomizationCheatsInst.CheatCleanliness();
                }

                if (CustomizationCheatsInst.CleanlinessDetourAddress == 0)
                {
                    toggleSwitch.Toggled -= MainSwitch_OnToggled;
                    toggleSwitch.IsOn = false;
                    toggleSwitch.Toggled += MainSwitch_OnToggled;
                    break;
                }
                
                ViewModel.DirtEnabled = toggleSwitch.IsOn;
                GetInstance().WriteMemory(CustomizationCheatsInst.CleanlinessDetourAddress + 0x37, toggleSwitch.IsOn ? (byte)1 : (byte)0);
                GetInstance().WriteMemory(CustomizationCheatsInst.CleanlinessDetourAddress + 0x38, Convert.ToSingle(MainSlider.Value));
                break;
            }
            case 1:
            {
                if (CustomizationCheatsInst.CleanlinessDetourAddress == 0)
                {
                    await CustomizationCheatsInst.CheatCleanliness();
                }

                if (CustomizationCheatsInst.CleanlinessDetourAddress == 0)
                {
                    toggleSwitch.Toggled -= MainSwitch_OnToggled;
                    toggleSwitch.IsOn = false;
                    toggleSwitch.Toggled += MainSwitch_OnToggled;
                    break;
                }   
                
                ViewModel.MudEnabled = toggleSwitch.IsOn;
                GetInstance().WriteMemory(CustomizationCheatsInst.CleanlinessDetourAddress + 0x3C, toggleSwitch.IsOn ? (byte)1 : (byte)0);
                GetInstance().WriteMemory(CustomizationCheatsInst.CleanlinessDetourAddress + 0x3D, Convert.ToSingle(MainSlider.Value));
                break;
            }
        } 
    }
#endif
    private async void HeadlightSwitch_OnToggled(object sender, RoutedEventArgs e)
    {
        if (sender is not ToggleSwitch toggleSwitch)
        {
            return;
        }

        toggleSwitch.IsEnabled = false;
        if (CustomizationCheatsInst.HeadlightColourDetourAddress == 0)
        {
            await CustomizationCheatsInst.CheatHeadlightColour();
        }
        toggleSwitch.IsEnabled = true;

        if (CustomizationCheatsInst.HeadlightColourDetourAddress == 0)
        {
            toggleSwitch.Toggled -= HeadlightSwitch_OnToggled;
            toggleSwitch.IsOn = false;
            toggleSwitch.Toggled += HeadlightSwitch_OnToggled;
            return;
        }
        
        ViewModel.AreHeadlightUiElementsEnabled = toggleSwitch.IsOn;
        var toggled = toggleSwitch.IsOn ? (byte)1 : (byte)0;
        var write = ConvertUiColorToGameValues((Color)ColorPicker.SelectedColor!);
        GetInstance().WriteMemory(CustomizationCheatsInst.HeadlightColourDetourAddress + 0x22, toggled);
        GetInstance().WriteMemory(CustomizationCheatsInst.HeadlightColourDetourAddress + 0x23, write);
    }
    
    private void ColorPickerBase_OnSelectedColorChanged(object sender, RoutedPropertyChangedEventArgs<Color?> e)
    {
        if (ColorPicker == null)
        {
            return;
        }

        var color = e.NewValue.GetValueOrDefault();
        UpdateHeadlightSwatch(color);

        if (CustomizationCheatsInst.HeadlightColourDetourAddress == 0)
        {
            return;
        }
        
        var write = ConvertUiColorToGameValues(color);
        GetInstance().WriteMemory(CustomizationCheatsInst.HeadlightColourDetourAddress + 0x23, write);
    }

    private void UpdateHeadlightSwatch(Color c)
    {
        var v = ConvertUiColorToGameValues(c);
        HeadlightSwatch.Background = new SolidColorBrush(Color.FromRgb(
            (byte)Math.Clamp((int)MathF.Round(v.X * 255), 0, 255),
            (byte)Math.Clamp((int)MathF.Round(v.Y * 255), 0, 255),
            (byte)Math.Clamp((int)MathF.Round(v.Z * 255), 0, 255)));
    }

    private static Vector3 ConvertUiColorToGameValues(Color uiColor)
    {
        var alpha = uiColor.A / 255f;
        var red = uiColor.R / 255f * alpha;
        var green = uiColor.G / 255f * alpha;
        var blue = uiColor.B / 255f * alpha;
        return new Vector3(red, green, blue);        
    }
#if false
    private void MainSlider_OnValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
    {
        switch (MainComboBox.SelectedIndex)
        {
            case 0:
            {
                ViewModel.DirtValue = Convert.ToSingle(e.NewValue);
                if (CustomizationCheatsInst.CleanlinessDetourAddress == 0)
                {
                    return;
                }
                
                GetInstance().WriteMemory(CustomizationCheatsInst.CleanlinessDetourAddress + 0x38, ViewModel.DirtValue);
                break;
            }
            case 1:
            {
                ViewModel.MudValue = Convert.ToSingle(e.NewValue);
                if (CustomizationCheatsInst.CleanlinessDetourAddress == 0)
                {
                    return;
                }
                
                GetInstance().WriteMemory(CustomizationCheatsInst.CleanlinessDetourAddress + 0x3D, ViewModel.MudValue);
                break;
            }
        }
    }

    private void MainComboBox_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (sender is not ComboBox comboBox || MainSwitch == null)
        {
            return;
        }

        MainSwitch.Toggled -= MainSwitch_OnToggled;
        
        MainSwitch.IsOn = comboBox.SelectedIndex switch
        {
            0 => ViewModel.GlowingPaintEnabled,
            1 => ViewModel.DirtEnabled,
            2 => ViewModel.MudEnabled,
            3 => ViewModel.ForceLodEnabled,
            _ => throw new IndexOutOfRangeException()
        };
        
        MainSwitch.Toggled += MainSwitch_OnToggled;
        
        MainSlider.Value = comboBox.SelectedIndex switch
        {
            0 => ViewModel.GlowingPaintValue,
            1 => ViewModel.DirtValue,
            2 => ViewModel.MudValue,
            3 => ViewModel.ForceLodValue,
            _ => throw new IndexOutOfRangeException()
        };
        
        MainSlider.Maximum = comboBox.SelectedIndex switch
        {
            0 => 100,
            1 or 2 => 1,
            3 => 4,
            _ => throw new IndexOutOfRangeException()
        };

        MainSlider.TickPlacement = comboBox.SelectedIndex == 3 ? TickPlacement.BottomRight : TickPlacement.None;
        MainSlider.IsSnapToTickEnabled = comboBox.SelectedIndex == 3;
    }
#endif
    private async void BackfireSwitch_OnToggled(object sender, RoutedEventArgs e)
    {
        if (sender is not ToggleSwitch toggleSwitch)
        {
            return;
        }

        toggleSwitch.IsEnabled = false;
        if (CustomizationCheatsInst.BackfireTimeDetourAddress == 0)
        {
            await CustomizationCheatsInst.CheatBackfireTime();
        }
        toggleSwitch.IsEnabled = true;

        if (CustomizationCheatsInst.BackfireTimeDetourAddress == 0)
        {
            toggleSwitch.Toggled -= BackfireSwitch_OnToggled;
            toggleSwitch.IsOn = false;
            toggleSwitch.Toggled += BackfireSwitch_OnToggled;
            return;
        }
        
        ViewModel.AreBackfireUiElementsEnabled = toggleSwitch.IsOn;
        GetInstance().WriteMemory(CustomizationCheatsInst.BackfireTimeDetourAddress + 0x26, toggleSwitch.IsOn ? (byte)1 : (byte)0);
        GetInstance().WriteMemory(CustomizationCheatsInst.BackfireTimeDetourAddress + 0x27, Convert.ToSingle(MinBackfire.Value));
        GetInstance().WriteMemory(CustomizationCheatsInst.BackfireTimeDetourAddress + 0x2B, Convert.ToSingle(MaxBackfire.Value));
    }

    private void MinBackfire_OnValueChanged(object sender, RoutedPropertyChangedEventArgs<double?> e)
    {
        if (CustomizationCheatsInst.BackfireTimeDetourAddress == 0)
        {
            return;
        }
        
        GetInstance().WriteMemory(CustomizationCheatsInst.BackfireTimeDetourAddress + 0x27, Convert.ToSingle(e.NewValue));
    }

    private void MaxBackfire_OnValueChanged(object sender, RoutedPropertyChangedEventArgs<double?> e)
    {
        if (CustomizationCheatsInst.BackfireTimeDetourAddress == 0)
        {
            return;
        }
        
        GetInstance().WriteMemory(CustomizationCheatsInst.BackfireTimeDetourAddress + 0x2B, Convert.ToSingle(e.NewValue));
    }

    private void PaintSlider_OnValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
    {
        if (sender is not Slider slider)
        {
            return;
        }

        slider.Value = Math.Round(e.NewValue, 2);
        ViewModel.GlowingPaintValue = Convert.ToSingle(e.NewValue);
        if (CustomizationCheatsInst.PaintDetourAddress == 0)
        {
            return;
        }
                
        GetInstance().WriteMemory(CustomizationCheatsInst.PaintDetourAddress + 0x37, ViewModel.GlowingPaintValue);
    }

    private async void PaintSwitchOnToggled(object sender, RoutedEventArgs e)
    {
        if (sender is not ToggleSwitch toggleSwitch)
        {
            return;
        }

        toggleSwitch.IsEnabled = false;
        if (CustomizationCheatsInst.PaintDetourAddress == 0)
        {
            await CustomizationCheatsInst.CheatGlowingPaint();
        }
        toggleSwitch.IsEnabled = true;

        if (CustomizationCheatsInst.PaintDetourAddress == 0)
        {
            toggleSwitch.Toggled -= PaintSwitchOnToggled;
            toggleSwitch.IsOn = false;
            toggleSwitch.Toggled += PaintSwitchOnToggled;
            return;
        }
                
        ViewModel.GlowingPaintEnabled = toggleSwitch.IsOn;
        var write = toggleSwitch.IsOn ? (byte)1 : (byte)0;
        GetInstance().WriteMemory(CustomizationCheatsInst.PaintDetourAddress + 0x36, write);
        GetInstance().WriteMemory(CustomizationCheatsInst.PaintDetourAddress + 0x37, ViewModel.GlowingPaintValue);
    }
}
