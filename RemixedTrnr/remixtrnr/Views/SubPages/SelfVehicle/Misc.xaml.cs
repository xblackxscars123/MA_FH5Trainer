using System.Text;
using System.Windows;
using System.Windows.Controls;
using XPaint.Cheats.Core;
using XPaint.Models;
using XPaint.ViewModels.SubPages.SelfVehicle;
using XPaint.Views.Windows;
using MahApps.Metro.Controls;
using static XPaint.Resources.Memory;

namespace XPaint.Views.SubPages.SelfVehicle;

public partial class Misc
{
    public Misc()
    {
        MainWindow = MainWindow.Instance ?? new MainWindow();
        ViewModel = new MiscViewModel();
        DataContext = this;
        
        InitializeComponent();
    }

    public MainWindow MainWindow { get; }
    public MiscViewModel ViewModel { get; }
    private static MiscCheats MiscCheatsInst => XPaint.Resources.Cheats.GetClass<MiscCheats>();
    private static CarCheats CarCheatsInst => XPaint.Resources.Cheats.GetClass<CarCheats>();
    
    private async void NameSpooferSwitch_OnToggled(object sender, RoutedEventArgs e)
    {
        if (sender is not ToggleSwitch toggleSwitch)
        {
            return;
        }

        ViewModel.SpooferUiElementsEnabled = false;
        if (MiscCheatsInst.NameDetourAddress == 0)
        {
            await MiscCheatsInst.CheatName();
        }
        ViewModel.SpooferUiElementsEnabled = true;

        if (MiscCheatsInst.NameDetourAddress == 0)
        {
            toggleSwitch.Toggled -= NameSpooferSwitch_OnToggled;
            toggleSwitch.IsOn = false;
            toggleSwitch.Toggled += NameSpooferSwitch_OnToggled;
            return;
        }
        
        GetInstance().WriteMemory(MiscCheatsInst.NameDetourAddress + 0x55, toggleSwitch.IsOn ? (byte)1 : (byte)0);
        if (string.IsNullOrEmpty(NameBox.Text))
        {
            return;
        }
        
        var name = Encoding.Unicode.GetBytes(NameBox.Text);
        GetInstance().WriteArrayMemory(MiscCheatsInst.NameDetourAddress + 0x56, name);
    }

    private void NameBox_OnTextChanged(object sender, TextChangedEventArgs e)
    {
        if (MiscCheatsInst.NameDetourAddress == 0 || string.IsNullOrEmpty(NameBox.Text))
        {
            return;
        }
        
        var name = Encoding.Unicode.GetBytes(NameBox.Text);
        GetInstance().WriteArrayMemory(MiscCheatsInst.NameDetourAddress + 0x56, name);
    }

    private async void TpSwitch_OnToggled(object sender, RoutedEventArgs e)
    {
        if (sender is not ToggleSwitch toggleSwitch)
        {
            return;
        }

        toggleSwitch.IsEnabled = false;
        if (CarCheatsInst.WaypointDetourAddress == 0)
        {
            await CarCheatsInst.CheatWaypoint();
        }
        toggleSwitch.IsEnabled = true;

        if (CarCheatsInst.WaypointDetourAddress == 0)
        {
            toggleSwitch.Toggled -= TpSwitch_OnToggled;
            toggleSwitch.IsOn = false;
            toggleSwitch.Toggled += TpSwitch_OnToggled;
            return;
        }
        
        GetInstance().WriteMemory(CarCheatsInst.WaypointDetourAddress + 0x32, toggleSwitch.IsOn ? (byte)1 : (byte)0);
    }

    private void MainComboBox_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (sender is not ComboBox comboBox || MainToggleSwitch == null || MainValueBox == null)
        {
            return;
        }

        MainToggleSwitch.Toggled -= MainToggleSwitch_OnToggled;
        MainValueBox.ValueChanged -= MainValueBox_OnValueChanged;
        
