using System.Windows;
using MarketWpfProject.ViewModels.UserUserControlViewModel;

namespace MarketWpfProject.Views.UserView
{
    public partial class PaymentWithCard : Window
    {
        public PaymentWithCard()
        {
            InitializeComponent();
            var pwcvw = new PaymentWithCardViewModels();
            DataContext = pwcvw;
        }
    }
}
