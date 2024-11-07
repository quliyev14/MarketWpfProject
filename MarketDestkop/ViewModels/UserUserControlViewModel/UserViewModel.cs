using GalaSoft.MvvmLight.Command;
using MarketWpfProject.Data;
using MarketWpfProject.Helper.PathHelper;
using MarketWpfProject.Moduls;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text.Json;
using System.Windows;
using System.Xml;

namespace MarketWpfProject.ViewModels.UserUserControlViewModel
{
    public class UserViewModel : INotifyPropertyChanged
    {
        private string path = "products.json";

        public ObservableCollection<Product> Products { get; set; } = new ObservableCollection<Product>();

        private string? _searchTb;

        public string? SearchTb
        {
            get => _searchTb;
            set
            {
                _searchTb = value;
                OnPropertyChanged(nameof(SearchTb));
            }
        }

        public RelayCommand SearchCommand { get; set; }

        public UserViewModel()
        {
            LoadProduct();
            SearchCommand = new RelayCommand(SearchProduct);
            AddToPacketCommand = new RelayCommand<Product>(AddProductToUserPacket);
        }

        public string? UserName { get; set; } 
        public RelayCommand<Product> AddToPacketCommand { get; set; }

        private void AddProductToUserPacket(Product product)
        {
            string userFileName = $"{UserName}_packet.json";

            if (PathCheck.OpenOrClosed("products.json"))
            {
                DB.JsonWrite(userFileName, "user.log", product.ToString().ToList());
                MessageBox.Show("Ürün başarıyla eklendi.");

            }
        }

        private void LoadProduct()
        {
            if (PathCheck.OpenOrClosed(path))
            {
                var products = DB.JsonRead<Product>(path) ?? throw new ArgumentNullException("Argument is null!");
                foreach (var product in products)
                    Products.Add(product);
            }
        }

        private void SearchProduct()
        {
            if (string.IsNullOrEmpty(SearchTb))
            {
                Products.Clear();
                LoadProduct();
                return;
            }

            Products.Clear();

            if (PathCheck.OpenOrClosed(path))
            {
                var products = DB.JsonRead<Product>(path) ?? throw new ArgumentNullException("Argument is null!");

                var matchedProducts = products.Where(p => p.Name!.ToLower().Contains(SearchTb.ToLower())).ToList();

                if (matchedProducts.Any())
                    foreach (var product in matchedProducts)
                        Products.Add(product);
                else
                    MessageBox.Show("No products found with this search term.");
            }
            SearchTb = string.Empty;
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}