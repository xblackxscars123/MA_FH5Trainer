using System.Windows;
using System.Windows.Controls;
<<<<<<< C:/Users/GAMING/Documents/GitHub/MA_FH5Trainer/MA_FH5Trainer/MA_FH5Trainer/Views/SubPages/Tuning/Tires.xaml.cs
<<<<<<< C:/Users/GAMING/Documents/GitHub/MA_FH5Trainer/MA_FH5Trainer/MA_FH5Trainer/Views/SubPages/Tuning/Tires.xaml.cs
<<<<<<< C:/Users/GAMING/Documents/GitHub/MA_FH5Trainer/MA_FH5Trainer/MA_FH5Trainer/Views/SubPages/Tuning/Tires.xaml.cs
<<<<<<< C:/Users/GAMING/Documents/GitHub/MA_FH5Trainer/MA_FH5Trainer/MA_FH5Trainer/Views/SubPages/Tuning/Tires.xaml.cs
using XPaint.Cheats.ForzaHorizon5;
=======
using XPaint.Cheats.Core;
>>>>>>> C:/Users/GAMING/.windsurf/worktrees/MA_FH5Trainer/MA_FH5Trainer-90665ca8/MA_FH5Trainer/MA_FH5Trainer/Views/SubPages/Tuning/Tires.xaml.cs
=======
using XPaint.Cheats.Core;
>>>>>>> C:/Users/GAMING/.windsurf/worktrees/MA_FH5Trainer/MA_FH5Trainer-90665ca8/MA_FH5Trainer/MA_FH5Trainer/Views/SubPages/Tuning/Tires.xaml.cs
=======
using XPaint.Cheats.Core;
>>>>>>> C:/Users/GAMING/.windsurf/worktrees/MA_FH5Trainer/MA_FH5Trainer-4284e8b3/MA_FH5Trainer/MA_FH5Trainer/Views/SubPages/Tuning/Tires.xaml.cs
=======
using XPaint.Cheats.Core;
>>>>>>> C:/Users/GAMING/.windsurf/worktrees/MA_FH5Trainer/MA_FH5Trainer-4284e8b3/MA_FH5Trainer/MA_FH5Trainer/Views/SubPages/Tuning/Tires.xaml.cs
using XPaint.Views.Windows;
using static XPaint.Resources.Memory;

namespace XPaint.Views.SubPages.Tuning;

public partial class Tires
{
    public Tires()
    {
        MainWindow = MainWindow.Instance ?? new MainWindow();
        DataContext = this;

        InitializeComponent();
    }
    
    public MainWindow MainWindow { get; }
<<<<<<< C:/Users/GAMING/Documents/GitHub/MA_FH5Trainer/MA_FH5Trainer/MA_FH5Trainer/Views/SubPages/Tuning/Tires.xaml.cs
<<<<<<< C:/Users/GAMING/Documents/GitHub/MA_FH5Trainer/MA_FH5Trainer/MA_FH5Trainer/Views/SubPages/Tuning/Tires.xaml.cs
<<<<<<< C:/Users/GAMING/Documents/GitHub/MA_FH5Trainer/MA_FH5Trainer/MA_FH5Trainer/Views/SubPages/Tuning/Tires.xaml.cs
<<<<<<< C:/Users/GAMING/Documents/GitHub/MA_FH5Trainer/MA_FH5Trainer/MA_FH5Trainer/Views/SubPages/Tuning/Tires.xaml.cs
    private static TuningCheats TuningCheatsFh5 => XPaint.Resources.Cheats.GetClass<TuningCheats>();
    private static CarCheats CarCheatsFh5 => XPaint.Resources.Cheats.GetClass<CarCheats>();
=======
    private static TuningCheats TuningCheatsInst => XPaint.Resources.Cheats.GetClass<TuningCheats>();
    private static CarCheats CarCheatsInst => XPaint.Resources.Cheats.GetClass<CarCheats>();
>>>>>>> C:/Users/GAMING/.windsurf/worktrees/MA_FH5Trainer/MA_FH5Trainer-90665ca8/MA_FH5Trainer/MA_FH5Trainer/Views/SubPages/Tuning/Tires.xaml.cs
=======
    private static TuningCheats TuningCheatsInst => XPaint.Resources.Cheats.GetClass<TuningCheats>();
    private static CarCheats CarCheatsInst => XPaint.Resources.Cheats.GetClass<CarCheats>();
>>>>>>> C:/Users/GAMING/.windsurf/worktrees/MA_FH5Trainer/MA_FH5Trainer-90665ca8/MA_FH5Trainer/MA_FH5Trainer/Views/SubPages/Tuning/Tires.xaml.cs
=======
    private static TuningCheats TuningCheatsInst => XPaint.Resources.Cheats.GetClass<TuningCheats>();
    private static CarCheats CarCheatsInst => XPaint.Resources.Cheats.GetClass<CarCheats>();
>>>>>>> C:/Users/GAMING/.windsurf/worktrees/MA_FH5Trainer/MA_FH5Trainer-4284e8b3/MA_FH5Trainer/MA_FH5Trainer/Views/SubPages/Tuning/Tires.xaml.cs
=======
    private static TuningCheats TuningCheatsInst => XPaint.Resources.Cheats.GetClass<TuningCheats>();
    private static CarCheats CarCheatsInst => XPaint.Resources.Cheats.GetClass<CarCheats>();
>>>>>>> C:/Users/GAMING/.windsurf/worktrees/MA_FH5Trainer/MA_FH5Trainer-4284e8b3/MA_FH5Trainer/MA_FH5Trainer/Views/SubPages/Tuning/Tires.xaml.cs
    private static UIntPtr Ptr => GetInstance()
        .ReadMemory<UIntPtr>(CarCheatsInst.LocalPlayerHookDetourAddress + CarCheatsOffsets.LocalPlayer);
    
