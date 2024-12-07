using GalaSoft.MvvmLight.Command;
using MarketDestkop;
using MarketDestkop.Views;
using MarketWpfProject.Data;
using MarketWpfProject.Models;
using MarketWpfProject.UserControls.UserUS;
using MarketWpfProject.Views;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace MarketWpfProject.ViewModels.UserUserControlViewModel
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        public RelayCommand ProductsCommand { get; }
        public RelayCommand MyBasketCommand { get; }
        public RelayCommand SignOutCommand { get; }
        public RelayCommand GoBackCommand { get; }
        public RelayCommand WishlistCommand { get; }
        public RelayCommand ProfileSettingsCommand { get; }
        public RelayCommand HistoryCommand { get; }

        private readonly Frame? _frame;

        private string? _nameSurname;
        public string? NameAndSurname { get => _nameSurname; set { _nameSurname = value; OnPropertyChanged(nameof(NameAndSurname)); } }

        private string? _phone;
        public string? Phone { get => _phone; set { _phone = value; OnPropertyChanged(nameof(Phone)); } }

        private string? _email;
        public string? Email { get => _email; set { _email = value; OnPropertyChanged(nameof(Email)); } }

        private string? _userfullname;
        public string? UserFullName { get => _userfullname; set { _userfullname = value; OnPropertyChanged(nameof(UserFullName)); } }

        private decimal? _balance;
        public decimal? Balance
        {
            get => _balance;
            set
            {
                _balance = value;
                OnPropertyChanged(nameof(Balance));
                UpdateBalanceInJson();
            }
        }

        public MainWindowViewModel(Frame frame)
        {
            _frame = frame;
            ProductsCommand = new RelayCommand(OpenProductsUserControl);
            MyBasketCommand = new RelayCommand(OpenMyBasketUserControl);
            SignOutCommand = new RelayCommand(UserSignOut);
            GoBackCommand = new RelayCommand(GoBackRegisterWindow);
            ProfileSettingsCommand = new RelayCommand(ProfilSetting);
            WishlistCommand = new RelayCommand(OpenMyFavoriteProduct);
            HistoryCommand = new RelayCommand(OpenHistory);
            NameAndSurname = $"{App.CurrentUser?.Name} {App.CurrentUser?.Surname}";
            Email = App.CurrentUser?.GmailService?.Email;
            Phone = App.CurrentUser?.Mobile;
            UserFullName = $"{App.CurrentUser?.Name?[0]} {App.CurrentUser?.Surname?[0]}";
            Balance = App.CurrentUser?.Balance;
        }

        private void OpenProductsUserControl() => _frame?.Navigate(new ProductUS());
        private void OpenHistory() => _frame?.Navigate(new TheHistoryOfTheProductsIBought());
        private void OpenMyFavoriteProduct() => _frame?.Navigate(new MyFavoriteProduct());
        private void OpenMyBasketUserControl() => _frame?.Navigate(new MyBasketUS());
        private void ProfilSetting() => _frame?.Navigate(new ProfileSettingUS());
        private void GoBackRegisterWindow()
        {
            OpenRegisterWindow();
            MainWindowExit();
        }
        private void MainWindowExit() => Application.Current.Windows.OfType<MainWindow>().FirstOrDefault()?.Close();
        private void OpenRegisterWindow()
        {
            var rw = new RegisterWindow();
            rw.Show();
        }
        private void UserSignOut()
        {
            MessageBoxResult mbr = MessageBox.Show("Sign Out?", "Sign Out", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (mbr == MessageBoxResult.Yes)
                Application.Current.Shutdown();
        }

        private void UpdateBalanceInJson()
        {
            if (App.CurrentUser == null)
                return;

            var usersPath = App.UserPath;
            var allUsers = DB.JsonRead<User>(usersPath) ?? new List<User>();

            var currentUser = allUsers.FirstOrDefault(u => u.GmailService.Email == App.CurrentUser.GmailService.Email);
            if (currentUser != null)
            {
                currentUser.Balance = Balance ?? 0m; 
            }

            DB.JsonWrite(usersPath, allUsers); 
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged(string? propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
