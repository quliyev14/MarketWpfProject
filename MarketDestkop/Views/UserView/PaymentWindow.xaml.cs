using System.Windows;
using MarketWpfProject.ViewModels.UserUserControlViewModel;

namespace MarketWpfProject.Views.UserView
{
    public partial class PaymentWindow : Window
    {
        public PaymentWindow()
        {
            InitializeComponent();
            DataContext = new PaymentViewModels();
        }
    }
}
