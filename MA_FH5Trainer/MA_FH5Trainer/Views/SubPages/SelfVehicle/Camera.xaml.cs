using System.Numerics;
using System.Windows;
using System.Windows.Controls;
<<<<<<< C:/Users/GAMING/Documents/GitHub/MA_FH5Trainer/MA_FH5Trainer/MA_FH5Trainer/Views/SubPages/SelfVehicle/Camera.xaml.cs
<<<<<<< C:/Users/GAMING/Documents/GitHub/MA_FH5Trainer/MA_FH5Trainer/MA_FH5Trainer/Views/SubPages/SelfVehicle/Camera.xaml.cs
using XPaint.Cheats.ForzaHorizon5;
=======
using XPaint.Cheats.Core;
>>>>>>> C:/Users/GAMING/.windsurf/worktrees/MA_FH5Trainer/MA_FH5Trainer-90665ca8/MA_FH5Trainer/MA_FH5Trainer/Views/SubPages/SelfVehicle/Camera.xaml.cs
=======
using XPaint.Cheats.Core;
>>>>>>> C:/Users/GAMING/.windsurf/worktrees/MA_FH5Trainer/MA_FH5Trainer-90665ca8/MA_FH5Trainer/MA_FH5Trainer/Views/SubPages/SelfVehicle/Camera.xaml.cs
using XPaint.Models;
using XPaint.ViewModels.SubPages.SelfVehicle;
using XPaint.Views.Windows;
using MahApps.Metro.Controls;
using static XPaint.Resources.Memory;

namespace XPaint.Views.SubPages.SelfVehicle;

public partial class Camera
{
    public Camera()
    {
        MainWindow = MainWindow.Instance ?? new MainWindow();
        ViewModel = new CameraViewModel();
        DataContext = this;
        
        InitializeComponent();
    }
    
    public MainWindow MainWindow { get; }
    public CameraViewModel ViewModel { get; }
<<<<<<< C:/Users/GAMING/Documents/GitHub/MA_FH5Trainer/MA_FH5Trainer/MA_FH5Trainer/Views/SubPages/SelfVehicle/Camera.xaml.cs
<<<<<<< C:/Users/GAMING/Documents/GitHub/MA_FH5Trainer/MA_FH5Trainer/MA_FH5Trainer/Views/SubPages/SelfVehicle/Camera.xaml.cs
    private static CameraCheats CameraCheatsFh5 => XPaint.Resources.Cheats.GetClass<CameraCheats>();
=======
    private static CameraCheats CameraCheatsInst => XPaint.Resources.Cheats.GetClass<CameraCheats>();
>>>>>>> C:/Users/GAMING/.windsurf/worktrees/MA_FH5Trainer/MA_FH5Trainer-90665ca8/MA_FH5Trainer/MA_FH5Trainer/Views/SubPages/SelfVehicle/Camera.xaml.cs
=======
    private static CameraCheats CameraCheatsInst => XPaint.Resources.Cheats.GetClass<CameraCheats>();
>>>>>>> C:/Users/GAMING/.windsurf/worktrees/MA_FH5Trainer/MA_FH5Trainer-90665ca8/MA_FH5Trainer/MA_FH5Trainer/Views/SubPages/SelfVehicle/Camera.xaml.cs

    private async void LimitersScanButton_OnClick(object sender, RoutedEventArgs e)
    {
        ViewModel.AreScanPromptLimiterUiElementsVisible = false;
        ViewModel.AreScanningLimiterUiElementsVisible = true;

        if (!CameraCheatsInst.WereLimitersScanned)
        {
            await CameraCheatsInst.CheatLimiters();
        }

        if (!CameraCheatsInst.WereLimitersScanned)
        {
            ViewModel.AreScanPromptLimiterUiElementsVisible = true;
            ViewModel.AreScanningLimiterUiElementsVisible = false;
            return;
        }
        
        LimiterMinBox.Value = GetInstance().ReadMemory<float>(CameraCheatsInst.ChaseAddress);
        LimiterMaxBox.Value = GetInstance().ReadMemory<float>(CameraCheatsInst.ChaseAddress + 4);
        
        ViewModel.AreScanningLimiterUiElementsVisible = false;
        ViewModel.AreLimiterUiElementsVisible = true;
    }

    private void LimiterComboBox_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        var successful = CameraCheatsInst.WereLimitersScanned;
        if (sender is not ComboBox comboBox || !successful)
        {
            return;
        }
        
        LimiterMinBox.Value = comboBox.SelectedIndex switch
        {
            0 => GetInstance().ReadMemory<float>(CameraCheatsInst.ChaseAddress),
            1 => GetInstance().ReadMemory<float>(CameraCheatsInst.ChaseFarAddress),
            2 => GetInstance().ReadMemory<float>(CameraCheatsInst.DriverAddress),
            3 => GetInstance().ReadMemory<float>(CameraCheatsInst.HoodAddress),
            4 => GetInstance().ReadMemory<float>(CameraCheatsInst.BumperAddress),
            _ => 0
        };
            
