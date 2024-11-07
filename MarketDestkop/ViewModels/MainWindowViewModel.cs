using GalaSoft.MvvmLight.Command;
using MarketWpfProject.Data;
using MarketWpfProject.Hashed;
using MarketWpfProject.Models;
using MarketWpfProject.UserControls.UserUS;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Xml.Linq;

namespace MarketWpfProject.ViewModels
{
    public class MainWindowViewModel
    {
        public RelayCommand ProductsCommand { get; set; }
        public RelayCommand MyBasketCommand { get; set; }
        public RelayCommand SignOutCommand { get; set; }

        private readonly Frame? _frame;
        public MainWindowViewModel(Frame frame)
        {
            _frame = frame;
            ProductsCommand = new RelayCommand(OpenProductsUserControl);
            SignOutCommand = new RelayCommand(UserSignOut);
        }

        private void OpenProductsUserControl() => _frame?.Navigate(new ProductUS());
        private void UserSignOut()
        {
            MessageBoxResult mbr = MessageBox.Show("Sign Out?", "Sign Out", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (mbr == MessageBoxResult.Yes)
                System.Windows.Application.Current.Shutdown();
        }
    }
}
