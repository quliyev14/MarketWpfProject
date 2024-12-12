using GalaSoft.MvvmLight.Command;
using MarketDestkop.Views;
using MarketWpfProject.Views;
using MarketWpfProject.Views.UserView;
using System.Windows.Forms;

namespace MarketWpfProject.ViewModels.UserUserControlViewModel
{
    public class PaymentViewModels
    {
        public RelayCommand CashPaymentCommand { get; }
        public RelayCommand CardPaymentCommand { get; }

        public PaymentViewModels()
        {
            CardPaymentCommand = new RelayCommand(OpenWithCardPayment);
            CashPaymentCommand = new RelayCommand(CashPayment); 
        }
        private void OpenWithCardPayment()
        {
            CardPayment();
            QuitPaymentWindow();
        }

        private void QuitPaymentWindow() => System.Windows.Application.Current.Windows.OfType<PaymentWindow>().FirstOrDefault()?.Close();
        private void CardPayment()
        {
            var pwc = new PaymentWithCard();
            pwc.Show();
        }
        private void CashPayment()
        {
            MessageBox.Show("Succesfuly");
            QuitPaymentWindow();
            OpenSignIn();
            QuitMainWindow();
        }

        private void OpenSignIn()
        {
            var rw = new RegisterWindow();
            rw.Show();
        }
        private void QuitMainWindow() => System.Windows.Application.Current.Windows.OfType<MainWindow>().FirstOrDefault()?.Close();




    }
}
