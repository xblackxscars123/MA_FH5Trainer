using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Media;
using ControlzEx.Theming;
using static System.Windows.Media.ColorConverter;

namespace XPaint.Resources.Theme;

public sealed class Theming : INotifyPropertyChanged
{
    private static readonly object s_lock = new object();
    private static Theming? _instance;
    public static Theming GetInstance()
    {
        lock (s_lock)
        {
            if (_instance != null)
            {
                return _instance;
            }
            
            _instance = new Theming();
            return _instance;
        }
    }
    
    private Brush _lighterColour = new SolidColorBrush((Color)ConvertFromString("#2D1F4E"));
    public Brush LighterColour
    {
        get => _lighterColour;
        private set => SetField(ref _lighterColour, value);
    }
    
    private Brush _lightColour = new SolidColorBrush((Color)ConvertFromString("#241840"));
    public Brush LightColour
    {
        get => _lightColour;
        private set => SetField(ref _lightColour, value);
    }
    
    private Brush _mainColour = new SolidColorBrush((Color)ConvertFromString("#1B1033"));
    public Brush MainColour
    {
        get => _mainColour;
        private set => SetField(ref _mainColour, value);
    }
    
    private Brush _darkishColour = new SolidColorBrush((Color)ConvertFromString("#130A26"));
    public Brush DarkishColour
    {
        get => _darkishColour;
        private set => SetField(ref _darkishColour, value);
    }
    
    private Brush _darkColour = new SolidColorBrush((Color)ConvertFromString("#0E071C"));
    public Brush DarkColour
    {
        get => _darkColour;
        private set => SetField(ref _darkColour, value);
    }
    
    private Brush _darkerColour = new SolidColorBrush((Color)ConvertFromString("#0A0514"));
    public Brush DarkerColour
    {
        get => _darkerColour;
        private set => SetField(ref _darkerColour, value);
    }

    private Brush _accentColour = new SolidColorBrush((Color)ConvertFromString("#BF00FF"));
    public Brush AccentColour
    {
        get => _accentColour;
        private set => SetField(ref _accentColour, value);
    }

    public event PropertyChangedEventHandler? PropertyChanged;
    private void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    private void SetField<T>(ref T field, T value, [CallerMemberName] string? propertyName = null)
    {
        if (EqualityComparer<T>.Default.Equals(field, value)) return;
        field = value;
        OnPropertyChanged(propertyName);
    }
}