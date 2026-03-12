using System.Windows;
using System.Windows.Controls;
<<<<<<< C:/Users/GAMING/Documents/GitHub/MA_FH5Trainer/MA_FH5Trainer/MA_FH5Trainer/Views/SubPages/SelfVehicle/PhotoMode.xaml.cs
<<<<<<< C:/Users/GAMING/Documents/GitHub/MA_FH5Trainer/MA_FH5Trainer/MA_FH5Trainer/Views/SubPages/SelfVehicle/PhotoMode.xaml.cs
using XPaint.Cheats.ForzaHorizon5;
=======
using XPaint.Cheats.Core;
>>>>>>> C:/Users/GAMING/.windsurf/worktrees/MA_FH5Trainer/MA_FH5Trainer-90665ca8/MA_FH5Trainer/MA_FH5Trainer/Views/SubPages/SelfVehicle/PhotoMode.xaml.cs
=======
using XPaint.Cheats.Core;
>>>>>>> C:/Users/GAMING/.windsurf/worktrees/MA_FH5Trainer/MA_FH5Trainer-90665ca8/MA_FH5Trainer/MA_FH5Trainer/Views/SubPages/SelfVehicle/PhotoMode.xaml.cs
using XPaint.Models;
using XPaint.ViewModels.SubPages.SelfVehicle;
using XPaint.Views.Windows;
using MahApps.Metro.Controls;
using static XPaint.Resources.Cheats;
using static XPaint.Resources.Memory;

namespace XPaint.Views.SubPages.SelfVehicle;

public partial class PhotoMode
{
    public PhotoMode()
    {
        ViewModel = new PhotoModeViewModel();
        DataContext = this;
        
        InitializeComponent();
    }

    public PhotoModeViewModel ViewModel { get; }
    private static PhotomodeCheats PhotomodeCheatsInst => GetClass<PhotomodeCheats>();

    private async void NoClipSwitch_OnToggled(object sender, RoutedEventArgs e)
    {
        if (sender is not ToggleSwitch toggleSwitch)
        {
            return;
        }

        toggleSwitch.IsEnabled = false;
        if (PhotomodeCheatsInst.NoClipDetourAddress == 0)
        {
            await PhotomodeCheatsInst.CheatNoClip();
        }
        toggleSwitch.IsEnabled = true;

        if (PhotomodeCheatsInst.NoClipDetourAddress == 0)
        {
            toggleSwitch.Toggled -= NoClipSwitch_OnToggled;
            toggleSwitch.IsOn = false;
            toggleSwitch.Toggled += NoClipSwitch_OnToggled;
            return;
        }
                
        GetInstance().WriteMemory(PhotomodeCheatsInst.NoClipDetourAddress + 0x19, toggleSwitch.IsOn ? (byte)1 : (byte)0);
    }

    private async void NoHeightLimitsSwitch_OnToggled(object sender, RoutedEventArgs e)
    {
        if (sender is not ToggleSwitch toggleSwitch)
        {
            return;
        }
        
        toggleSwitch.IsEnabled = false;
        if (PhotomodeCheatsInst.NoHeightLimitDetourAddress == 0)
        {
            await PhotomodeCheatsInst.CheatNoHeightLimits();
        }
        toggleSwitch.IsEnabled = true;

        if (PhotomodeCheatsInst.NoHeightLimitDetourAddress == 0)
        {
            toggleSwitch.Toggled -= NoHeightLimitsSwitch_OnToggled;
            toggleSwitch.IsOn = false;
            toggleSwitch.Toggled += NoHeightLimitsSwitch_OnToggled;
            return;
        }
        
        GetInstance().WriteMemory(PhotomodeCheatsInst.NoHeightLimitDetourAddress + 0x24, toggleSwitch.IsOn ? (byte)1 : (byte)0);
    }

    private async void IncreasedZoomSwitch_OnToggled(object sender, RoutedEventArgs e)
    {
        if (sender is not ToggleSwitch toggleSwitch)
        {
            return;
        }
        
        toggleSwitch.IsEnabled = false;
        if (PhotomodeCheatsInst.IncreasedZoomDetourAddress == 0)
        {
            await PhotomodeCheatsInst.CheatIncreasedZoom();
        }
        toggleSwitch.IsEnabled = true;

        if (PhotomodeCheatsInst.IncreasedZoomDetourAddress == 0)
        {
            toggleSwitch.Toggled -= IncreasedZoomSwitch_OnToggled;
            toggleSwitch.IsOn = false;
            toggleSwitch.Toggled += IncreasedZoomSwitch_OnToggled;
            return;
        }
        
        GetInstance().WriteMemory(PhotomodeCheatsInst.IncreasedZoomDetourAddress + 0x21, toggleSwitch.IsOn ? (byte)1 : (byte)0);
    }

