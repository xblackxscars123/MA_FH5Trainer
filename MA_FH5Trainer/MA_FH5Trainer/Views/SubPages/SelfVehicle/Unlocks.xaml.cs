using System.Windows;
using System.Windows.Controls;
<<<<<<< C:/Users/GAMING/Documents/GitHub/MA_FH5Trainer/MA_FH5Trainer/MA_FH5Trainer/Views/SubPages/SelfVehicle/Unlocks.xaml.cs
<<<<<<< C:/Users/GAMING/Documents/GitHub/MA_FH5Trainer/MA_FH5Trainer/MA_FH5Trainer/Views/SubPages/SelfVehicle/Unlocks.xaml.cs
using XPaint.Cheats.ForzaHorizon5;
=======
using XPaint.Cheats.Core;
>>>>>>> C:/Users/GAMING/.windsurf/worktrees/MA_FH5Trainer/MA_FH5Trainer-90665ca8/MA_FH5Trainer/MA_FH5Trainer/Views/SubPages/SelfVehicle/Unlocks.xaml.cs
=======
using XPaint.Cheats.Core;
>>>>>>> C:/Users/GAMING/.windsurf/worktrees/MA_FH5Trainer/MA_FH5Trainer-90665ca8/MA_FH5Trainer/MA_FH5Trainer/Views/SubPages/SelfVehicle/Unlocks.xaml.cs
using XPaint.Models;
using XPaint.ViewModels.SubPages.SelfVehicle;
using XPaint.Views.Windows;
using MahApps.Metro.Controls;
using static XPaint.Resources.Cheats;
using static XPaint.Resources.Memory;

namespace XPaint.Views.SubPages.SelfVehicle;

public partial class Unlocks
{
    public Unlocks()
    {
        MainWindow = MainWindow.Instance ?? new MainWindow();
        ViewModel =  new UnlocksViewModel();
        DataContext = this;
        
        InitializeComponent();
    }

    public MainWindow MainWindow { get; } 
    public UnlocksViewModel ViewModel { get; }
    private static UnlocksCheats UnlocksCheatsInst => GetClass<UnlocksCheats>();
    
    private async void ToggleSwitch_OnToggled(object sender, RoutedEventArgs e)
    {
        if (sender is not ToggleSwitch toggleSwitch || !MainWindow.ViewModel.Attached)
        {
            return;
        }

        ViewModel.AreUiElementsEnabled = false;
        bool success = UnlockBox.SelectedIndex switch
        {
            0 => await SkillPoints(toggleSwitch.IsOn),
            1 => await Accolades(toggleSwitch.IsOn),
            2 => await Kudos(toggleSwitch.IsOn),
            3 => await Series(toggleSwitch.IsOn),
            4 => await Seasonal(toggleSwitch.IsOn),
            _ => false
        };

        ViewModel.AreUiElementsEnabled = true;
        if (!success)
        {
            toggleSwitch.Toggled -= ToggleSwitch_OnToggled;
            toggleSwitch.IsOn = false;
            toggleSwitch.Toggled += ToggleSwitch_OnToggled;
        }
    }
    private async Task<bool> SkillPoints(bool toggled)
    {
        if (UnlocksCheatsInst.SkillPointsDetourAddress == 0)
        {
            await UnlocksCheatsInst.CheatSkillPoints();
        }

        if (UnlocksCheatsInst.SkillPointsDetourAddress <= 0)
        {
            return false;
        }
        
        GetInstance().WriteMemory(UnlocksCheatsInst.SkillPointsDetourAddress + 0x19, toggled ? (byte)1 : (byte)0);
        GetInstance().WriteMemory(UnlocksCheatsInst.SkillPointsDetourAddress + 0x1A, Convert.ToInt32(ValueBox.Value));  
        ViewModel.IsSkillPointsEnabled = toggled;
        return true;
    }

