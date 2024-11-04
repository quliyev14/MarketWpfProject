using GalaSoft.MvvmLight.Command;
using MarketWpfProject.Moduls;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace MarketWpfProject.ViewModels.AdminPanelUserControlViewModel
{
    public class AddProductViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<Product> Product { get; set; }

        public RelayCommand AddProductCommand { get; set; }

        public AddProductViewModel()
        {
            Product = new ObservableCollection<Product>();
            AddProductCommand = new RelayCommand(AddProduct);

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

            Product.Add(newProduct);
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
