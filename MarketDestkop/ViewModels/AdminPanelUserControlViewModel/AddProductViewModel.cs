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
        private static string path = "products.json";
        private static string log = "products.log";

        private readonly static object _prso = new();
        public ObservableCollection<Product> Products { get; set; }
        public RelayCommand AddProductCommand { get; set; }
        public RelayCommand DeleteCommand { get; set; }
        public RelayCommand EditCommand { get; set; }
        public RelayCommand SelectImageCommand {  get; set; }

        public AddProductViewModel()
        {
            Products = new ObservableCollection<Product>();
            AddProductCommand = new RelayCommand(AddProduct);
            DeleteCommand = new RelayCommand(DeleteProduct);
            EditCommand = new RelayCommand(EditProduct);
            SelectImageCommand = new RelayCommand(SelectImage);
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

        private string? _imagePath;

        public string? ImagePath
        {
            get => _imagePath;
            set
            {
                _imagePath = value;
                OnPropertyChanged(nameof(ImagePath));
            }
        }

        private void AddProduct()
        {
            if (!string.IsNullOrWhiteSpace(Name) && Count > 0 && Price > 0)
            {
                var newProduct = new Product
                (
                    Name = Name,
                    Price = Price,
                    Count = Count,
                    ImagePath = ImagePath
                );

                Products.Add(newProduct);
                DB.JsonWrite<Product>(path, log, Products);
            }
            Clear();
        }

        private void Clear()
        {
            Name = string.Empty;
            Price = 0;
            Count = 0;
        }

        private void DeleteProduct()
        {
            if (SelectedProduct != null)
            {
                lock (_prso)
                {
                    Products.Remove(SelectedProduct);
                    DB.JsonWrite(path, log, Products);
                }
                SelectedProduct = null;
                return;
            }
            MessageBox.Show("Please Listbox item select");
        }

        private void LoadProductsFromJson()
        {
            var products = DB.JsonRead<Product>(path);

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
                ImagePath = openFileDialog.FileName;
        }

        private void EditProduct()
        {
            var editWindow = new EditWindow(SelectedProduct);
            editWindow.Show();
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}