    private async void ModifiersScanButton_OnClick(object sender, RoutedEventArgs e)
    {
        ViewModel.AreScanPromptLimiterUiElementsVisible = false;
        ViewModel.AreScanningLimiterUiElementsVisible = true;

        if (!PhotomodeCheatsInst.WasModifiersScanSuccessful)
        {
            await PhotomodeCheatsInst.CheatModifiers();
        }

        if (!PhotomodeCheatsInst.WasModifiersScanSuccessful)
        {
            ViewModel.AreScanPromptLimiterUiElementsVisible = true;
            ViewModel.AreScanningLimiterUiElementsVisible = false;
            return;
        }
        
        ValueBox.Value = GetInstance().ReadMemory<int>(PhotomodeCheatsInst.MainModifiersAddress);
        ViewModel.AreScanningLimiterUiElementsVisible = false;
        ViewModel.AreModifierUiElementsVisible = true;
    }

    private void Selector_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (sender is not ComboBox comboBox || !PhotomodeCheatsInst.WasModifiersScanSuccessful)
        {
            return;
        }

        ValueBox.Value = comboBox.SelectedIndex switch
        {
            0 => GetInstance().ReadMemory<int>(PhotomodeCheatsInst.MainModifiersAddress),
            1 => GetInstance().ReadMemory<float>(PhotomodeCheatsInst.MainModifiersAddress + 0x20),
            2 => GetInstance().ReadMemory<float>(PhotomodeCheatsInst.MainModifiersAddress + 0x30),
            3 => GetInstance().ReadMemory<float>(PhotomodeCheatsInst.MainModifiersAddress + 0x38),
            4 => GetInstance().ReadMemory<float>(PhotomodeCheatsInst.MainModifiersAddress + 0xC),
            5 => GetInstance().ReadMemory<float>(PhotomodeCheatsInst.SpeedAddress),
            6 => GetInstance().ReadMemory<float>(PhotomodeCheatsInst.SpeedAddress + 0x4),
            _ => ValueBox.Value
        };
    }

    private void ValueBox_OnValueChanged(object sender, RoutedPropertyChangedEventArgs<double?> e)
    {
        var address = GameVerPlat.GetInstance().Type switch
        {
            GameVerPlat.GameType.Default => PhotomodeCheatsInst.MainModifiersAddress,
            _ => throw new IndexOutOfRangeException()
        };

        var speedAddress = GameVerPlat.GetInstance().Type switch
        {
            GameVerPlat.GameType.Default => PhotomodeCheatsInst.SpeedAddress,
            _ => throw new IndexOutOfRangeException()
        };
        
        switch (MainComboBox.SelectedIndex)
        {
            case 0:
            {
                GetInstance().WriteMemory(address, Convert.ToInt32(e.NewValue.GetValueOrDefault()));
                break;
            }
            case 1:
            {
                GetInstance().WriteMemory(address + 0x20, Convert.ToSingle(e.NewValue.GetValueOrDefault()));
                break;
            }
            case 2:
            {
                GetInstance().WriteMemory(address + 0x30, Convert.ToSingle(e.NewValue.GetValueOrDefault()));
                break;
            }
            case 3:
            {
                GetInstance().WriteMemory(address + 0x38, Convert.ToSingle(e.NewValue.GetValueOrDefault()));
                break;
            }
            case 4:
            {
                GetInstance().WriteMemory(address + 0xC, Convert.ToSingle(e.NewValue.GetValueOrDefault()));
                break;
            }
            case 5:
            {
                GetInstance().WriteMemory(speedAddress, Convert.ToSingle(e.NewValue.GetValueOrDefault()));
                break;
            }
            case 6:
            {
                GetInstance().WriteMemory(speedAddress + 0x4, Convert.ToSingle(e.NewValue.GetValueOrDefault()));
                break;
            }
        }
    }
}