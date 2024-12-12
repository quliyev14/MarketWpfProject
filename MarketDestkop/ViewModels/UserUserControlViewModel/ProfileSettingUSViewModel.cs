using System.ComponentModel;
using System.Windows;
using GalaSoft.MvvmLight.Command;
using MarketDestkop;
using MarketDestkop.Views;
using MarketWpfProject.Data;
using MarketWpfProject.Hashed;
using MarketWpfProject.Models;
using MarketWpfProject.Views;

namespace MarketWpfProject.ViewModels.UserUserControlViewModel
{
    public class ProfileSettingUSViewModel : INotifyPropertyChanged
    {
        private string _userPath = App.UserPath;
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

            var users = DB.JsonRead<User>(_userPath);

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

            DB.JsonWrite(_userPath, users);
            MessageBox.Show("Operation succesfully completed!", "Succesfuly", MessageBoxButton.OK, MessageBoxImage.Information);
            OpenSignIn();
            QuitMainWindow();
        }
        private void OpenSignIn()
        {
            var rw = new RegisterWindow();
            rw.Show();
        }
        private void QuitMainWindow() => System.Windows.Application.Current.Windows.OfType<MainWindow>().FirstOrDefault()?.Close();

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged(string? propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}