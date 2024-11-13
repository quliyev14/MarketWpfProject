using MarketDestkop.Views;
using MarketWpfProject.ViewModels;
using MarketWpfProject.ViewModels.AdminPanelUserControlViewModel;
using MarketWpfProject.ViewModels.UserUserControlViewModel;
using System.Windows;

namespace MarketWpfProject.Views
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            var viewmodel = new MainWindowViewModel(ContentFrame);
            DataContext = viewmodel;
        }
    }
}