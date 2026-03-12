using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
<<<<<<< C:/Users/GAMING/Documents/GitHub/MA_FH5Trainer/MA_FH5Trainer/MA_FH5Trainer/Views/SubPages/SelfVehicle/Handling.xaml.cs
<<<<<<< C:/Users/GAMING/Documents/GitHub/MA_FH5Trainer/MA_FH5Trainer/MA_FH5Trainer/Views/SubPages/SelfVehicle/Handling.xaml.cs
<<<<<<< C:/Users/GAMING/Documents/GitHub/MA_FH5Trainer/MA_FH5Trainer/MA_FH5Trainer/Views/SubPages/SelfVehicle/Handling.xaml.cs
<<<<<<< C:/Users/GAMING/Documents/GitHub/MA_FH5Trainer/MA_FH5Trainer/MA_FH5Trainer/Views/SubPages/SelfVehicle/Handling.xaml.cs
using XPaint.Cheats.ForzaHorizon5;
=======
using XPaint.Cheats.Core;
>>>>>>> C:/Users/GAMING/.windsurf/worktrees/MA_FH5Trainer/MA_FH5Trainer-90665ca8/MA_FH5Trainer/MA_FH5Trainer/Views/SubPages/SelfVehicle/Handling.xaml.cs
=======
using XPaint.Cheats.Core;
>>>>>>> C:/Users/GAMING/.windsurf/worktrees/MA_FH5Trainer/MA_FH5Trainer-90665ca8/MA_FH5Trainer/MA_FH5Trainer/Views/SubPages/SelfVehicle/Handling.xaml.cs
=======
using XPaint.Cheats.Core;
>>>>>>> C:/Users/GAMING/.windsurf/worktrees/MA_FH5Trainer/MA_FH5Trainer-4284e8b3/MA_FH5Trainer/MA_FH5Trainer/Views/SubPages/SelfVehicle/Handling.xaml.cs
=======
using XPaint.Cheats.Core;
>>>>>>> C:/Users/GAMING/.windsurf/worktrees/MA_FH5Trainer/MA_FH5Trainer-4284e8b3/MA_FH5Trainer/MA_FH5Trainer/Views/SubPages/SelfVehicle/Handling.xaml.cs
using XPaint.Resources.Keybinds;
using XPaint.Resources.Theme;
using XPaint.ViewModels.SubPages.SelfVehicle;
using XPaint.ViewModels.Windows;
using XPaint.Views.Windows;
using MahApps.Metro.Controls;
using static XPaint.Resources.Cheats;
using static XPaint.Resources.Memory;

namespace XPaint.Views.SubPages.SelfVehicle;

public partial class Handling
{
    private readonly GlobalHotkey _jumpHackHotkey = new("Jump Hack", ModifierKeys.None, Key.None, JumpHackCallback, 1000);
    private readonly GlobalHotkey _brakeHackHotkey = new("Super Brake", ModifierKeys.None, Key.None, BrakeHackCallback, 1);
    private readonly GlobalHotkey _velocityHotkey = new("Velocity", ModifierKeys.None, Key.Q, VelocityCallback, 1);
    private readonly GlobalHotkey _wheelspeedHotkey = new("Wheelspeed", ModifierKeys.None, Key.None, WheelspeedCallback, 1);

    private static void JumpHackCallback()
    {
        var address = CarCheatsInst.LocalPlayerHookDetourAddress;
        if (address <= UIntPtr.Zero)
        {
            return;
        }
        
        GetInstance().WriteMemory(address + CarCheatsOffsets.JumpHackEnabled, (byte)1);
    }
    
    private static void BrakeHackCallback()
    {
        var address = CarCheatsInst.LocalPlayerHookDetourAddress;
        if (address <= UIntPtr.Zero)
        {
            return;
        }

        GetInstance().WriteMemory(address + CarCheatsOffsets.BrakeHackEnabled, (byte)1);
    }
    
    private static void VelocityCallback()
    {
        var address = CarCheatsInst.LocalPlayerHookDetourAddress;
        if (address <= UIntPtr.Zero)
        {
            return;
        }

        GetInstance().WriteMemory(address + CarCheatsOffsets.VelEnabled, (byte)1);
    }

    private static void WheelspeedCallback()
    {
        var address = CarCheatsInst.LocalPlayerHookDetourAddress;
        if (address <= UIntPtr.Zero)
        {
            return;
        }

        GetInstance().WriteMemory(address + CarCheatsOffsets.WheelspeedEnabled, (byte)1);
    }

