using MarketDestkop.Views;
using MarketWpfProject.Command;
using MarketWpfProject.Data;
using MarketWpfProject.Hashed;
using MarketWpfProject.Helper.PathHelper;
using MarketWpfProject.Models;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;

namespace MarketWpfProject.ViewModels
{
    public class SignInViewModels : INotifyPropertyChanged
    {
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
            var users = DB.JsonRead<User>(path) ?? new List<User>();
            string hashpassword = DatasIsHashed.PasswordHash(Password);

            if (!PathCheck.OpenOrClosed(path)) throw new FieldAccessException(nameof(path));

            else
            {
                foreach (var user in users)
                {
                    if (user.GmailService.Email == _email && user.GmailService.Password == hashpassword)
                    {
                        MessageBox.Show("True");
                    }
                }
                MessageBox.Show("False");
            }
        }

        public void OpenSignUpWindow(object? parametr)
        {
            var signUpWindow = new SignUpWindow();
            signUpWindow.Show();
            //signUpWindow.ShowDialog();
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}