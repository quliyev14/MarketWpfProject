using GalaSoft.MvvmLight.Command;
using MarketDestkop;
using MarketDestkop.Views;
using MarketWpfProject.UserControls.UserUS;
using MarketWpfProject.Views;
using System.Windows;
using System.Windows.Controls;

namespace MarketWpfProject.ViewModels.UserUserControlViewModel
{
    public class MainWindowViewModel
    {
        public RelayCommand ProductsCommand { get; }
        public RelayCommand MyBasketCommand { get; }
        public RelayCommand SignOutCommand { get; }
        public RelayCommand GoBackCommand { get; }
        public RelayCommand ProfileSettingsCommand { get; }

        private readonly Frame? _frame;

        public string? Name { get; }
        public string? Surname { get; }
        public string? UserFullName { get; }

        public MainWindowViewModel(Frame frame)
        {
            _frame = frame;
            ProductsCommand = new RelayCommand(OpenProductsUserControl);
            MyBasketCommand = new RelayCommand(OpenMyBasketUserControl);
            SignOutCommand = new RelayCommand(UserSignOut);
            GoBackCommand = new RelayCommand(GoBackRegisterWindow);
            ProfileSettingsCommand = new RelayCommand(ProfilSetting);
            Name = App.CurrentUser?.Name;
            Surname = App.CurrentUser?.Surname;
            UserFullName = $"{App.CurrentUser?.Name?[0]} {App.CurrentUser?.Surname?[0]}";
        }

        private void OpenProductsUserControl() => _frame?.Navigate(new ProductUS());
        private void OpenMyBasketUserControl() => _frame?.Navigate(new MyBasketUS());
        private void ProfilSetting() => _frame?.Navigate(new ProfileSettingUS());
        private void GoBackRegisterWindow()
        {
            OpenRegisterWindow();
            Application.Current.Windows.OfType<MainWindow>().FirstOrDefault()?.Close();
        }
        private void OpenRegisterWindow()
        {
            var rw = new RegisterWindow();
            rw.Show();
        }
        private void UserSignOut()
        {
            MessageBoxResult mbr = MessageBox.Show("Sign Out?", "Sign Out", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (mbr == MessageBoxResult.Yes)
                Application.Current.Shutdown();
        }
    }
}