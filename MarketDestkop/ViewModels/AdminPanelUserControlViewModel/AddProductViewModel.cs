using GalaSoft.MvvmLight.Command;
using MarketWpfProject.Moduls;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;

namespace MarketWpfProject.ViewModels.AdminPanelUserControlViewModel
{
    public class AddProductViewModel : INotifyPropertyChanged
    {
        private readonly static object _prso = new();
        public ObservableCollection<Product> Products { get; set; }
        public RelayCommand AddProductCommand { get; set; }
        public RelayCommand DeleteCommand { get; set; }
        public RelayCommand EditCommand { get; set; }

        public AddProductViewModel()
        {
            Products = new ObservableCollection<Product>();
            AddProductCommand = new RelayCommand(AddProduct);
            DeleteCommand = new RelayCommand(DeleteProduct);
            EditCommand = new RelayCommand(EditProduct);
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

        private void AddProduct()
        {
            var newProduct = new Product
            (
                Name = Name,
                Price = Price,
                Count = Count
            );

            Products.Add(newProduct);
        }

        private void DeleteProduct()
        {
            if (SelectedProduct != null)
            {
                lock (_prso)
                    Products.Remove(SelectedProduct);
                SelectedProduct = null;
                return;
            }
            MessageBox.Show("Please Listbox item select");
        }

        private void EditProduct()
        {

        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
