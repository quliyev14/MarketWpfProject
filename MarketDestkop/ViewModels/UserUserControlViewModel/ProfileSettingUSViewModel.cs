using System.ComponentModel;
using System.Windows;
using GalaSoft.MvvmLight.Command;
using MarketDestkop;
using MarketWpfProject.Data;
using MarketWpfProject.Hashed;
using MarketWpfProject.Models;

namespace MarketWpfProject.ViewModels.UserUserControlViewModel
{
    public class ProfileSettingUSViewModel : INotifyPropertyChanged
    {
        public RelayCommand SaveCommand { get; }

        private User? _user;
        public User? User { get => _user; set { _user = value; OnPropertyChanged(nameof(User)); } }

        public ProfileSettingUSViewModel()
        {
            User = App.CurrentUser;
            User!.GmailService.Password = App.Password;
            SaveCommand = new RelayCommand(UserDataUpdateSave);
        }

        private void UserDataUpdateSave()
        {
            if (User is null) return;

            var users = DB.JsonRead<User>("Users.json");

            foreach (var existingUser in users)
            {
                if (existingUser.GmailService.Email == User.GmailService.Email) 
                {
                    existingUser.Name = User.Name;
                    existingUser.Surname = User.Surname;
                    existingUser.GmailService.Password = DatasIsHashed.WithSHA256PasswordHash(User.GmailService.Password);
                    existingUser.Mobile = User.Mobile;
                    break;
                }
            }

            DB.JsonWrite("Users.json", users);
            MessageBox.Show("Operation succesfully completed!", "Succesfuly", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged(string? propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}