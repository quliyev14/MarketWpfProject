using System.ComponentModel;
using MarketDestkop;
using MarketWpfProject.Hashed;
using MarketWpfProject.Models;

namespace MarketWpfProject.ViewModels.UserUserControlViewModel
{
    public class ProfileSettingUSViewModel : INotifyPropertyChanged
    {
        private User? _user;
        public User? User { get => _user; set { _user = value; OnPropertyChanged(nameof(User)); } }
        public ProfileSettingUSViewModel()
        {
            User = App.CurrentUser;
        }


        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged(string? propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
