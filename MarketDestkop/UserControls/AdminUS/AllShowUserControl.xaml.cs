using MarketWpfProject.ViewModels;
using MarketWpfProject.ViewModels.AdminPanelUserControlViewModel;
using System.Windows.Controls;

namespace MarketWpfProject.UserControls.AdminUS
{
    public partial class AllShowUserControl : UserControl
    {
        public AllShowUserControl()
        {
            InitializeComponent();
            DataContext = new AddProductViewModel();
        }
    }
}