        LimiterMaxBox.Value = comboBox.SelectedIndex switch
        {
            0 => GetInstance().ReadMemory<float>(CameraCheatsInst.ChaseAddress + 4),
            1 => GetInstance().ReadMemory<float>(CameraCheatsInst.ChaseFarAddress + 4),
            2 => GetInstance().ReadMemory<float>(CameraCheatsInst.DriverAddress + 4),
            3 => GetInstance().ReadMemory<float>(CameraCheatsInst.HoodAddress + 4),
            4 => GetInstance().ReadMemory<float>(CameraCheatsInst.BumperAddress + 4),
            _ => 0
        };
    }

    private void LimiterMinBox_OnValueChanged(object sender, RoutedPropertyChangedEventArgs<double?> e)
    {
        if (sender is not NumericUpDown numericUpDown || !CameraCheatsInst.WereLimitersScanned)
        {
            return;
        }

        var newValue = Convert.ToSingle(numericUpDown.Value);
        switch (LimiterComboBox.SelectedIndex)
        {
            case 0:
            {
                GetInstance().WriteMemory(CameraCheatsInst.ChaseAddress, newValue);
                break;
            }
            case 1:
            {
                GetInstance().WriteMemory(CameraCheatsInst.ChaseFarAddress, newValue);
                break;
            }
            case 2:
            {
                GetInstance().WriteMemory(CameraCheatsInst.DriverAddress, newValue);
                break;
            }
            case 3:
            {
                GetInstance().WriteMemory(CameraCheatsInst.HoodAddress, newValue);
                break;
            }
            case 4:
            {
                GetInstance().WriteMemory(CameraCheatsInst.BumperAddress, newValue);
                break;
            }
        }
    }

    private void LimiterMaxBox_OnValueChanged(object sender, RoutedPropertyChangedEventArgs<double?> e)
    {
        if (sender is not NumericUpDown numericUpDown || !CameraCheatsInst.WereLimitersScanned)
        {
            return;
        }

        var newValue = Convert.ToSingle(numericUpDown.Value);
        switch (LimiterComboBox.SelectedIndex)
        {
            case 0:
            {
                GetInstance().WriteMemory(CameraCheatsInst.ChaseAddress + 4, newValue);
                break;
            }
            case 1:
            {
                GetInstance().WriteMemory(CameraCheatsInst.ChaseFarAddress + 4, newValue);
                break;
            }
            case 2:
            {
                GetInstance().WriteMemory(CameraCheatsInst.DriverAddress + 4, newValue);
                break;
            }
            case 3:
            {
                GetInstance().WriteMemory(CameraCheatsInst.HoodAddress + 4, newValue);
                break;
            }
            case 4:
            {
                GetInstance().WriteMemory(CameraCheatsInst.BumperAddress + 4, newValue);
                break;
            }
        }
    }

    private async void FovLockSwitch_OnToggled(object sender, RoutedEventArgs e)
    {
        if (sender is not ToggleSwitch toggleSwitch)
        {
            return;
        }

        OffsetSwitch.IsEnabled = false;
        toggleSwitch.IsEnabled = false;
        if (CameraCheatsInst.CameraDetourAddress == 0)
        {
            await CameraCheatsInst.CheatCamera();
        }
        toggleSwitch.IsEnabled = true;
        OffsetSwitch.IsEnabled = true;

        if (CameraCheatsInst.CameraDetourAddress == 0)
        {
            toggleSwitch.Toggled -= FovLockSwitch_OnToggled;
            toggleSwitch.IsOn = false;
            toggleSwitch.Toggled += FovLockSwitch_OnToggled;
            return;
        }
        
        ViewModel.AreCameraHookUiElementsEnabled = toggleSwitch.IsOn;
        GetInstance().WriteMemory(CameraCheatsInst.CameraDetourAddress + 0x59, toggleSwitch.IsOn ? (byte)1 : (byte)0);
        GetInstance().WriteMemory(CameraCheatsInst.CameraDetourAddress + 0x5A, Convert.ToSingle(FovLockSlider.Value) / 10);
    }

    private void FovLockSlider_OnValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
    {
        if (sender is not Slider slider)
        {
            return;
        }

        slider.Value = Math.Round(e.NewValue, 2);
        if (CameraCheatsInst.CameraDetourAddress == 0)
        {
            return;
        }
        
        GetInstance().WriteMemory(CameraCheatsInst.CameraDetourAddress + 0x5A, Convert.ToSingle(slider.Value) / 10);
    }

    private async void OffsetSwitch_OnToggled(object sender, RoutedEventArgs e)
    {
        if (sender is not ToggleSwitch toggleSwitch)
        {
            return;
        }

        FovSwitch.IsEnabled = false;
        toggleSwitch.IsEnabled = false;
        if (CameraCheatsInst.CameraDetourAddress == 0)
        {
            await CameraCheatsInst.CheatCamera();
        }
        toggleSwitch.IsEnabled = true;
        FovSwitch.IsEnabled = true;

        if (CameraCheatsInst.CameraDetourAddress == 0)
        {
            toggleSwitch.Toggled -= OffsetSwitch_OnToggled;
            toggleSwitch.IsOn = false;
            toggleSwitch.Toggled += OffsetSwitch_OnToggled;
            return;
        }
        
        ViewModel.AreCameraOffsetUiElementsEnabled = toggleSwitch.IsOn;
        GetInstance().WriteMemory(CameraCheatsInst.CameraDetourAddress + 0x5E, toggleSwitch.IsOn ? (byte)1 : (byte)0);

        var write = new Vector3(
            Convert.ToSingle(XValueBox.Value),
            Convert.ToSingle(YValueBox.Value),
            0
        );
        
        GetInstance().WriteMemory(CameraCheatsInst.CameraDetourAddress + 0x5F, write);
    }

    private void OffsetBoxes_OnValueChanged(object sender, RoutedPropertyChangedEventArgs<double?> e)
    {
        if (CameraCheatsInst.CameraDetourAddress == 0)
        {
            return;
        }
        
        var write = new Vector3(
            Convert.ToSingle(XValueBox.Value),
            Convert.ToSingle(YValueBox.Value),
            0
        );
        
        GetInstance().WriteMemory(CameraCheatsInst.CameraDetourAddress + 0x5F, write);
    }
}