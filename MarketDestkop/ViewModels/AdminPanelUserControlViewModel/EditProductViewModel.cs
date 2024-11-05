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

        private readonly Product _product;

        public EditProductViewModel(Product selectedProduct)
        {
            _product = selectedProduct;
            Name = selectedProduct.Name;
            Price = selectedProduct.Price;
            Count = selectedProduct.Count;
            SaveCommand = new RelayCommand(SaveProduct);
            CancelCommand = new RelayCommand(CancelEditWindow);
        }

        private string? _name;
        public string? Name
        {
            get => _name;
            set
            {
                _name = value;
                OnPropertyChanged(nameof(Name));
            }
        }
        private decimal? _price;
        public decimal? Price
        {
            get => _price;
            set
            {
                _price = value;
                OnPropertyChanged(nameof(Price));
            }
        }
        private int _count;
        public int Count
        {
            get => _count;
            set
            {
                _count = value;
                OnPropertyChanged(nameof(Count));
            }
        }


        private void SaveProduct()
        {
            var products = DB.JsonRead<Product>(path) ?? throw new ArgumentNullException("");

            var existingProduct = products.FirstOrDefault(p => p.Name == _product.Name && p.Price == _product.Price && p.Count == _product.Count);

            if (existingProduct != null)
            {
                existingProduct.Name = Name;
                existingProduct.Price = Price;
                existingProduct.Count = Count;

                DB.JsonWrite(path, log, products);
            }
            CancelEditWindow();
        }


        private void CancelEditWindow() => System.Windows.Application.Current.Windows.OfType<EditWindow>().FirstOrDefault()?.Close();

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
