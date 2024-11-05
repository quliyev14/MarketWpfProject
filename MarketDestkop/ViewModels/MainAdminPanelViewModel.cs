
using System.Windows;
using GalaSoft.MvvmLight.Command;
using MarketWpfProject.UserControls.AdminUS;
using System.Windows.Controls;

namespace MarketWpfProject.ViewModels
{
    public class MainAdminPanelViewModel
    {
        public RelayCommand HomeCommand { get; set; }
        public RelayCommand ExitWindowCommmand { get; set; }

        private readonly Frame _frame;

        public MainAdminPanelViewModel(Frame frame)
        {
            _frame = frame;
            HomeCommand = new RelayCommand(Home);
            ExitWindowCommmand = new RelayCommand(ExitWindow);
        }

        private void Home() => _frame.Navigate(new HomeUserControl());

        private void DeleteProduct() => _frame.Navigate(new DeleteProductUserControl());

        private void EditProduct() => _frame.Navigate(new EditProductUserControl());

        private void ExitWindow() => Application.Current.Shutdown();
    }
}