        switch (comboBox.SelectedIndex)
        {
            case 0:
            {              
                MainValueBox.Value = ViewModel.SkillTreeWideEditValue;
                MainToggleSwitch.IsOn = ViewModel.SkillTreeWideEditEnabled;
                break;
            }
            case 1:
            {             
                MainValueBox.Value = ViewModel.SkillTreeCostValue;
                MainToggleSwitch.IsOn = ViewModel.SkillTreeCostEnabled;
                break;
            }
            case 2:
            {           
                MainValueBox.Value = ViewModel.DroneModeHeightValue;
                MainToggleSwitch.IsOn = ViewModel.DroneModeHeightEnabled;
                break;
            }
        }

        MainValueBox.Minimum = comboBox.SelectedIndex switch
        { 
            0 or 1 or 2 or 3 or 4 or 6 or 7 or 8 or 9 or 10 or 11 => 0,
            5 => int.MinValue,
            12 => 1,
            _ => throw new IndexOutOfRangeException()
        };

        MainValueBox.Maximum = comboBox.SelectedIndex switch
        { 
            0 or 1 or 2 or 4 or 5 or 12 => int.MaxValue,
            3 or 8 or 10 or 11 => 10,
            6 or 7 or 9 => 1,
            _ => throw new IndexOutOfRangeException()
        };

        MainValueBox.Interval = comboBox.SelectedIndex switch
        { 
            0 or 1 or 2 or 3 or 4 or 5 or 8 or 10 or 11 or 12 => 1,
            6 or 7 or 9 => 0.1,
            _ => throw new IndexOutOfRangeException()
        };
        
