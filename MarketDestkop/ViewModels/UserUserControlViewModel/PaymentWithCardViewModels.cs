using System.ComponentModel;
using System.Windows;
using GalaSoft.MvvmLight.Command;
using MarketDestkop;
using MarketDestkop.Views;
using MarketWpfProject.Data;
using MarketWpfProject.Helper;
using MarketWpfProject.Models;
using MarketWpfProject.Moduls;
using MarketWpfProject.Views;
using MarketWpfProject.Views.UserView;

namespace MarketWpfProject.ViewModels.UserUserControlViewModel
{
    public class PaymentWithCardViewModels : INotifyPropertyChanged
    {
        private decimal? _totalAmount;
        private decimal? _userPayment;
        private decimal? _remainingAmount;
        private string? _cardHolder;
        private string? _cardNumber;
        private string? _cardCVC;
        //private string? _mmyy;
        public RelayCommand CancelCommand { get; }
        public decimal? TotalAmount { get => _totalAmount; set { _totalAmount = value; OnPropertyChanged(nameof(TotalAmount)); UpdateRemainingAmount(); } }
        public string? CardHolder { get => _cardHolder; set { _cardHolder = value; OnPropertyChanged(nameof(CardHolder)); } }
        public decimal? UserPayment { get => _userPayment; set { _userPayment = value; OnPropertyChanged(nameof(UserPayment)); UpdateRemainingAmount(); } }
        public decimal? RemainingAmount { get => _remainingAmount; private set { _remainingAmount = value; OnPropertyChanged(nameof(RemainingAmount)); } }
        public string? CardNumber { get => _cardNumber; set { _cardNumber = value; OnPropertyChanged(nameof(CardNumber)); } }
        public string? CvC { get => _cardCVC; set { _cardCVC = value; OnPropertyChanged(nameof(CvC)); } }
        public decimal? Balance
        {
            get => App.CurrentUser?.Balance ?? 0m;
            set
            {
                if (App.CurrentUser != null)
                {
                    App.CurrentUser.Balance = value;
                    OnPropertyChanged(nameof(Balance));
                }
            }
        }
        private string _userFileName = $"{App.CurrentUser?.GmailService.Email}.json";
        private string userHistoryFileName = $"{App.CurrentUser?.GmailService.Email}_PurchasedHistory.json";
        private string _userPath = App.UserPath;
        public RelayCommand SubmitPaymentCommand { get; }
        public RelayCommand GoBackCommand { get; }
        public PaymentWithCardViewModels()
        {
            //_frame = frame;
            TotalAmount = App.TotalAmount;
            CardHolder = $"{App.CurrentUser?.Surname} {App.CurrentUser?.Name}";
            UserPayment = 0m;
            CardNumber = RandomCardGenerator.RandomForCvcAndCardNumber(16) ?? "4169 9178 9017 7612";
            CvC = RandomCardGenerator.RandomForCvcAndCardNumber(3) ?? "187";
            //MMYY = "04/24";
            CancelCommand = new RelayCommand(PaymentWindowQuit);
            SubmitPaymentCommand = new RelayCommand(SubmitPayment);
            GoBackCommand = new RelayCommand(GoOpenPaymentWindow);
            TotalAmount -= App.CurrentUser?.Balance;
        }
        private void SubmitPayment()
        {
            if (UserPayment >= TotalAmount)
            {
                var changeAmount = UserPayment - TotalAmount;
                Balance = changeAmount;

                UpdateUserBalanceInJson();

                MessageBox.Show("Payment successful", "Success", MessageBoxButton.OK, MessageBoxImage.Information);

                MoveBoughtProductsToHistory();
                ClearUserBasket();
                PaymentWindowQuit();
                QuitMainWindow();
                OpenSignIn();
            }
            else
                RemainingAmount = TotalAmount - UserPayment;
        }

        private void UpdateUserBalanceInJson()
        {
            var allUsers = DB.JsonRead<User>(_userPath) ?? new List<User>();

            var currentUser = allUsers.FirstOrDefault(u => u.GmailService.Email == App.CurrentUser?.GmailService.Email);

            if (currentUser is not null)
                currentUser.Balance = Balance;

            else
            {
                allUsers.Add(new User
                {
                    Name = App.CurrentUser?.Name,
                    Surname = App.CurrentUser?.Surname,
                    GmailService = new GmailService()
                    {
                        Email = App.CurrentUser?.GmailService.Email,
                        Password = App.CurrentUser?.GmailService?.Password
                    },
                    Mobile = App.CurrentUser?.Mobile,
                    Balance = Balance
                });
            }
            DB.JsonWrite(_userPath, allUsers);
        }
        private void GoOpenPaymentWindow()
        {
            OpenPaymentWindow();
            PaymentWindowQuit();
        }
        private void ClearUserBasket() => DB.JsonWrite(_userFileName, new List<Product>());
        private void MoveBoughtProductsToHistory()
        {
            var userBasketProducts = DB.JsonRead<Product>(_userFileName) ?? new List<Product>();

            if (userBasketProducts.Count == 0)
            {
                MessageBox.Show("Basket is empty. Nothing to save to history.", "Info", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var purchaseHistory = DB.JsonRead<PurchaseHistory>(userHistoryFileName) ?? new List<PurchaseHistory>();

            var newPurchase = new PurchaseHistory
            {
                PurchaseDate = DateTime.Now,
                Products = userBasketProducts
            };

            purchaseHistory.Add(newPurchase);

            DB.JsonWrite(userHistoryFileName, purchaseHistory);
        }
        private void OpenPaymentWindow()
        {
            var pw = new PaymentWindow();
            pw.Show();
        }

        private void OpenSignIn()
        {
            var rw = new RegisterWindow();
            rw.Show();
        }
        private void QuitMainWindow() => System.Windows.Application.Current.Windows.OfType<MainWindow>().FirstOrDefault()?.Close();
        private void UpdateRemainingAmount() => RemainingAmount = TotalAmount - UserPayment;
        private void PaymentWindowQuit() => System.Windows.Application.Current.Windows.OfType<PaymentWithCard>().FirstOrDefault()?.Close();
        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}