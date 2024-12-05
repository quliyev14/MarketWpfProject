using System.ComponentModel;
using System.Windows;
using GalaSoft.MvvmLight.Command;
using MarketDestkop;
using MarketWpfProject.Helper;
using MarketWpfProject.Views.UserView;

namespace MarketWpfProject.ViewModels.UserUserControlViewModel
{
    public class PaymentWithCardViewModels : INotifyPropertyChanged
    {
        private decimal _totalAmount;
        private decimal _userPayment;
        private decimal _remainingAmount;
        private string? _cardHolder;
        private string? _cardNumber;
        private string? _cardCVC;
        //private string? _mmyy;
        public RelayCommand CancelCommand { get; }
        public decimal TotalAmount { get => _totalAmount; set { _totalAmount = value; OnPropertyChanged(nameof(TotalAmount)); UpdateRemainingAmount(); } }
        public string? CardHolder { get => _cardHolder; set { _cardHolder = value; OnPropertyChanged(nameof(CardHolder)); } }
        public decimal UserPayment { get => _userPayment; set { _userPayment = value; OnPropertyChanged(nameof(UserPayment)); UpdateRemainingAmount(); } }
        public decimal RemainingAmount { get => _remainingAmount; private set { _remainingAmount = value; OnPropertyChanged(nameof(RemainingAmount)); } }
        public string? CardNumber { get => _cardNumber; set { _cardNumber = value; OnPropertyChanged(nameof(CardNumber)); } }
        public string? CvC { get => _cardCVC; set { _cardCVC = value; OnPropertyChanged(nameof(CvC)); } }

        //public string? MMYY { get => _mmyy; private set { _mmyy = value; OnPropertyChanged(nameof(MMYY)); } }

        public RelayCommand SubmitPaymentCommand { get; }
        public PaymentWithCardViewModels()
        {

            TotalAmount = App.TotalAmount;
            CardHolder = $"{App.CurrentUser?.Surname} {App.CurrentUser?.Name}";
            UserPayment = 0m;
            CardNumber = RandomCardGenerator.RandomForCvcAndCardNumber(16) ?? "4169 9178 9017 7612";
            CvC = RandomCardGenerator.RandomForCvcAndCardNumber(3) ?? "187";
            //MMYY = "04/24";
            CancelCommand = new RelayCommand(PaymentWindowQuit);
            SubmitPaymentCommand = new RelayCommand(SubmitPayment);
        }
        private void SubmitPayment()
        {
            if (RemainingAmount <= 0)
            {
                MessageBox.Show("Payment successful", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                PaymentWindowQuit();
                OpenForOrderMap();
            }
            else MessageBox.Show($"Remaining amount: {RemainingAmount:C}", "Payment Incomplete", MessageBoxButton.OK, MessageBoxImage.Warning);
        }
        private void UpdateRemainingAmount() => RemainingAmount = TotalAmount - UserPayment;
        private void OpenForOrderMap()
        {
            var fom = new ForOrderMap();
            fom.Show();
        }
        private void PaymentWindowQuit() => System.Windows.Application.Current.Windows.OfType<PaymentWithCard>().FirstOrDefault()?.Close();

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}