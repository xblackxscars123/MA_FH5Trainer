using XPaint.ViewModels.SubPages;

namespace XPaint.Views.SubPages.SelfVehicle;

public partial class Painter
{
    public Painter()
    {
        ViewModel = new PainterViewModel();
        DataContext = this;
        InitializeComponent();
    }

    public PainterViewModel ViewModel { get; }
}
