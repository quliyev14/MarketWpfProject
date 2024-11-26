using MarketDestkop.Views;
using MarketWpfProject.Command;
using MarketWpfProject.Data;
using MarketWpfProject.Hashed;
using MarketWpfProject.Models;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;

namespace MarketWpfProject.ViewModels.UserUserControlViewModel
{
    public class SignUpViewModels : INotifyPropertyChanged
    {
        private readonly static object _prso = new();

        private string path = "Users.json";
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
            //MessageBoxResult mbb = MessageBox.Show("Data is saved?", "Sign Up", MessageBoxButton.YesNo, MessageBoxImage.Question);

            //if (User is null) MessageBox.Show("User is null Please auto fill");

            var user = new User()
            {
                Name = User?.Name,
                Surname = User?.Surname,
                GmailService = new GmailService() { Email = User?.GmailService?.Email, Password = DatasIsHashed.WithSHA256PasswordHash(User?.GmailService?.Password) },
                DateTime = User?.DateTime,
                Mobile = User?.Mobile,
                CountryMobileCode = User?.CountryMobileCode
            };

            Users.Add(user);

            //if (mbb == MessageBoxResult.Yes)
            //{
            //lock (_prso)
            DB.JsonWrite<User>(path, Users);
            //ClearFields(parametr);
            //}

        }

        private void ClearFields(object? paraetr) => User = new();
        private void CanselWindow(object? parametr) => CanselSignUpWindow();
        private void CanselSignUpWindow() => Application.Current.Windows.OfType<SignUpWindow>().FirstOrDefault()?.Close();

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged(string? propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}