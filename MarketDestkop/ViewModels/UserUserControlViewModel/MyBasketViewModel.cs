using System.Collections.ObjectModel;
using System.ComponentModel;
using GalaSoft.MvvmLight.Command;
using MarketDestkop;
using MarketWpfProject.Data;
using MarketWpfProject.Helper.PathHelper;
using MarketWpfProject.Moduls;

namespace MarketWpfProject.ViewModels.UserUserControlViewModel
{
    public class MyBasketViewModel : INotifyPropertyChanged
    {
        private readonly string _userpath = $"{App.CurrentUser?.GmailService.Email}.json";
        public ObservableCollection<Product> Products { get; set; }
        public RelayCommand<Product> DeleteFromBasketCommand { get; set; }
        public RelayCommand<Product> IncreaseQuantityCommand { get; set; }
        public RelayCommand<Product> DecreaseQuantityCommand { get; set; }
        public RelayCommand SearchCommand { get; set; }

        private string? _searchTb;
        public string? SearchTb { get => _searchTb; set { _searchTb = value; OnPropertyChanged(nameof(SearchTb)); } }

        private decimal? _totalPrice;
        public decimal? TotalPrice { get => _totalPrice; set { _totalPrice = value; OnPropertyChanged(nameof(TotalPrice)); } }
        public MyBasketViewModel()
        {
            Products = new ObservableCollection<Product>();
            Products.CollectionChanged += Products_CollectionChanged; // Koleksiyon değişikliklerini dinliyoruz.
            SearchCommand = new RelayCommand(MyBasketSearchProduct);
            DeleteFromBasketCommand = new RelayCommand<Product>(MyBasketDelete);
            IncreaseQuantityCommand = new RelayCommand<Product>(IncreaseQuantity);
            DecreaseQuantityCommand = new RelayCommand<Product>(DecreaseQuantity);
            LoadProduct();
            MyBasketProductTotalPrice();
        }

        private void Products_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e) => MyBasketProductTotalPrice();
        private void MyBasketProductTotalPrice() => TotalPrice = Products.Sum(p => (p.Quantity ?? 1) * (p.Price ?? 0));
        private void IncreaseQuantity(Product product)
        {
            if (product != null && product.Quantity < 30)
            {
                product.Quantity++;
            }
        }
        private void DecreaseQuantity(Product product)
        {
            if (product != null && product.Quantity > 1)
            {
                product.Quantity--;
            }
        }
        private void MyBasketSearchProduct()
        {
            if (string.IsNullOrEmpty(SearchTb))
            {
                Products.Clear();
                LoadProduct();
                return;
            }

            Products.Clear();

            if (PathCheck.OpenOrClosed(_userpath))
            {
                var matchedProducts = DB.JsonRead<Product>(_userpath).Where(p => p.Name!.ToLower().Contains(SearchTb.ToLower())).ToList() ?? throw new Exception();

                if (matchedProducts.Any()) matchedProducts.ForEach(p => Products.Add(p));
            }
            SearchTb = string.Empty;
        }
        private void MyBasketDelete(Product product)
        {
            if (Products is not null)
            {
                var products = DB.JsonRead<Product>(_userpath) ?? new List<Product>();

                if (products is not null)
                {
                    Products.Remove(product);
                    DB.JsonWrite<Product>(_userpath, Products);
                }
            }
        }
        private void LoadProduct()
        {
            if (PathCheck.OpenOrClosed(_userpath))
            {
                var products = DB.JsonRead<Product>(_userpath) ?? throw new ArgumentNullException("Argument is null!");
                foreach (var product in products)
                    Products.Add(product);
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