    public Handling()
    {
        ViewModel = new HandlingViewModel();
        DataContext = this;
        
        InitializeComponent();
        HotkeysManager.Register(_jumpHackHotkey);
        HotkeysManager.Register(_brakeHackHotkey);
        HotkeysManager.Register(_velocityHotkey);
        HotkeysManager.Register(_wheelspeedHotkey);

        if (MainWindow.Instance != null)
        {
            var viewModel = MainWindow.Instance.ViewModel;
            viewModel.Hotkeys.Add(_velocityHotkey);
            viewModel.Hotkeys.Add(_wheelspeedHotkey);
            viewModel.Hotkeys.Add(_jumpHackHotkey);
            viewModel.Hotkeys.Add(_brakeHackHotkey);
        }
    }

    private static CarCheats CarCheatsInst => GetClass<CarCheats>();
    public HandlingViewModel ViewModel { get; }

    private async void ModifierToggleSwitch_OnToggled(object sender, RoutedEventArgs e)
    {
        if (sender is not ToggleSwitch toggleSwitch)
        {
            return;
        }

        ViewModel.AreUiElementsEnabled = false;
        if (CarCheatsInst.AccelDetourAddress == 0)
        {
            await CarCheatsInst.CheatAccel();
        }
        ViewModel.AreUiElementsEnabled = true;

        if (CarCheatsInst.AccelDetourAddress <= 0)
        {
            toggleSwitch.Toggled -= ModifierToggleSwitch_OnToggled;
            toggleSwitch.IsOn = false;
            toggleSwitch.Toggled -= ModifierToggleSwitch_OnToggled;
            return;
        }
        
        GetInstance().WriteMemory(CarCheatsInst.AccelDetourAddress + 0x58, toggleSwitch.IsOn ? (byte)1 : (byte)0);
        GetInstance().WriteMemory(CarCheatsInst.AccelDetourAddress + 0x59, Convert.ToSingle(AccelSlider.Value) / 100);  
        ViewModel.IsAccelEnabled = toggleSwitch.IsOn;
    }
    
    private void ModifierValueBox_OnValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
    {
        ViewModel.AccelValue = Convert.ToDouble(e.NewValue);
        if (CarCheatsInst.AccelDetourAddress <= UIntPtr.Zero)
        {
            return;
        }
        
        GetInstance().WriteMemory(CarCheatsInst.AccelDetourAddress + 0x59, Convert.ToSingle(e.NewValue) / 100);  
    }

    private async void VelocitySwitch_OnToggled(object sender, RoutedEventArgs e)
    {
        if (sender is not ToggleSwitch toggleSwitch)
        {
            return;
        }

        ViewModel.AreUiElementsEnabled = false;

        if (CarCheatsInst.LocalPlayerHookDetourAddress == 0)
        {
            await CarCheatsInst.CheatLocalPlayer();
        }
        ViewModel.AreUiElementsEnabled = true;

        if (CarCheatsInst.LocalPlayerHookDetourAddress <= UIntPtr.Zero)
        {
            toggleSwitch.Toggled -= VelocitySwitch_OnToggled;
            toggleSwitch.IsOn = false;
            toggleSwitch.Toggled -= VelocitySwitch_OnToggled;
            return;
        }
        
        GetInstance().WriteMemory(CarCheatsInst.LocalPlayerHookDetourAddress + CarCheatsOffsets.VelBoost, 1f + Convert.ToSingle(VelocityStrength.Value / 1000));  
        GetInstance().WriteMemory(CarCheatsInst.LocalPlayerHookDetourAddress + CarCheatsOffsets.VelLimit, Convert.ToSingle(VelocityLimit.Value));
        _velocityHotkey.CanExecute = toggleSwitch.IsOn;
    }

    private void VelocityStrength_OnValueChanged(object sender, RoutedPropertyChangedEventArgs<double?> e)
    {
        if (CarCheatsInst.LocalPlayerHookDetourAddress <= UIntPtr.Zero) return;
        GetInstance().WriteMemory(CarCheatsInst.LocalPlayerHookDetourAddress + CarCheatsOffsets.VelBoost, 1f + Convert.ToSingle(VelocityStrength.Value / 1000));  
    }

    private void VelocityLimit_OnValueChanged(object sender, RoutedPropertyChangedEventArgs<double?> e)
    {
        if (CarCheatsInst.LocalPlayerHookDetourAddress <= UIntPtr.Zero) return;
        GetInstance().WriteMemory(CarCheatsInst.LocalPlayerHookDetourAddress + CarCheatsOffsets.VelLimit, Convert.ToSingle(VelocityLimit.Value));  
    }