    private async Task<bool> Kudos(bool toggled)
    {
        if (UnlocksCheatsInst.BxmlEncryptionDetourAddress == 0)
        {
            await UnlocksCheatsInst.CheatBxmlEncryption();
        }

        if (UnlocksCheatsInst.BxmlEncryptionDetourAddress <= 0)
        {
            return false;
        }
        
        GetInstance().WriteMemory(UnlocksCheatsInst.BxmlEncryptionDetourAddress + 0x54, toggled ? (byte)1 : (byte)0);
        GetInstance().WriteMemory(UnlocksCheatsInst.BxmlEncryptionDetourAddress + 0x55, Convert.ToInt32(ValueBox.Value));  
        ViewModel.IsKudosEnabled = toggled;
        return true;
    }

    private async Task<bool> Accolades(bool toggled)
    {
        if (UnlocksCheatsInst.BxmlEncryptionDetourAddress == 0)
        {
            await UnlocksCheatsInst.CheatBxmlEncryption();
        }

        if (UnlocksCheatsInst.BxmlEncryptionDetourAddress <= 0)
        {
            return false;
        }
        
        GetInstance().WriteMemory(UnlocksCheatsInst.BxmlEncryptionDetourAddress + 0x59, toggled ? (byte)1 : (byte)0);
        GetInstance().WriteMemory(UnlocksCheatsInst.BxmlEncryptionDetourAddress + 0x5A, Convert.ToInt32(ValueBox.Value));  
        ViewModel.IsAccoladesEnabled = toggled;
        return true;
    }

    private async Task<bool> Seasonal(bool toggled)
    {
        if (UnlocksCheatsInst.SeasonalDetourAddress == 0)
        {
            await UnlocksCheatsInst.CheatSeasonal();
        }

        if (UnlocksCheatsInst.SeasonalDetourAddress <= 0)
        {
            return false;
        }
        
        GetInstance().WriteMemory(UnlocksCheatsInst.SeasonalDetourAddress + 0x23, toggled ? (byte)1 : (byte)0);
        GetInstance().WriteMemory(UnlocksCheatsInst.SeasonalDetourAddress + 0x24, Convert.ToInt32(ValueBox.Value));  
        ViewModel.IsSeasonalEnabled = toggled;
        return true;
    }
    
    private async Task<bool> Series(bool toggled)
    {
        if (UnlocksCheatsInst.SeriesDetourAddress == 0)
        {
            await UnlocksCheatsInst.CheatSeries();
        }

        if (UnlocksCheatsInst.SeriesDetourAddress <= 0)
        {
            return false;
        }
        
        GetInstance().WriteMemory(UnlocksCheatsInst.SeriesDetourAddress + 0x1B, toggled ? (byte)1 : (byte)0);
        GetInstance().WriteMemory(UnlocksCheatsInst.SeriesDetourAddress + 0x1C, Convert.ToInt32(ValueBox.Value));  
        ViewModel.IsSeriesEnabled = toggled;
        return true;
    }

    private void UnlockBox_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (UnlockSwitch == null || sender is not ComboBox comboBox)
        {
            return;
        }

        // ReSharper disable once AssignNullToNotNullAttribute
        // note: unclear purpose of this block.
        while (((ComboBoxItem)comboBox.Items[comboBox.SelectedIndex]).Visibility == Visibility.Collapsed)
        {
            comboBox.SelectedIndex -= 1;
        }
        
        UnlockSwitch.Toggled -= ToggleSwitch_OnToggled;
        switch (UnlockBox.SelectedIndex)
        {
            case 0:
            {
                ValueBox.Value = ViewModel.SkillPointsValue;
                UnlockSwitch.IsOn = ViewModel.IsSkillPointsEnabled;
                break;
            }
            case 1:
            {
                ValueBox.Value = ViewModel.AccoladesValue;
                UnlockSwitch.IsOn = ViewModel.IsAccoladesEnabled;
                break;
            }
            case 2:
            {
                ValueBox.Value = ViewModel.KudosValue;
                UnlockSwitch.IsOn = ViewModel.IsKudosEnabled;
                break;
            }
            case 3:
            {
                ValueBox.Value = ViewModel.SeriesValue;
                UnlockSwitch.IsOn = ViewModel.IsSeriesEnabled;
                break;
            }
            case 4:
            {
                ValueBox.Value = ViewModel.SeasonalValue;
                UnlockSwitch.IsOn = ViewModel.IsSeasonalEnabled;
                break;
            }
        }
        
