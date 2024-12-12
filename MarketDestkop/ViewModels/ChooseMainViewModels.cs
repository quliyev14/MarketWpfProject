using GalaSoft.MvvmLight.Command;
using MarketDestkop.Views;
using MarketWpfProject.Views;
using MarketWpfProject.Views.SuperAdminView;

namespace MarketWpfProject.ViewModels
{
    public class ChooseMainViewModels
    {
        public RelayCommand SuperAdminCommand { get; }
        public RelayCommand AdminCommand { get; }
        public RelayCommand UserCommand { get; }
        public ChooseMainViewModels()
        {
            SuperAdminCommand = new RelayCommand(OpenSuperAdminWindow);
            AdminCommand = new RelayCommand(OpenAdminWindow);
            UserCommand = new RelayCommand(OpenUserWindow);
        }

        private void OpenSuperAdminWindow() 
        {
            var sapw = new SuperAdminPanelMainWindow();
            sapw.Show();
        }
      
        private void OpenAdminWindow()
        {
            var mapw = new AdminPanelWindow();
            mapw.Show();
        }
        private void OpenUserWindow()
        {
            var rw = new RegisterWindow();
            rw.Show();
        }
    }
}
