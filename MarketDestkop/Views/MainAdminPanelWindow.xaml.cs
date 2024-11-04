using MarketWpfProject.ViewModels;
using System.Windows;

namespace MarketWpfProject.Views
{
    public partial class MainAdminPanelWindow : Window
    {
        public MainAdminPanelWindow()
        {
            InitializeComponent();
            var viewModel = new MainAdminPanelViewModel(ContentFrame);
            DataContext = viewModel;
        }
    }
}
