using System.Windows;
using System.Windows.Controls;
using GalaSoft.MvvmLight.Command;
using MarketDestkop.Views;
using MarketWpfProject.UserControls;
using MarketWpfProject.UserControls.SuperAdminUS;
using MarketWpfProject.Views;
using MarketWpfProject.Views.SuperAdminView;

namespace MarketWpfProject.ViewModels
{
    public class SuperAdminPanelWewModels
    {
        public RelayCommand SignUpCommand { get; }
        public RelayCommand GoBackCommand { get; }
        public RelayCommand AllShowUserCommand { get; }

        private readonly Frame? _frame;
        public SuperAdminPanelWewModels(Frame frame)
        {
            _frame = frame;
            SignUpCommand = new RelayCommand(OpenSuperAdminPanelWindow);
            AllShowUserCommand = new RelayCommand(AllShowUser);
            GoBackCommand = new RelayCommand(ThisWindowExit);
        }
        private void OpenSuperAdminPanelWindow()
        {
            var sapw = new SuperAdminPanelWindow();
            sapw.Show();
        }
        private void AllShowUser() => _frame?.Navigate(new AdminViewUserControl());
        private void ThisWindowExit() => Application.Current.Windows.OfType<SuperAdminPanelMainWindow>().FirstOrDefault()?.Close();
    }
}
