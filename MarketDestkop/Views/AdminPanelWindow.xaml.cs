using MarketWpfProject.ViewModels;
using System.Windows;

namespace MarketWpfProject.Views
{
    public partial class AdminPanelWindow : Window
    {
        public AdminPanelWindow()
        {
            InitializeComponent();
            DataContext = new AdminPanelViewModel();
        }
    }
}