    private async void Scan_OnClick(object sender, RoutedEventArgs e)
    {
        MainWindow.ViewModel.TuningScanSuccess = false;
        MainWindow.ViewModel.TuningScanToBeDone = false;
        MainWindow.ViewModel.TuningScanInProgress = true;

        if (!TuningCheatsInst.WasScanSuccessful)
        {
            await TuningCheatsInst.Scan();
        }

        if (!TuningCheatsInst.WasScanSuccessful)
        {
            MainWindow.ViewModel.TuningScanSuccess = false;
            MainWindow.ViewModel.TuningScanToBeDone = true;
            MainWindow.ViewModel.TuningScanInProgress = false;
            return;
        }
        
        MainWindow.ViewModel.TuningScanSuccess = true;
        MainWindow.ViewModel.TuningScanToBeDone = false;
        MainWindow.ViewModel.TuningScanInProgress = false;
    }
    
    private bool _codeChange;

    private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
    {
        UpdateValue();
    }

    private void ValueBox_OnValueChanged(object sender, RoutedPropertyChangedEventArgs<double?> e)
    {
        if (!TuningCheatsInst.WasScanSuccessful || _codeChange)
        {
            return;
        }
        
        var divider = UnitBox.SelectedIndex == 0 ? 1f : 14.503773773f;
        var newValue = Convert.ToSingle(e.NewValue.GetValueOrDefault()) / divider;
        switch (ComboBox.SelectedIndex)
        {
            case 0:
            {
                GetInstance().WriteMemory(Ptr + TuningOffsets.FrontLeftTirePressureOffset, newValue);                
                break;
            }
            case 1:
            {
                GetInstance().WriteMemory(Ptr + TuningOffsets.FrontRightTirePressureOffset, newValue);                
                break;
            }
            case 2:
            {
                GetInstance().WriteMemory(Ptr + TuningOffsets.RearLeftTirePressureOffset, newValue);                
                break;
            }
            case 3:
            {
                GetInstance().WriteMemory(Ptr + TuningOffsets.RearRightTirePressureOffset, newValue);                
                break;
            }
        }
    }

    private void ComboBox_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        UpdateValue();
    }

    private void UnitBox_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        UpdateValue();
    }

    private void UpdateValue()
    {
        if (ValueBox == null || !TuningCheatsInst.WasScanSuccessful)
        {
            return;
        }

        _codeChange = true;
        var divider = UnitBox.SelectedIndex == 0 ? 1f : 14.503773773f;
        ValueBox.Value = ComboBox.SelectedIndex switch
        {
            0 => GetInstance().ReadMemory<float>(Ptr + TuningOffsets.FrontLeftTirePressureOffset) / divider,
            1 => GetInstance().ReadMemory<float>(Ptr + TuningOffsets.FrontRightTirePressureOffset) / divider,
            2 => GetInstance().ReadMemory<float>(Ptr + TuningOffsets.RearLeftTirePressureOffset) / divider,
            3 => GetInstance().ReadMemory<float>(Ptr + TuningOffsets.RearRightTirePressureOffset) / divider,
            _ => ValueBox.Value
        };
        _codeChange = false;
    }
}