        UnlockSwitch.Toggled += ToggleSwitch_OnToggled;
    }

    private void ValueBox_OnValueChanged(object sender, RoutedPropertyChangedEventArgs<double?> e)
    {
        switch (UnlockBox.SelectedIndex)
        {
            case 0:
            {
                ViewModel.SkillPointsValue = Convert.ToInt32(ValueBox.Value);
                if (UnlocksCheatsInst.SkillPointsDetourAddress <= 0)
                {
                    return;
                }
                
                GetInstance().WriteMemory(UnlocksCheatsInst.SkillPointsDetourAddress + 0x1A, ViewModel.SkillPointsValue);  
                break;
            }
            case 1:
            {
                ViewModel.AccoladesValue = Convert.ToInt32(ValueBox.Value);
                if (UnlocksCheatsInst.BxmlEncryptionDetourAddress <= 0)
                {
                    return;
                }
                GetInstance().WriteMemory(UnlocksCheatsInst.BxmlEncryptionDetourAddress + 0x5A, Convert.ToInt32(ValueBox.Value));  
                break;
            }
            case 2:
            {
                ViewModel.KudosValue = Convert.ToInt32(ValueBox.Value);
                if (UnlocksCheatsInst.BxmlEncryptionDetourAddress <= 0)
                {
                    return;
                }
                GetInstance().WriteMemory(UnlocksCheatsInst.BxmlEncryptionDetourAddress + 0x55, Convert.ToInt32(ValueBox.Value));  
                break;
            }
            case 3:
            {
                ViewModel.SeriesValue = Convert.ToInt32(ValueBox.Value);
                if (UnlocksCheatsInst.SeriesDetourAddress <= 0)
                {
                    return;
                }
                GetInstance().WriteMemory(UnlocksCheatsInst.SeriesDetourAddress + 0x1C, Convert.ToInt32(ValueBox.Value));  
                break;
            }
            case 4:
            {
                ViewModel.SeasonalValue = Convert.ToInt32(ValueBox.Value);
                if (UnlocksCheatsInst.SeasonalDetourAddress <= 0)
                {
                    return;
                }
                GetInstance().WriteMemory(UnlocksCheatsInst.SeasonalDetourAddress + 0x24, Convert.ToInt32(ValueBox.Value));  
                break;
            }
        }
    }
    private async void FreeClothingSwitch_OnToggled(object sender, RoutedEventArgs e)
    {
        if (sender is not ToggleSwitch toggleSwitch || !MainWindow.ViewModel.Attached)
        {
            return;
        }

        toggleSwitch.IsEnabled = false;
        if (UnlocksCheatsInst.Clothing1DetourAddress == 0 || UnlocksCheatsInst.Clothing2DetourAddress == 0)
        {
            await UnlocksCheatsInst.CheatClothing();
        }
        
        toggleSwitch.IsEnabled = true;
        if (UnlocksCheatsInst.Clothing1DetourAddress == 0 || UnlocksCheatsInst.Clothing2DetourAddress == 0)
        {
            toggleSwitch.Toggled -= FreeClothingSwitch_OnToggled;
            toggleSwitch.IsOn = false;
            toggleSwitch.Toggled += FreeClothingSwitch_OnToggled;
            return;
        }

        GetInstance().WriteMemory(UnlocksCheatsInst.Clothing1DetourAddress + 0x19, toggleSwitch.IsOn ? (byte)1 : (byte)0);
        GetInstance().WriteMemory(UnlocksCheatsInst.Clothing2DetourAddress + 0x19, toggleSwitch.IsOn ? (byte)1 : (byte)0);
    }

}