    private async void WheelspeedSwitch_OnToggled(object sender, RoutedEventArgs e)
    {
        if (sender is not ToggleSwitch toggleSwitch)
        {
            return;
        }

        ViewModel.AreUiElementsEnabled = false;

        if (CarCheatsInst.LocalPlayerHookDetourAddress == 0)
        {
            await CarCheatsInst.CheatLocalPlayer();
        }
        ViewModel.AreUiElementsEnabled = true;

        if (CarCheatsInst.LocalPlayerHookDetourAddress <= UIntPtr.Zero)
        {
            toggleSwitch.Toggled -= WheelspeedSwitch_OnToggled;
            toggleSwitch.IsOn = false;
            toggleSwitch.Toggled -= WheelspeedSwitch_OnToggled;
            return;
        }
        
        GetInstance().WriteMemory(CarCheatsInst.LocalPlayerHookDetourAddress + CarCheatsOffsets.WheelspeedMode, (byte)WheelspeedModeBox.SelectedIndex);  
        GetInstance().WriteMemory(CarCheatsInst.LocalPlayerHookDetourAddress + CarCheatsOffsets.WheelspeedBoost, Convert.ToSingle(WheelspeedValueBox.Value));
        GetInstance().WriteMemory(CarCheatsInst.LocalPlayerHookDetourAddress + CarCheatsOffsets.WheelspeedLimit, Convert.ToSingle(WheelspeedLimit.Value));  
        _wheelspeedHotkey.CanExecute = toggleSwitch.IsOn;
    }

    private void WheelspeedNum_OnValueChanged(object sender, RoutedPropertyChangedEventArgs<double?> e)
    {
        if (CarCheatsInst.LocalPlayerHookDetourAddress <= UIntPtr.Zero) return;
        GetInstance().WriteMemory(CarCheatsInst.LocalPlayerHookDetourAddress + CarCheatsOffsets.WheelspeedBoost, Convert.ToSingle(WheelspeedValueBox.Value));  
    }

