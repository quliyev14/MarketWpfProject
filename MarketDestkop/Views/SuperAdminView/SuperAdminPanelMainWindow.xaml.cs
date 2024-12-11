using System.Windows;
using MarketWpfProject.ViewModels;

namespace MarketWpfProject.Views.SuperAdminView
{
    public partial class SuperAdminPanelMainWindow : Window
    {
        public SuperAdminPanelMainWindow()
        {
            InitializeComponent();
            var sapwm = new SuperAdminPanelWewModels(ContentFrame);
            DataContext = sapwm;
        }
    }
}
