using System.Windows;
using MarketWpfProject.ViewModels.UserUserControlViewModel;

namespace MarketWpfProject.Views.UserView
{
    public partial class PaymentWithCard : Window
    {
        public PaymentWithCard()
        {
            InitializeComponent();
            DataContext = new PaymentWithCardViewModels();
        }
    }
}
