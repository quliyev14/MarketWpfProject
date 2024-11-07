using MarketDestkop.Views;
using MarketWpfProject.Command;
using MarketWpfProject.Data;
using MarketWpfProject.Hashed;
using MarketWpfProject.Helper.PathHelper;
using MarketWpfProject.Models;
using MarketWpfProject.Views;
using System.ComponentModel;

namespace MarketWpfProject.ViewModels
{
    public class SignInViewModels : INotifyPropertyChanged
    {
        private static readonly object _psro = new object();
        private const string path = "saved.json";
        public RelayCommand LoginCommand { get; }
        public RelayCommand SignUpCommand { get; }

        public SignInViewModels()
        {
            LoginCommand = new RelayCommand(SignIn);
            SignUpCommand = new RelayCommand(OpenSignUpWindow);
        }

        private string _email;

        public string Email
        {
            get => _email;
            set
            {
                _email = value;
                OnPropertyChanged(nameof(Email));
            }
        }

        private string _password;

        public string Password
        {
            get => _password;
            set
            {
                _password = value;
                OnPropertyChanged(nameof(Password));
            }
        }

        public void SignIn(object? parametr)
        {
            lock (_psro)
            {
                var users = DB.JsonRead<User>(path) ?? new List<User>();
                if (!PathCheck.OpenOrClosed(path)) throw new FieldAccessException(nameof(path));

                var isAuthenticated = users.Any(user =>
                    user.GmailService.Email == Email &&
                    user.GmailService.Password == DatasIsHashed.WithSHA256PasswordHash(Password));

                if (isAuthenticated) OpenMainWindow();
                RefreshMethod();
            }
        }

        private void RefreshMethod()
        {
            Email = string.Empty;
            Password = string.Empty;
        }

        public void OpenMainWindow()
        {
            var mainWindow = new MainWindow();
            mainWindow.Show();
        }

        public void OpenSignUpWindow(object? parametr)
        {
            var signUpWindow = new SignUpWindow();
            signUpWindow.Show();
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

    }
}