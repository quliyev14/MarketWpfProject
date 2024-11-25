using GalaSoft.MvvmLight.Command;
using MarketWpfProject.Data;
using MarketWpfProject.Moduls;
using MarketWpfProject.Views;
using Microsoft.Win32;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;

namespace MarketWpfProject.ViewModels.AdminPanelUserControlViewModel
{
    public class AddProductViewModel : INotifyPropertyChanged
    {
        private readonly static object _prso = new();

        private static string path = "products.json";
        private static string log = "products.log";

        public ObservableCollection<Product> Products { get; set; }

        private Product _product;
        public Product Product { get => _product; set { _product = value; OnPropertyChanged(nameof(Product)); } }
        public RelayCommand AddProductCommand { get; set; }
        public RelayCommand DeleteCommand { get; set; }
        public RelayCommand EditCommand { get; set; }
        public RelayCommand SelectImageCommand { get; set; }

        public AddProductViewModel()
        {
            Products = new ObservableCollection<Product>();
            AddProductCommand = new RelayCommand(AddProduct);
            DeleteCommand = new RelayCommand(DeleteProduct);
            EditCommand = new RelayCommand(EditProduct);
            SelectImageCommand = new RelayCommand(SelectImage);
            Product = new();
            LoadProductsFromJson();
        }

        private Product _selectedProduct;
        public Product SelectedProduct
        {
            get => _selectedProduct;
            set
            {
                _selectedProduct = value;
                OnPropertyChanged(nameof(SelectedProduct));
            }
        }
        private void AddProduct()
        {
            if (!string.IsNullOrWhiteSpace(Product.Name) && Product.Count > 0 && Product.Price > 0)
            {
                var product = new Product
                {
                    Name = Product.Name,
                    Price = Product.Price,
                    ImagePath = Product.ImagePath,
                    Count = Product.Count
                };
                Products.Add(product);
                DB.JsonWrite<Product>(path, Products);
            }
            Product = new();
        }
        private void DeleteProduct()
        {
            if (SelectedProduct != null)
            {
                lock (_prso)
                {
                    Products.Remove(SelectedProduct);
                    DB.JsonWrite<Product>(path, Products);
                }
                SelectedProduct = null;
                return;
            }
            MessageBox.Show("Please Listbox item select");
        }

        private async void LoadProductsFromJson()
        {
            var products = await DB.JsonRead<Product>(path);

            if (products is not null)
                foreach (var product in products)
                    Products.Add(product);
        }
        private void SelectImage()
        {
            var openFileDialog = new OpenFileDialog
            {
                Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp"
            };
            if (openFileDialog.ShowDialog() == true)
                Product.ImagePath = openFileDialog.FileName;
        }

        private void EditProduct()
        {
            if (SelectedProduct != null)
            {
                lock (_prso)
                {
                    var editWindow = new EditWindow(SelectedProduct);
                    editWindow.Show();
                }
                SelectedProduct = null;
                return;
            }
            MessageBox.Show("Please Listbox item select");
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}