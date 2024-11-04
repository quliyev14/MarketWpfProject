using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using MarketWpfProject.Data;
using MarketWpfProject.Hashed;
using MarketWpfProject.Helper.PathHelper;
using MarketWpfProject.Models;
using MarketWpfProject.Moduls;
using System.ComponentModel;
using System.Windows;


namespace MarketWpfProject.ViewModels
{
    public class AdminPanelViewModel : INotifyPropertyChanged
    {
        private string path = "admin.json";
        public RelayCommand LoginCommand { get; set; }

        public AdminPanelViewModel()
        {
            LoginCommand = new RelayCommand(SignIn);
        }

        private string? _email;

        public string? Email
        {
            get => _email;
            set
            {
                _email = value;
                OnPropertyChanged(nameof(Email));
            }
        }

        private string? _password;

        public string? Password
        {
            get => _password;
            set
            {
                _password = value;
                OnPropertyChanged(nameof(Password));
            }
        }

        public void SignIn()
        {
            var admins = DB.JsonRead<Admin>(path) ?? new List<Admin>();
            if (!PathCheck.OpenOrClosed(path)) throw new FieldAccessException(nameof(path));

            foreach (var admin in admins)
            {
                if (admin.gmailService.Email == Email && admin.gmailService.Password == DatasIsHashed.WithSHA256PasswordHash(Password))
                {
                    MessageBox.Show("True");
                    return;
                }
            }
            MessageBox.Show("False");


            //if (isAuthenticated) OpenMainWindow();
            RefreshMethod();
        }

        private void RefreshMethod()
        {
            Email = string.Empty;
            Password = string.Empty;
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

    }
}