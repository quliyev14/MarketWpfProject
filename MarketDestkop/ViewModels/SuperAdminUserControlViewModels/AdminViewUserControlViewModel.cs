using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using GalaSoft.MvvmLight.Command;
using MarketWpfProject.Data;
using MarketWpfProject.Moduls;

namespace MarketWpfProject.ViewModels.SuperAdminUserControlViewModels
{
    public class AdminViewUserControlViewModel
    {
        private string _userPath = "admin.json";

        private static object _prso = new object();
        public ObservableCollection<Admin> Admins { get; set; }
        public RelayCommand DeleteCommand { get; set; }

        private Admin _selectedadmin;
        public Admin SelectedAdmin
        {
            get => _selectedadmin;
            set
            {
                _selectedadmin = value;
                OnPropertyChanged(nameof(SelectedAdmin));
            }
        }
        public AdminViewUserControlViewModel()
        {
            Admins = new ObservableCollection<Admin>();
            DeleteCommand = new RelayCommand(UserForDelete);
            LoadUsersFromJson();
        }
        private void UserForDelete()
        {
            if (SelectedAdmin != null)
            {
                lock (_prso)
                {
                    Admins.Remove(SelectedAdmin);
                    DB.JsonWrite<Admin>(_userPath, Admins);
                }
                SelectedAdmin = null;
                return;
            }
            MessageBox.Show("Please Listbox item select");
        }
        private void LoadUsersFromJson()
        {
            var admins = DB.JsonRead<Admin>(_userPath);

            if (admins is not null)
                foreach (var admin in admins)
                    Admins.Add(admin);
        }
        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
