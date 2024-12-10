using GalaSoft.MvvmLight.Command;
using MarketWpfProject.Views;

namespace MarketWpfProject.ViewModels
{
    public class SuperAdminPanelWewModels
    {
        public RelayCommand SignUpCommand { get; }
        public SuperAdminPanelWewModels()
        {
            SignUpCommand = new RelayCommand(OpenSuperAdminPanelWindow);
        }
        private void OpenSuperAdminPanelWindow()
        {
            var sapw = new SuperAdminPanelWindow();
            sapw.Show();
        }
    }
}
