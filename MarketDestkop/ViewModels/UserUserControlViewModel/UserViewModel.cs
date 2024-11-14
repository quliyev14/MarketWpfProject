using GalaSoft.MvvmLight.Command;
using MarketWpfProject.Data;
using MarketWpfProject.Helper.PathHelper;
using MarketWpfProject.Moduls;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;

namespace MarketWpfProject.ViewModels.UserUserControlViewModel
{
    public class UserViewModel : INotifyPropertyChanged
    {
        private string path = "products.json";
        public ObservableCollection<Product> Products { get; set; } = new();
        public RelayCommand SearchCommand { get; set; }
        public RelayCommand<Product> IncreaseQuantityCommand { get; set; }
        public RelayCommand<Product> DecreaseQuantityCommand { get; set; }
        public RelayCommand<Product> AddToPacketCommand { get; set; }

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


        public UserViewModel()
        {
            LoadProduct();
            SearchCommand = new RelayCommand(SearchProduct);
            AddToPacketCommand = new RelayCommand<Product>(AddProductToUserPacket);
        }

        private void AddProductToUserPacket(Product product)
        {
            string FullName = "mypacket";

            string userFileName = $"{FullName}.json";
            var productList = new List<Product>();

            if (PathCheck.OpenOrClosed(userFileName))
            {
                var existingProducts = DB.JsonRead<Product>(userFileName)?.ToList();
                if (existingProducts is not null)
                    productList.AddRange(existingProducts);
            }
            productList.Add(product);
            DB.JsonWrite(userFileName, "user.log", productList);
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
                var matchedProducts = DB.JsonRead<Product>(path)?.Where(p => p.Name!.ToLower().Contains(SearchTb.ToLower())).ToList() ?? throw new Exception();

                if (matchedProducts.Any()) matchedProducts.ForEach(p => Products.Add(p));
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