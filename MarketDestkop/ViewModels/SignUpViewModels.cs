using MarketWpfProject.Command;
using MarketWpfProject.Data;
using MarketWpfProject.Hashed;
using MarketWpfProject.Models;
using System.ComponentModel;

using System.Windows;

namespace MarketWpfProject.ViewModels
{
    public class SignUpViewModels : INotifyPropertyChanged
    {
        private readonly static object _prso = new();

        private string path = "saved.json";
        private string log = "users.log";
        public RelayCommand SignUpCommand { get; }
        public RelayCommand RefreshCommand { get; }

        public SignUpViewModels()
        {
            SignUpCommand = new RelayCommand(SaveJson);
            RefreshCommand = new RelayCommand(RefreshFields);
        }

        #region Propdbs

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

        private string? _countrycode;

        public string? CountryCode
        {
            get => _countrycode;
            set
            {
                _countrycode = value;
                OnPropertyChanged(nameof(CountryCode));
            }
        }

        private string? _mobile;

        public string? Mobile
        {
            get => _mobile;
            set
            {
                _mobile = value;
                OnPropertyChanged(nameof(Mobile));
            }
        }

        private string? _gender;

        public string? Gender
        {
            get => _gender;
            set
            {
                if (_gender != value)
                {
                    _gender = value;
                    OnPropertyChanged(nameof(Gender));
                    OnPropertyChanged(nameof(IsMale));
                    OnPropertyChanged(nameof(IsFemale));
                }
            }
        }

        public bool IsMale
        {
            get => Gender == "Male";
            set
            {
                if (value)
                    Gender = "Male";
            }
        }

        public bool IsFemale
        {
            get => Gender == "Female";
            set
            {
                if (value)
                    Gender = "Female";
            }
        }

        private DateOnly? _birthday;
        public DateOnly? Birthday
        {
            get => _birthday;
            set
            {
                _birthday = value;
                OnPropertyChanged(nameof(Birthday));
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
        #endregion


        private void SaveJson(object? parametr)
        {
            MessageBoxResult mbb = MessageBox.Show("Data is saved?", "Sign Up", MessageBoxButton.YesNo, MessageBoxImage.Question);

            var users = new List<User>
            {
                 new User(Name, Surname, new GmailService(Email, DatasIsHashed.WithSHA256PasswordHash(Password)), Gender, Birthday, Mobile, CountryCode)
            };

            if (mbb == MessageBoxResult.Yes)
            {
                lock (_prso)
                    DB.JsonWrite<User>(path, log, users);
                ClearFields();
            }
        }

        private void ClearFields()
        {
            Name = string.Empty;
            Surname = string.Empty;
            Email = string.Empty;
            Password = string.Empty;
            Mobile = string.Empty;
            CountryCode = string.Empty;
            IsMale = false;
            IsFemale = false;
            Birthday = null;
        }

        private void RefreshFields(object? parametr) => ClearFields();


        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged(string? propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}