using MarketDestkop.Views;
using MarketWpfProject.Command;
using MarketWpfProject.Data;
using MarketWpfProject.Hashed;
using MarketWpfProject.Helper.PathHelper;
using MarketWpfProject.Models;
using MarketWpfProject.Views;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace MarketWpfProject.ViewModels.UserUserControlViewModel
{
    public class SignInViewModels : INotifyPropertyChanged
    {
        private const string path = "saved.json";
        public RelayCommand LoginCommand { get; }
        public RelayCommand SignUpCommand { get; }

        private User _user;
        public User User { get => _user; set { _user = value; OnPropertyChanged(nameof(User)); } }

        public SignInViewModels()
        {
            LoginCommand = new RelayCommand(SignIn);
            SignUpCommand = new RelayCommand(OpenSignUpWindow);
            User = new();
        }

        public async void SignIn(object? parametr)
        {
            var users = await DB.JsonRead<User>(path) ?? new List<User>();
            if (!PathCheck.OpenOrClosed(path)) throw new FieldAccessException(nameof(path));

            var isAuthenticated = users.Any(user =>
                user.GmailService.Email == User.GmailService.Email &&
                user.GmailService.Password == DatasIsHashed.WithSHA256PasswordHash(User.GmailService.Password));

            if (isAuthenticated) OpenMainWindow();

            User = new();
            RegisterCloseWindow();
        }

        private void RegisterCloseWindow() => System.Windows.Application.Current.Windows.OfType<RegisterWindow>().FirstOrDefault()?.Close();

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