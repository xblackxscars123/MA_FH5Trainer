using System.Windows.Controls;
<<<<<<< C:/Users/GAMING/Documents/GitHub/MA_FH5Trainer/MA_FH5Trainer/MA_FH5Trainer/Views/SubPages/SelfVehicle/Autoshow.xaml.cs
<<<<<<< C:/Users/GAMING/Documents/GitHub/MA_FH5Trainer/MA_FH5Trainer/MA_FH5Trainer/Views/SubPages/SelfVehicle/Autoshow.xaml.cs
<<<<<<< C:/Users/GAMING/Documents/GitHub/MA_FH5Trainer/MA_FH5Trainer/MA_FH5Trainer/Views/SubPages/SelfVehicle/Autoshow.xaml.cs
<<<<<<< C:/Users/GAMING/Documents/GitHub/MA_FH5Trainer/MA_FH5Trainer/MA_FH5Trainer/Views/SubPages/SelfVehicle/Autoshow.xaml.cs
<<<<<<< C:/Users/GAMING/Documents/GitHub/MA_FH5Trainer/MA_FH5Trainer/MA_FH5Trainer/Views/SubPages/SelfVehicle/Autoshow.xaml.cs
<<<<<<< C:/Users/GAMING/Documents/GitHub/MA_FH5Trainer/MA_FH5Trainer/MA_FH5Trainer/Views/SubPages/SelfVehicle/Autoshow.xaml.cs
<<<<<<< C:/Users/GAMING/Documents/GitHub/MA_FH5Trainer/MA_FH5Trainer/MA_FH5Trainer/Views/SubPages/SelfVehicle/Autoshow.xaml.cs
<<<<<<< C:/Users/GAMING/Documents/GitHub/MA_FH5Trainer/MA_FH5Trainer/MA_FH5Trainer/Views/SubPages/SelfVehicle/Autoshow.xaml.cs
<<<<<<< C:/Users/GAMING/Documents/GitHub/MA_FH5Trainer/MA_FH5Trainer/MA_FH5Trainer/Views/SubPages/SelfVehicle/Autoshow.xaml.cs
using MA_FH5Trainer.ViewModels.Pages;
using MA_FH5Trainer.Views.Windows;
=======
using XPaint.ViewModels.Pages;
using XPaint.Views.Windows;
>>>>>>> C:/Users/GAMING/.windsurf/worktrees/MA_FH5Trainer/MA_FH5Trainer-4284e8b3/MA_FH5Trainer/MA_FH5Trainer/Views/SubPages/SelfVehicle/Autoshow.xaml.cs
=======
using XPaint.ViewModels.Pages;
using XPaint.Views.Windows;
>>>>>>> C:/Users/GAMING/.windsurf/worktrees/MA_FH5Trainer/MA_FH5Trainer-4284e8b3/MA_FH5Trainer/MA_FH5Trainer/Views/SubPages/SelfVehicle/Autoshow.xaml.cs
=======
using XPaint.ViewModels.Pages;
using XPaint.Views.Windows;
>>>>>>> C:/Users/GAMING/.windsurf/worktrees/MA_FH5Trainer/MA_FH5Trainer-4284e8b3/MA_FH5Trainer/MA_FH5Trainer/Views/SubPages/SelfVehicle/Autoshow.xaml.cs
=======
using XPaint.ViewModels.Pages;
using XPaint.Views.Windows;
>>>>>>> C:/Users/GAMING/.windsurf/worktrees/MA_FH5Trainer/MA_FH5Trainer-4284e8b3/MA_FH5Trainer/MA_FH5Trainer/Views/SubPages/SelfVehicle/Autoshow.xaml.cs
=======
using XPaint.ViewModels.Pages;
using XPaint.Views.Windows;
>>>>>>> C:/Users/GAMING/.windsurf/worktrees/MA_FH5Trainer/MA_FH5Trainer-4284e8b3/MA_FH5Trainer/MA_FH5Trainer/Views/SubPages/SelfVehicle/Autoshow.xaml.cs
=======
using XPaint.ViewModels.Pages;
using XPaint.Views.Windows;
>>>>>>> C:/Users/GAMING/.windsurf/worktrees/MA_FH5Trainer/MA_FH5Trainer-4284e8b3/MA_FH5Trainer/MA_FH5Trainer/Views/SubPages/SelfVehicle/Autoshow.xaml.cs
=======
using XPaint.ViewModels.Pages;
using XPaint.Views.Windows;
>>>>>>> C:/Users/GAMING/.windsurf/worktrees/MA_FH5Trainer/MA_FH5Trainer-4284e8b3/MA_FH5Trainer/MA_FH5Trainer/Views/SubPages/SelfVehicle/Autoshow.xaml.cs
=======
using XPaint.ViewModels.Pages;
using XPaint.Views.Windows;
>>>>>>> C:/Users/GAMING/.windsurf/worktrees/MA_FH5Trainer/MA_FH5Trainer-90665ca8/MA_FH5Trainer/MA_FH5Trainer/Views/SubPages/SelfVehicle/Autoshow.xaml.cs
=======
using XPaint.ViewModels.Pages;
using XPaint.Views.Windows;
>>>>>>> C:/Users/GAMING/.windsurf/worktrees/MA_FH5Trainer/MA_FH5Trainer-90665ca8/MA_FH5Trainer/MA_FH5Trainer/Views/SubPages/SelfVehicle/Autoshow.xaml.cs
using MahApps.Metro.Controls;

namespace XPaint.Views.SubPages.SelfVehicle;

public partial class Autoshow : Page
{
    public Autoshow()
    {
        ViewModel = new AutoshowViewModel();
        DataContext = this;
        
        InitializeComponent();
    }
    
    public AutoshowViewModel ViewModel { get; }
}