        MainValueBox.ValueChanged += MainValueBox_OnValueChanged;
        MainToggleSwitch.Toggled += MainToggleSwitch_OnToggled;
    }

    private void MainValueBox_OnValueChanged(object sender, RoutedPropertyChangedEventArgs<double?> e)
    {
        WriteValue(e);
    }

    private void WriteValue(RoutedPropertyChangedEventArgs<double?> e)
    {
        switch (MainComboBox.SelectedIndex)
        {
            case 0:
            {              
                ViewModel.SkillTreeWideEditValue = Convert.ToSingle(e.NewValue);
                if (MiscCheatsInst.SkillTreeWideEditDetourAddress == 0)
                {
                    return;
                }
                
                GetInstance().WriteMemory(MiscCheatsInst.SkillTreeWideEditDetourAddress + 0x1C, ViewModel.SkillTreeWideEditValue);
                break;
            }
            case 1:
            {             
                ViewModel.SkillTreeCostValue = Convert.ToInt32(e.NewValue);
                if (MiscCheatsInst.SkillTreePerksCostDetourAddress == 0)
                {
                    return;
                }
                
                GetInstance().WriteMemory(MiscCheatsInst.SkillTreePerksCostDetourAddress + 0x1B, ViewModel.SkillTreeCostValue);
                break;
            }
            case 2:
            {           
                ViewModel.DroneModeHeightValue = Convert.ToSingle(e.NewValue);
                if (MiscCheatsInst.DroneModeMaxHeightMultiDetourAddress == 0)
                {
                    return;
                }
                
                GetInstance().WriteMemory(MiscCheatsInst.DroneModeMaxHeightMultiDetourAddress + 0x1E, ViewModel.DroneModeHeightValue);
                break;
            }
        }
    }
    private async void MainToggleSwitch_OnToggled(object sender, RoutedEventArgs e)
    {
        if (sender is not ToggleSwitch toggleSwitch)
        {
            return;
        }

        ViewModel.MainUiElementsEnabled = false;
        await EnableCheat(toggleSwitch);
        ViewModel.MainUiElementsEnabled = true;
    }

    private async Task EnableCheat(ToggleSwitch toggleSwitch)
    {
        switch (MainComboBox.SelectedIndex)
        {
            case 0:
            {
                await SkillTreeWideEdit(toggleSwitch.IsOn);
                break;
            }
            case 1:
            {
                await SkillTreePerksCost(toggleSwitch.IsOn);
                break;
            }
            case 2:
            {
                await DroneModeHeight(toggleSwitch.IsOn);
                break;
            }
        }
    }
    
    private async Task PrizeScale(bool toggled)
    {
        if (MiscCheatsInst.PrizeScaleDetourAddress == 0)
        {
            await MiscCheatsInst.CheatPrizeScale();
        }

        if (MiscCheatsInst.PrizeScaleDetourAddress == 0) return;
        GetInstance().WriteMemory(MiscCheatsInst.PrizeScaleDetourAddress + 0x1B, toggled ? (byte)1 : (byte)0);
        GetInstance().WriteMemory(MiscCheatsInst.PrizeScaleDetourAddress + 0x1C, Convert.ToSingle(MainValueBox.Value));
        ViewModel.SpinPrizeScaleEnabled = toggled;
    }

    private async Task SellFactor(bool toggled)
    {
        if (MiscCheatsInst.SellFactorDetourAddress == 0)
        {
            await MiscCheatsInst.CheatSellFactor();
        }
        
        if (MiscCheatsInst.SellFactorDetourAddress == 0) return;
        GetInstance().WriteMemory(MiscCheatsInst.SellFactorDetourAddress + 0x1C, toggled ? (byte)1 : (byte)0);
        GetInstance().WriteMemory(MiscCheatsInst.SellFactorDetourAddress + 0x1D, Convert.ToInt32(MainValueBox.Value));
        ViewModel.SpinSellFactorEnabled = toggled;
    }

    private async Task SkillScoreMultiplier(bool toggled)
    {
        if (MiscCheatsInst.SkillScoreMultiplierDetourAddress == 0)
        {
            await MiscCheatsInst.CheatSkillScoreMultiplier();
        }
        
        if (MiscCheatsInst.SkillScoreMultiplierDetourAddress == 0) return;
        GetInstance().WriteMemory(MiscCheatsInst.SkillScoreMultiplierDetourAddress + 0x1C, toggled ? (byte)1 : (byte)0);
        GetInstance().WriteMemory(MiscCheatsInst.SkillScoreMultiplierDetourAddress + 0x1D, Convert.ToInt32(MainValueBox.Value));
        ViewModel.SkillScoreMultiplierEnabled = toggled;
    }
    
    private async Task DriftScoreMultiplier(bool toggled)
    {
        if (MiscCheatsInst.DriftScoreMultiplierDetourAddress == 0)
        {
            await MiscCheatsInst.CheatDriftScoreMultiplier();
        }
        
        if (MiscCheatsInst.DriftScoreMultiplierDetourAddress == 0) return;
        GetInstance().WriteMemory(MiscCheatsInst.DriftScoreMultiplierDetourAddress + 0x1F, toggled ? (byte)1 : (byte)0);
        GetInstance().WriteMemory(MiscCheatsInst.DriftScoreMultiplierDetourAddress + 0x20, Convert.ToSingle(MainValueBox.Value));
        ViewModel.DriftScoreMultiplierEnabled = toggled;
    }
    
    private async Task SkillTreeWideEdit(bool toggled)
    {
        if (MiscCheatsInst.SkillTreeWideEditDetourAddress == 0)
        {
            await MiscCheatsInst.CheatSkillTreeWideEdit();
        }
        
        if (MiscCheatsInst.SkillTreeWideEditDetourAddress == 0) return;
        GetInstance().WriteMemory(MiscCheatsInst.SkillTreeWideEditDetourAddress + 0x1B, toggled ? (byte)1 : (byte)0);
        GetInstance().WriteMemory(MiscCheatsInst.SkillTreeWideEditDetourAddress + 0x1C, Convert.ToSingle(MainValueBox.Value));
        ViewModel.SkillTreeWideEditEnabled = toggled;
    }
    
    private async Task SkillTreePerksCost(bool toggled)
    {
        if (MiscCheatsInst.SkillTreePerksCostDetourAddress == 0)
        {
            await MiscCheatsInst.CheatSkillTreePerksCost();
        }
        
        if (MiscCheatsInst.SkillTreePerksCostDetourAddress == 0) return;
        GetInstance().WriteMemory(MiscCheatsInst.SkillTreePerksCostDetourAddress + 0x1A, toggled ? (byte)1 : (byte)0);
        GetInstance().WriteMemory(MiscCheatsInst.SkillTreePerksCostDetourAddress + 0x1B, Convert.ToInt32(MainValueBox.Value));
        ViewModel.SkillTreeCostEnabled = toggled;
    }
    
    private async Task MissionTimeScale(bool toggled)
    {
        if (MiscCheatsInst.MissionTimeScaleDetourAddress == 0)
        {
            await MiscCheatsInst.CheatMissionTimeScale();
        }
        
        if (MiscCheatsInst.MissionTimeScaleDetourAddress == 0) return;
        GetInstance().WriteMemory(MiscCheatsInst.MissionTimeScaleDetourAddress + 0x22, toggled ? (byte)1 : (byte)0);
        GetInstance().WriteMemory(MiscCheatsInst.MissionTimeScaleDetourAddress + 0x23, Convert.ToSingle(MainValueBox.Value));
        ViewModel.MissionTimeScaleEnabled = toggled;
    }
    
    private async Task TrailblazerTimeScale(bool toggled)
    {
        if (MiscCheatsInst.TrailblazerTimeScaleDetourAddress == 0)
        {
            await MiscCheatsInst.CheatTrailblazerTimeScale();
        }
        
        if (MiscCheatsInst.TrailblazerTimeScaleDetourAddress == 0) return;
        GetInstance().WriteMemory(MiscCheatsInst.TrailblazerTimeScaleDetourAddress + 0x22, toggled ? (byte)1 : (byte)0);
        GetInstance().WriteMemory(MiscCheatsInst.TrailblazerTimeScaleDetourAddress + 0x23, Convert.ToSingle(MainValueBox.Value));
        ViewModel.TrailblazerTimeScaleEnabled = toggled;
    }

    private async Task SpeedZoneMultiplier(bool toggled)
    {
        if (MiscCheatsInst.SpeedZoneMultiplierDetourAddress == 0)
        {
            await MiscCheatsInst.CheatSpeedZoneMultiplier();
        }
        
        if (MiscCheatsInst.SpeedZoneMultiplierDetourAddress == 0) return;
        GetInstance().WriteMemory(MiscCheatsInst.SpeedZoneMultiplierDetourAddress + 0x1F, toggled ? (byte)1 : (byte)0);
        GetInstance().WriteMemory(MiscCheatsInst.SpeedZoneMultiplierDetourAddress + 0x20, Convert.ToSingle(MainValueBox.Value));
        ViewModel.SpeedZoneMultiplierEnabled = toggled;
    }
    
    private async Task RaceTimeScale(bool toggled)
    {
        if (MiscCheatsInst.RaceTimeScaleDetourAddress == 0)
        {
            await MiscCheatsInst.CheatRaceTimeScale();
        }
        
        if (MiscCheatsInst.RaceTimeScaleDetourAddress == 0) return;
        GetInstance().WriteMemory(MiscCheatsInst.RaceTimeScaleDetourAddress + 0x34, toggled ? (byte)1 : (byte)0);
        GetInstance().WriteMemory(MiscCheatsInst.RaceTimeScaleDetourAddress + 0x35, Convert.ToSingle(MainValueBox.Value));
        ViewModel.RaceTimeScaleEnabled = toggled;
    }
    
    private async Task DangerSignMultiplier(bool toggled)
    {
        if (MiscCheatsInst.DangerSign1DetourAddress == 0 ||
            MiscCheatsInst.DangerSign2DetourAddress == 0 ||
            MiscCheatsInst.DangerSign3DetourAddress == 0)
        {
            await MiscCheatsInst.CheatDangerSignMultiplier();
        }

        if (MiscCheatsInst.DangerSign1DetourAddress == 0 ||
            MiscCheatsInst.DangerSign2DetourAddress == 0 ||
            MiscCheatsInst.DangerSign3DetourAddress == 0)
        {
            return;
        }
        
        GetInstance().WriteMemory(MiscCheatsInst.DangerSign1DetourAddress + 0x37, toggled ? (byte)1 : (byte)0);
        GetInstance().WriteMemory(MiscCheatsInst.DangerSign1DetourAddress + 0x38, Convert.ToSingle(MainValueBox.Value));
        GetInstance().WriteMemory(MiscCheatsInst.DangerSign2DetourAddress + 0x34, toggled ? (byte)1 : (byte)0);
        GetInstance().WriteMemory(MiscCheatsInst.DangerSign2DetourAddress + 0x35, Convert.ToSingle(MainValueBox.Value));
        GetInstance().WriteMemory(MiscCheatsInst.DangerSign3DetourAddress + 0x34, toggled ? (byte)1 : (byte)0);
        GetInstance().WriteMemory(MiscCheatsInst.DangerSign3DetourAddress + 0x35, Convert.ToSingle(MainValueBox.Value));
        ViewModel.DangerSignMultiplierEnabled = toggled;
    }

    private async Task SpeedTrapMultiplier(bool toggled)
    {
        if (MiscCheatsInst.SpeedTrapMultiplierDetourAddress == 0)
        {
            await MiscCheatsInst.CheatSpeedTrapMultiplier();
        }
        
        if (MiscCheatsInst.SpeedTrapMultiplierDetourAddress == 0) return;
        GetInstance().WriteMemory(MiscCheatsInst.SpeedTrapMultiplierDetourAddress + 0x34, toggled ? (byte)1 : (byte)0);
        GetInstance().WriteMemory(MiscCheatsInst.SpeedTrapMultiplierDetourAddress + 0x35, Convert.ToSingle(MainValueBox.Value));
        ViewModel.SpeedTrapMultiplierEnabled = toggled;
    }

    private async Task DroneModeHeight(bool toggled)
    {
        if (MiscCheatsInst.DroneModeMaxHeightMultiDetourAddress == 0)
        {
            await MiscCheatsInst.CheatDroneModeMaxHeightMulti();
        }
        
        if (MiscCheatsInst.DroneModeMaxHeightMultiDetourAddress == 0) return;
        GetInstance().WriteMemory(MiscCheatsInst.DroneModeMaxHeightMultiDetourAddress + 0x1D, toggled ? (byte)1 : (byte)0);
        GetInstance().WriteMemory(MiscCheatsInst.DroneModeMaxHeightMultiDetourAddress + 0x1E, Convert.ToSingle(MainValueBox.Value));
        ViewModel.DroneModeHeightEnabled = toggled;
    }

    private async void UnbreakableSkillScoreSwitch_OnToggled(object sender, RoutedEventArgs e)
    {
        if (sender is not ToggleSwitch toggleSwitch)
        {
            return;
        }

        toggleSwitch.IsEnabled = false;
        if (MiscCheatsInst.UnbreakableSkillScoreDetourAddress == 0)
        {
            await MiscCheatsInst.CheatUnbreakableSkillScore();
        }
        toggleSwitch.IsEnabled = true;

        if (MiscCheatsInst.UnbreakableSkillScoreDetourAddress == 0)
        {
            toggleSwitch.Toggled -= UnbreakableSkillScoreSwitch_OnToggled;
            toggleSwitch.IsOn = false;
            toggleSwitch.Toggled -= UnbreakableSkillScoreSwitch_OnToggled;
            return;
        }
        
        GetInstance().WriteMemory(MiscCheatsInst.UnbreakableSkillScoreDetourAddress + 0x1A, toggleSwitch.IsOn ? (byte)1 : (byte)0);
    }

    private async void RemoveBuildCapSwitch_OnToggled(object sender, RoutedEventArgs e)
    {
        if (sender is not ToggleSwitch toggleSwitch)
        {
            return;
        }

        toggleSwitch.IsEnabled = false;
        
        if (MiscCheatsInst.RemoveBuildCapDetourAddress == 0)
        {
            await MiscCheatsInst.CheatRemoveBuildCap();
        }
        toggleSwitch.IsEnabled = true;

        if (MiscCheatsInst.RemoveBuildCapDetourAddress == 0)
        {
            toggleSwitch.Toggled -= RemoveBuildCapSwitch_OnToggled;
            toggleSwitch.IsOn = false;
            toggleSwitch.Toggled -= RemoveBuildCapSwitch_OnToggled;
            return;
        }
        
        GetInstance().WriteMemory(MiscCheatsInst.RemoveBuildCapDetourAddress + 0x16, toggleSwitch.IsOn ? (byte)1 : (byte)0);
    }
}
