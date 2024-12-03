using GalaSoft.MvvmLight.Command;
using MarketWpfProject.Views.UserView;

namespace MarketWpfProject.ViewModels.UserUserControlViewModel
{
    public class PaymentWithCardViewModels
    {
        public RelayCommand CanselCommand { get; }
        public PaymentWithCardViewModels()
        {
            CanselCommand = new RelayCommand(ClosePaymentWithCard);
        }

        private void ClosePaymentWithCard()
        {
            System.Windows.Application.Current.Windows.OfType<PaymentWithCard>().FirstOrDefault()?.Close();

        }
    }
}
