using GalaSoft.MvvmLight.Command;
using MarketWpfProject.Data;
using MarketWpfProject.Hashed;
using MarketWpfProject.Helper.PathHelper;
using MarketWpfProject.Moduls;
using MarketWpfProject.Views;
using MarketWpfProject.Views.UserView;
using System.ComponentModel;

namespace MarketWpfProject.ViewModels.AdminPanelUserControlViewModel
{
    public class AdminPanelViewModel : INotifyPropertyChanged
    {
        private string path = "admin.json";
        public RelayCommand LoginCommand { get; set; }

        private Admin? _admin;
        public Admin? Admin { get => _admin; set { _admin = value; OnPropertyChanged(nameof(Admin)); } }

        public AdminPanelViewModel()
        {
            LoginCommand = new RelayCommand(SignIn);
            Admin = new();
        }

        private void SignIn()
        {
            var admins = DB.JsonRead<Admin>(path) ?? new List<Admin>();
            if (!PathCheck.OpenOrClosed(path)) throw new FieldAccessException(nameof(path));

            foreach (var admin in admins)
            {
                if (admin.GmailService?.Email == Admin?.GmailService?.Email &&
                    admin.GmailService?.Password == DatasIsHashed.WithSHA256PasswordHash(Admin?.GmailService?.Password))
                    OpenMainWindowShow();
            }
            QuitAdminPanelWindow();
            RefreshMethod();
        }

        private void QuitAdminPanelWindow() => System.Windows.Application.Current.Windows.OfType<AdminPanelWindow>().FirstOrDefault()?.Close();
        private void OpenMainWindowShow()
        {
            var signUpWindow = new MainAdminPanelWindow();
            signUpWindow.Show();
        }
        private void RefreshMethod() => Admin = new();

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}