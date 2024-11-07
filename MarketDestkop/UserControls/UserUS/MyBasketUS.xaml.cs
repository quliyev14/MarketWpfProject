using MarketWpfProject.ViewModels.UserUserControlViewModel;
using System.Windows.Controls;

namespace MarketWpfProject.UserControls.UserUS
{
    public partial class MyBasketUS : UserControl
    {
        public MyBasketUS()
        {
            InitializeComponent();
            DataContext = new MyBasketViewModel();
        }
    }
}
