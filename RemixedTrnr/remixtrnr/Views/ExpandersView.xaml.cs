using System.Windows.Controls;
using XPaint.Converters;
using XPaint.Resources.Theme;
using XPaint.ViewModels.Windows;
using XPaint.Views.Windows;

namespace XPaint.Views;

public partial class ExpandersView : Page
{
    public ExpandersView()
    {
        DataContext = this;
        ViewModel = MainWindow.Instance!.ViewModel;
        Theming = Theming.GetInstance();
        InitializeComponent();
    }
    
    public MainWindowViewModel ViewModel { get; }
    public Theming Theming { get; }
}
