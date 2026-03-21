using System.Windows;
using System.Windows.Controls;
using XPaint.Cheats.Core;
using XPaint.ViewModels.SubPages.SelfVehicle;
using XPaint.Views.Windows;
using MahApps.Metro.Controls;
using static XPaint.Resources.Memory;

namespace XPaint.Views.SubPages.SelfVehicle
{
    public partial class Wheelspins : Page
    {
        public Wheelspins()
        {
            DataContext = this;
            MainWindow = MainWindow.Instance!;
            ViewModel = new();
            InitializeComponent();
        }

        public MainWindow MainWindow { get; }
        public WheelspinsViewModel ViewModel { get; }

        public MiscCheats MiscCheats = XPaint.Resources.Cheats.GetClass<MiscCheats>();
        public UnlocksCheats UnlocksCheatsInst => XPaint.Resources.Cheats.GetClass<UnlocksCheats>();

        private async void EmoteSwitch_OnToggled(object sender, RoutedEventArgs e)
        {
            if (sender is not ToggleSwitch toggleSwitch || emoteIdBox.Value == null || !MainWindow.ViewModel.Attached)
            {
                return;
            }

            toggleSwitch.IsEnabled = false;
            if (UnlocksCheatsInst.EmoteDetourAddress == 0)
            {
                await UnlocksCheatsInst.CheatEmote();
            }
            toggleSwitch.IsEnabled = true;

            if (UnlocksCheatsInst.EmoteDetourAddress == 0)
            {
                toggleSwitch.Toggled -= EmoteSwitch_OnToggled;
                toggleSwitch.IsOn = false;
                toggleSwitch.Toggled += EmoteSwitch_OnToggled;
                return;
            }

            int value = (int)emoteIdBox.Value;
            GetInstance().WriteMemory(UnlocksCheatsInst.EmoteDetourAddress + 0x20, value);
            GetInstance().WriteMemory(UnlocksCheatsInst.EmoteDetourAddress + 0x1F, toggleSwitch.IsOn ? (byte)1 : (byte)0);
        }

        private void EmoteIdBox_OnValueChanged(object sender, RoutedPropertyChangedEventArgs<double?> e)
        {
            if (UnlocksCheatsInst.EmoteDetourAddress == 0 || e.NewValue == null)
            {
                return;
            }

            int value = (int)e.NewValue.Value;
            GetInstance().WriteMemory(UnlocksCheatsInst.EmoteDetourAddress + 0x20, value);
        }

        private async void SellHackToggle_Toggled(object sender, RoutedEventArgs e)
        {
            if (sender is not ToggleSwitch toggleSwitch)
            {
                return;
            }

            toggleSwitch.IsEnabled = false;
            if (MiscCheats.SellFactorDetourAddress == 0)
            {
                await MiscCheats.CheatSellFactor();
            }
            toggleSwitch.IsEnabled = true;

            if (MiscCheats.SellFactorDetourAddress == 0)
            {
                toggleSwitch.Toggled -= SellHackToggle_Toggled;
                toggleSwitch.IsOn = false;
                toggleSwitch.Toggled += SellHackToggle_Toggled;
                return;
            }

            int value = (int)SellBox!.Value.GetValueOrDefault();
            GetInstance().WriteMemory(MiscCheats.SellFactorDetourAddress + 0x1D, value);
            GetInstance().WriteMemory(MiscCheats.SellFactorDetourAddress + 0x1C, toggleSwitch.IsOn ? (byte)1 : (byte)0);
        }

        private void SellBox_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double?> e)
        {
            if (MiscCheats.SellFactorDetourAddress != 0)
            {
                int value = (int)SellBox!.Value.GetValueOrDefault();
                GetInstance().WriteMemory(MiscCheats.SellFactorDetourAddress + 0x1D, value);
            }
        }

        private async void PrizeHackToggle_Toggled(object sender, RoutedEventArgs e)
        {
            if (sender is not ToggleSwitch toggleSwitch)
            {
                return;
            }

            toggleSwitch.IsEnabled = false;
            if (MiscCheats.PrizeScaleDetourAddress == 0)
            {
                await MiscCheats.CheatPrizeScale();
            }
            toggleSwitch.IsEnabled = true;

            if (MiscCheats.PrizeScaleDetourAddress == 0)
            {
                toggleSwitch.Toggled -= PrizeHackToggle_Toggled;
                toggleSwitch.IsOn = false;
                toggleSwitch.Toggled += PrizeHackToggle_Toggled;
                return;
            }

            float value = (float)PrizeBox!.Value.GetValueOrDefault();
            GetInstance().WriteMemory(MiscCheats.PrizeScaleDetourAddress + 0x1c, value);
            GetInstance().WriteMemory(MiscCheats.PrizeScaleDetourAddress + 0x1b, toggleSwitch.IsOn ? (byte)1 : (byte)0);
        }

        private void PrizeBox_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double?> e)
        {
            if (MiscCheats.PrizeScaleDetourAddress != 0)
            {
                float value = (float)PrizeBox!.Value.GetValueOrDefault();
                GetInstance().WriteMemory(MiscCheats.PrizeScaleDetourAddress + 0x1c, value);
            }
        }
    }
}
