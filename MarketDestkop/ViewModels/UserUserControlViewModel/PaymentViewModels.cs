using GalaSoft.MvvmLight.Command;
using MarketWpfProject.Views.UserView;

namespace MarketWpfProject.ViewModels.UserUserControlViewModel
{
    public class PaymentViewModels
    {
        public RelayCommand CashPaymentCommand { get; }
        public RelayCommand CardPaymentCommand { get; }

        public PaymentViewModels()
        {
            CardPaymentCommand = new RelayCommand(OpenWithCardPayment);
        }

        private void OpenWithCardPayment()
        {
            CardPayment();
            System.Windows.Application.Current.Windows.OfType<PaymentWindow>().FirstOrDefault()?.Close();
        }

        private void CardPayment()
        {
            var pwc = new PaymentWithCard();
            pwc.Show();
        }
    }
}
