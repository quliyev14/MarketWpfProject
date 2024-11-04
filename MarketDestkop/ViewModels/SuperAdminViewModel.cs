using GalaSoft.MvvmLight.Command;
using MarketWpfProject.Data;
using MarketWpfProject.Hashed;
using MarketWpfProject.Models;
using MarketWpfProject.Moduls;
using System.ComponentModel;

using System.Windows;

namespace MarketWpfProject.ViewModels
{
    public class SuperAdminViewModel : INotifyPropertyChanged
    {
        private static readonly object _psro = new();

        private string path = "admin.json";
        private string log = "admin.log";
        public RelayCommand SignUpCommand { get; }
        public RelayCommand RefreshCommand { get; }


        public SuperAdminViewModel()
        {
            RefreshCommand = new RelayCommand(ClearFields);
            SignUpCommand = new RelayCommand(SaveJson);
        }


        private string? _name;

        public string? Name
        {
            get => _name;
            set
            {
                _name = value;
                OnPropertyChanged(nameof(Name));
            }
        }

        private string? _surname;

        public string? Surname
        {
            get => _surname;
            set
            {
                _surname = value;
                OnPropertyChanged(nameof(Surname));
            }
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

        private void SaveJson()
        {
            MessageBoxResult mbb = MessageBox.Show("Data is saved?", "Sign Up", MessageBoxButton.YesNo, MessageBoxImage.Question);
            var admins = new List<Admin>
            {
                new Admin(Name,Surname,new GmailService(Email,DatasIsHashed.WithSHA256PasswordHash(Password)))
            };

            if (mbb == MessageBoxResult.Yes)
            {
                lock (_psro)
                    DB.JsonWrite<Admin>(path, log, admins);
                ClearFields();
            }
            return;
        }

        private void ClearFields()
        {
            Name = string.Empty;
            Surname = string.Empty;
            Email = string.Empty;
            Password = string.Empty;
        }


        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
