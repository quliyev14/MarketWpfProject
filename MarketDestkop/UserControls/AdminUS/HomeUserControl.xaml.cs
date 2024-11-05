using MarketWpfProject.ViewModels.AdminPanelUserControlViewModel;
using System.Windows.Controls;

namespace MarketWpfProject.UserControls.AdminUS
{
    public partial class HomeUserControl : UserControl
    {
        public HomeUserControl()
        {
            InitializeComponent();
            DataContext = new AddProductViewModel();
        }
    }
}