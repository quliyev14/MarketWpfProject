using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using GalaSoft.MvvmLight.Command;
using MarketWpfProject.Data;
using MarketWpfProject.Models;

namespace MarketWpfProject.ViewModels.AdminPanelUserControlViewModel
{
    public class UserViewViewModels : INotifyPropertyChanged
    {
        private string _userPath = "Users.json";

        private static object _prso = new object();
        public ObservableCollection<User> Users { get; set; }
        public RelayCommand DeleteCommand { get; set; }

        private User _selectedUser;
        public User SelectedUser
        {
            get => _selectedUser;
            set
            {
                _selectedUser = value;
                OnPropertyChanged(nameof(SelectedUser));
            }
        }
        public UserViewViewModels()
        {
            Users = new ObservableCollection<User>();
            DeleteCommand = new RelayCommand(UserForDelete);
            LoadUsersFromJson();
        }

        private void UserForDelete()
        {
            if (SelectedUser != null)
            {
                lock (_prso)
                {
                    Users.Remove(SelectedUser);
                    DB.JsonWrite<User>(_userPath, Users);
                }
                SelectedUser = null;
                return;
            }
            MessageBox.Show("Please Listbox item select");
        }

        private void LoadUsersFromJson()
        {
            var users = DB.JsonRead<User>(_userPath);

            if (users is not null)
                foreach (var user in users)
                    Users.Add(user);
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

    }
}
