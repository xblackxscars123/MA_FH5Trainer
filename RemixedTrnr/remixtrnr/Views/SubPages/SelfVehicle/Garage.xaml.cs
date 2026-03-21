using System.Windows.Controls;
using XPaint.ViewModels.Pages;

namespace XPaint.Views.SubPages.SelfVehicle;

public partial class Garage : Page
{
    public Garage()
    {
        ViewModel = new GarageViewModel();
        DataContext = this;
        InitializeComponent();
    }

    public GarageViewModel ViewModel { get; }
}