    private void WheelspeedModeBox_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (CarCheatsInst.LocalPlayerHookDetourAddress <= UIntPtr.Zero) return;
        GetInstance().WriteMemory(CarCheatsInst.LocalPlayerHookDetourAddress + CarCheatsOffsets.WheelspeedMode, (byte)WheelspeedModeBox.SelectedIndex);   
    }

    private void JumpSlider_OnValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
    {
        if (sender is Slider slider)
        {
            slider.Value = (Math.Round(e.NewValue, 2));
        }
        
        if (CarCheatsInst.LocalPlayerHookDetourAddress <= UIntPtr.Zero)
        {
            return;
        }
        
        GetInstance().WriteMemory(CarCheatsInst.LocalPlayerHookDetourAddress + CarCheatsOffsets.JumpHackBoost, Convert.ToSingle(e.NewValue));   
    }

    private async void JumpSwitch_OnToggled(object sender, RoutedEventArgs e)
    {
        if (sender is not ToggleSwitch toggleSwitch)
        {
            return;
        }
        
        ViewModel.AreUiElementsEnabled = false;

        if (CarCheatsInst.LocalPlayerHookDetourAddress == 0)
        {
            await CarCheatsInst.CheatLocalPlayer();
        }
        ViewModel.AreUiElementsEnabled = true;

        if (CarCheatsInst.LocalPlayerHookDetourAddress <= UIntPtr.Zero)
        {
            toggleSwitch.Toggled -= JumpSwitch_OnToggled;
            toggleSwitch.IsOn = false;
            toggleSwitch.Toggled -= JumpSwitch_OnToggled;
            return;
        } 
        GetInstance().WriteMemory(CarCheatsInst.LocalPlayerHookDetourAddress + CarCheatsOffsets.JumpHackBoost, Convert.ToSingle(JumpSlider.Value));
        _jumpHackHotkey.CanExecute = toggleSwitch.IsOn;
    }

    private void StopSlider_OnValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
    {
        if (CarCheatsInst.LocalPlayerHookDetourAddress <= UIntPtr.Zero) return;
        GetInstance().WriteMemory(CarCheatsInst.LocalPlayerHookDetourAddress + CarCheatsOffsets.BrakeHackBoost, 1f - Convert.ToSingle(e.NewValue) / 100);   
    }

    private async void StopSwitch_OnToggled(object sender, RoutedEventArgs e)
    {
        if (sender is not ToggleSwitch toggleSwitch)
        {
            return;
        }
        
        ViewModel.AreUiElementsEnabled = false;
        if (CarCheatsInst.LocalPlayerHookDetourAddress == 0)
        {
            await CarCheatsInst.CheatLocalPlayer();
        }
        ViewModel.AreUiElementsEnabled = true;

        if (CarCheatsInst.LocalPlayerHookDetourAddress <= UIntPtr.Zero)
        {
            toggleSwitch.Toggled -= StopSwitch_OnToggled;
            toggleSwitch.IsOn = false;
            toggleSwitch.Toggled += StopSwitch_OnToggled;
            return;
        }
        
        GetInstance().WriteMemory(CarCheatsInst.LocalPlayerHookDetourAddress + CarCheatsOffsets.BrakeHackBoost, 1f - Convert.ToSingle(StopSlider.Value) / 100);
        _brakeHackHotkey.CanExecute = toggleSwitch.IsOn;
    }

    private async void NoWaterDragSwitch_OnToggled(object sender, RoutedEventArgs e)
    {        
        if (sender is not ToggleSwitch toggleSwitch)
        {
            return;
        }

        toggleSwitch.IsEnabled = false;
        if (CarCheatsInst.NoWaterDragDetourAddress == 0)
        {
            await CarCheatsInst.CheatNoWaterDrag();
        }
        toggleSwitch.IsEnabled = true;

        if (CarCheatsInst.NoWaterDragDetourAddress == 0)
        {
            toggleSwitch.Toggled -= NoWaterDragSwitch_OnToggled;
            toggleSwitch.IsOn = false;
            toggleSwitch.Toggled += NoWaterDragSwitch_OnToggled;
            return;
        }
        GetInstance().WriteMemory(CarCheatsInst.NoWaterDragDetourAddress + 0x17, toggleSwitch.IsOn ? (byte)1 : (byte)0);
    }

    private async void NoClipSwitch_OnToggled(object sender, RoutedEventArgs e)
    {
        if (sender is not ToggleSwitch toggleSwitch)
        {
            return;
        }

        ViewModel.AreUiElementsEnabled = false;
        if (CarCheatsInst.NoClipDetourAddress == 0)
        {
            await CarCheatsInst.CheatNoClip();
        }
        ViewModel.AreUiElementsEnabled = true;

        if (CarCheatsInst.NoClipDetourAddress == 0)
        {
            toggleSwitch.Toggled -= NoClipSwitch_OnToggled;
            toggleSwitch.IsOn = false;
            toggleSwitch.Toggled += NoClipSwitch_OnToggled;
            return;
        }
        GetInstance().WriteMemory(CarCheatsInst.NoClipDetourAddress + 0x31, toggleSwitch.IsOn ? (byte)1 : (byte)0);
    }
    
    private void WheelspeedLimit_OnValueChanged(object sender, RoutedPropertyChangedEventArgs<double?> e)
    {
        if (CarCheatsInst.LocalPlayerHookDetourAddress <= UIntPtr.Zero)
        {
            return;
        }
        
        GetInstance().WriteMemory(CarCheatsInst.LocalPlayerHookDetourAddress + CarCheatsOffsets.WheelspeedLimit, Convert.ToSingle(e.NewValue));  
    }
    private async void GravToggleSwitch_OnToggled(object sender, RoutedEventArgs e)
    {                
        if (sender is not ToggleSwitch toggleSwitch)
        {
            return;
        }

        ViewModel.AreUiElementsEnabled = false;
        if (CarCheatsInst.GravityDetourAddress == 0)
        {
            await CarCheatsInst.CheatGravity();
        }
        ViewModel.AreUiElementsEnabled = true;

        if (CarCheatsInst.GravityDetourAddress <= 0)
        {
            toggleSwitch.Toggled -= GravToggleSwitch_OnToggled;
            toggleSwitch.IsOn = false;
            toggleSwitch.Toggled -= GravToggleSwitch_OnToggled;
            return;
        }
        
        GetInstance().WriteMemory(CarCheatsInst.GravityDetourAddress + 0x59, toggleSwitch.IsOn ? (byte)1 : (byte)0);
        GetInstance().WriteMemory(CarCheatsInst.GravityDetourAddress + 0x5A,Convert.ToSingle(GravValueBox.Value) / 100);   
        ViewModel.IsGravityEnabled = toggleSwitch.IsOn;
    }

    private void GravValueBox_OnValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
    {
        ViewModel.GravityValue = Convert.ToDouble(e.NewValue);
        if (CarCheatsInst.GravityDetourAddress <= UIntPtr.Zero)
        {
            return;
        }
        
        GetInstance().WriteMemory(CarCheatsInst.GravityDetourAddress + 0x5A, Convert.ToSingle(e.NewValue) / 100);   
    }
}