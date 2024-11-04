using MarketWpfProject.ViewModels.AdminPanelUserControlViewModel;
using System.Windows.Controls;

namespace MarketWpfProject.UserControls.AdminUS
{
    public partial class AddProductUserControl : UserControl
    {
        public AddProductUserControl()
        {
            InitializeComponent();
            DataContext = new AddProductViewModel();
        }
    }
}
