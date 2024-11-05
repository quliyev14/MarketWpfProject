using MarketWpfProject.ViewModels;
using System.Windows.Controls;

namespace MarketWpfProject.UserControls.AdminUS
{
    public partial class AllShowUserControl : UserControl
    {
        public AllShowUserControl()
        {
            InitializeComponent();
            var viewModel = new MainAdminPanelViewModel(ContentFrame);
            DataContext = viewModel;
        }
    }
}
