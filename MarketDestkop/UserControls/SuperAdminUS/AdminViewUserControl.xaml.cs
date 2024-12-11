using System.Windows.Controls;
using MarketWpfProject.ViewModels.SuperAdminUserControlViewModels;

namespace MarketWpfProject.UserControls.SuperAdminUS
{
    public partial class AdminViewUserControl : UserControl
    {
        public AdminViewUserControl()
        {
            InitializeComponent();
            DataContext = new AdminViewUserControlViewModel();
        }
    }
}
