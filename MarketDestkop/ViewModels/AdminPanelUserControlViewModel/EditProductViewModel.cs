using GalaSoft.MvvmLight.Command;
using MarketWpfProject.Moduls;
using System.ComponentModel;
using MarketWpfProject.Views;
using MarketWpfProject.Data;

namespace MarketWpfProject.ViewModels.AdminPanelUserControlViewModel
{
    public class EditProductViewModel : INotifyPropertyChanged
    {
        private static string path = "products.json";
        private static string log = "products.log";
        public RelayCommand SaveCommand { get; set; }
        public RelayCommand CancelCommand { get; set; }

        private Product _product;
        public Product Product { get => _product; set { _product = value; OnPropertyChanged(nameof(Product)); } }

        private Product _originalProduct;
        public EditProductViewModel(Product selectedProduct)
        {
            _originalProduct = selectedProduct;
            Product = selectedProduct.Clone();
            SaveCommand = new RelayCommand(SaveProduct);
            CancelCommand = new RelayCommand(CancelEditWindow);
        }

        private async void SaveProduct()
        {
            var products = await DB.JsonRead<Product>(path) ?? throw new ArgumentNullException("");
            var existingProduct = products.FirstOrDefault(p =>
                                                          p.Name == _originalProduct.Name &&
                                                          p.Price == _originalProduct.Price &&
                                                          p.Count == _originalProduct.Count);
            if (existingProduct != null)
            {
                existingProduct.Name = Product.Name;
                existingProduct.Price = Product.Price;
                existingProduct.Count = Product.Count;

                DB.JsonWrite(path, products);
            }
            CancelEditWindow();
        }

        private void CancelEditWindow() => System.Windows.Application.Current.Windows.OfType<EditWindow>().FirstOrDefault()?.Close();

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}