 using MarketDestkop;
using MarketDestkop.Views;
using MarketWpfProject.Command;
using MarketWpfProject.Data;
using MarketWpfProject.Hashed;
using MarketWpfProject.Models;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Windows;

namespace MarketWpfProject.ViewModels.UserUserControlViewModel
{
    public class SignUpViewModels : INotifyPropertyChanged
    {
        private readonly static object _prso = new();

        private string _userPath = App.UserPath;

        private string log = "users.log";
        public RelayCommand SignUpCommand { get; }
        public RelayCommand RefreshCommand { get; }
        public RelayCommand CanselCommand { get; }
        public ObservableCollection<User> Users { get; set; }

        private User? _user;
        public User? User { get => _user; set { _user = value; OnPropertyChanged(nameof(User)); } }

        public SignUpViewModels()
        {
            Users = new ObservableCollection<User>();
            SignUpCommand = new RelayCommand(SaveJson);
            CanselCommand = new RelayCommand(CanselWindow);
            RefreshCommand = new RelayCommand(ClearFields);
            User = new();
        }

        private void SaveJson(object? parametr)
        {
            MessageBoxResult mbb = MessageBox.Show("Data is saved?", "Sign Up", MessageBoxButton.YesNo, MessageBoxImage.Question);

            var users = DB.JsonRead<User>(_userPath) ?? throw new FileNotFoundException(nameof(_userPath));

            var user = new User()
            {
                Name = User?.Name,
                Surname = User?.Surname,
                GmailService = new GmailService() { Email = User?.GmailService?.Email, Password = DatasIsHashed.WithSHA256PasswordHash(User?.GmailService?.Password) },
                DateTime = User?.DateTime,
                Mobile = User?.Mobile,
                Balance = 0,
                CountryMobileCode = User?.CountryMobileCode
            };
            users.Add(user);
            if (mbb == MessageBoxResult.Yes)
            {
                lock (_prso)
                    DB.JsonWrite<User>(_userPath, users);
                ClearFields(parametr);
            }
        }
        private void ClearFields(object? parametr) => User = new();
        private void CanselWindow(object? parametr) => CanselSignUpWindow();
        private void CanselSignUpWindow() => Application.Current.Windows.OfType<SignUpWindow>().FirstOrDefault()?.Close();

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged(string? propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}