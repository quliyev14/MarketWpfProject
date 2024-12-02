using System.Windows.Controls;
using MarketWpfProject.ViewModels.AdminPanelUserControlViewModel;

namespace MarketWpfProject.UserControls.AdminUS
{
    public partial class UserViewUserControl : UserControl
    {
        public UserViewUserControl()
        {
            InitializeComponent();
            DataContext = new UserViewViewModels();
        }
    }
}
