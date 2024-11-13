using GalaSoft.MvvmLight.Command;
using MarketWpfProject.Data;
using MarketWpfProject.Hashed;
using MarketWpfProject.Models;
using MarketWpfProject.Moduls;
using MarketWpfProject.Views;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;

namespace MarketWpfProject.ViewModels.AdminPanelUserControlViewModel
{
    public class SuperAdminViewModel : INotifyPropertyChanged
    {
        private static readonly object _psro = new();

        private string path = "admin.json";
        private string log = "admin.log";
        public RelayCommand SignUpCommand { get; }
        public RelayCommand RefreshCommand { get; }
        public RelayCommand CancelCommand { get; }

        private Admin _admin;
        public Admin Admin { get => _admin; set { _admin = value; OnPropertyChanged(nameof(Admin)); } }

        public ObservableCollection<Admin> Admins { get; set; }

        public SuperAdminViewModel()
        {
            Admins = new ObservableCollection<Admin>();
            RefreshCommand = new RelayCommand(ClearFields);
            SignUpCommand = new RelayCommand(SaveJson);
            CancelCommand = new RelayCommand(WindowCansel);
            Admin = new();
        }

        private void SaveJson()
        {
            MessageBoxResult mbb = MessageBox.Show("Data is saved?", "Sign Up", MessageBoxButton.YesNo, MessageBoxImage.Question);

            var admin = new Admin()
            {
                Name = Admin.Name,
                Surname = Admin.Surname,
                GmailService = new GmailService() { Email = Admin.GmailService?.Email, Password = DatasIsHashed.WithSHA256PasswordHash(Admin.GmailService?.Password) },
            };

            Admins.Add(admin);

            if (mbb == MessageBoxResult.Yes)
            {
                lock (_psro)
                    DB.JsonWrite<Admin>(path, log, Admins);
            }
            ClearFields();
            return;
        }

        private void ClearFields() => Admin = new();

        private void WindowCansel() => Application.Current.Windows.OfType<SuperAdminPanelWindow>().FirstOrDefault()?.Close();

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}