
using System.Windows;
using GalaSoft.MvvmLight.Command;
using MarketWpfProject.UserControls.AdminUS;
using System.Windows.Controls;

namespace MarketWpfProject.ViewModels
{
    public class MainAdminPanelViewModel
    {
        public RelayCommand AddProductCommand { get; set; }
        public RelayCommand DeleteProductCommand { get; set; }
        public RelayCommand EditProductCommand { get; set; }
        public RelayCommand ExitWindowCommmand { get; set; }

        private readonly Frame _frame;

        public MainAdminPanelViewModel(Frame frame)
        {
            _frame = frame;
            AddProductCommand = new RelayCommand(AddProduct);
            DeleteProductCommand = new RelayCommand(DeleteProduct);
            EditProductCommand = new RelayCommand(EditProduct);
            ExitWindowCommmand = new RelayCommand(ExitWindow);
        }

        private void AddProduct() => _frame.Navigate(new AddProductUserControl());

        private void DeleteProduct() => _frame.Navigate(new DeleteProductUserControl());

        private void EditProduct() => _frame.Navigate(new EditProductUserControl());

        private void ExitWindow() => Application.Current.Shutdown();
    }
}