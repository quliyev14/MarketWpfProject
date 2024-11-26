using GalaSoft.MvvmLight.Command;
using MarketDestkop;
using MarketWpfProject.Data;
using MarketWpfProject.Helper.PathHelper;
using MarketWpfProject.Moduls;
using System.Collections.ObjectModel;
using System.ComponentModel;

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
        public Product Product { get; set; }

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

        //private int? _quantity = 0;
        //public int? Quantity { get => _quantity; set { _quantity = value; OnPropertyChanged(nameof(_quantity)); } }
        public UserViewModel()
        {
            LoadProduct();
            SearchCommand = new RelayCommand(SearchProduct);
            AddToPacketCommand = new RelayCommand<Product>(AddProductToUserPacket);
            IncreaseQuantityCommand = new RelayCommand<Product>(IncreaseQuantity);
            DecreaseQuantityCommand = new RelayCommand<Product>(DecreaseQuantity);
        }

        private void IncreaseQuantity(Product product)
        {
            if (product != null && product.Quantity < product.Count)
            {
                product.Quantity++;
                OnPropertyChanged(nameof(Products));
            }
        }

        private void DecreaseQuantity(Product product)
        {
            if (product != null && product.Quantity > 0)
            {
                product.Quantity--;
                OnPropertyChanged(nameof(Products));
            }
        }

        private void AddProductToUserPacket(Product product)
        {
            string userFileName = $"{App.CurrentUser?.Name}{App.CurrentUser?.Surname}.json";

            var productList = new List<Product>();

            if (PathCheck.OpenOrClosed(userFileName))
            {
                var existingProducts = DB.JsonRead<Product>(userFileName);
                if (existingProducts is not null)
                    productList.AddRange(existingProducts);
            }
            productList.Add(product);
            DB.JsonWrite(userFileName, productList);
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
            //if (string.IsNullOrEmpty(SearchTb))
            //{
            //    Products.Clear();
            //    LoadProduct();
            //    return;
            //}

            //Products.Clear();

            //if (PathCheck.OpenOrClosed(path))
            //{
            //    var matchedProducts = await DB.JsonRead<Product>(path).ToString()?.Where(p => p.Name!.ToLower().Contains(SearchTb.ToLower())).ToList() ?? throw new Exception();

            //    if (matchedProducts.Any()) matchedProducts.ForEach(p => Products.Add(p));
            //}
            //SearchTb = string.Empty;
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}