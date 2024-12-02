using System.Windows;
using GalaSoft.MvvmLight.Command;
using MarketWpfProject.UserControls.AdminUS;
using System.Windows.Controls;

namespace MarketWpfProject.ViewModels.AdminPanelUserControlViewModel
{
    public class MainAdminPanelViewModel
    {
        public RelayCommand HomeCommand { get; set; }
        public RelayCommand AllShowCommand { get; set; }
        public RelayCommand ExitWindowCommmand { get; set; }
        public RelayCommand UserViewCommand { get; set; }

        private readonly Frame _frame;
        public MainAdminPanelViewModel(Frame frame)
        {
            _frame = frame;
            HomeCommand = new RelayCommand(Home);
            AllShowCommand = new RelayCommand(AllShow);
            UserViewCommand = new RelayCommand(UserAllShow);
            ExitWindowCommmand = new RelayCommand(ExitWindow);
        }
        private void Home() => _frame?.Navigate(new HomeUserControl());
        private void AllShow() => _frame?.Navigate(new AllShowUserControl());
        private void UserAllShow() => _frame.Navigate(new UserViewUserControl());
        private void ExitWindow() => Application.Current.Shutdown();
    }
}