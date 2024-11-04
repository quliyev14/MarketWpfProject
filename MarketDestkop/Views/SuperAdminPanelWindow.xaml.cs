using MarketWpfProject.ViewModels;
using System.Windows;


namespace MarketWpfProject.Views
{
    public partial class SuperAdminPanelWindow : Window
    {
        public SuperAdminPanelWindow()
        {
            InitializeComponent();
            DataContext = new SuperAdminViewModel();
        }
    }
}
