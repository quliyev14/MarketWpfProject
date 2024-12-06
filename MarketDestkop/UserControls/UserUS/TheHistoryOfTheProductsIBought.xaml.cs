using System.Windows.Controls;
using MarketWpfProject.ViewModels.UserUserControlViewModel;

namespace MarketWpfProject.UserControls.UserUS
{
    public partial class TheHistoryOfTheProductsIBought : UserControl
    {
        public TheHistoryOfTheProductsIBought()
        {
            InitializeComponent();
            DataContext = new TheHistoryOfTheProductsIBoughtViewModels();
        }
    }
}
