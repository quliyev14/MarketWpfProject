using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Threading;
using GalaSoft.MvvmLight.Command;
using MarketDestkop;
using MarketWpfProject.Data;
using MarketWpfProject.Helper.PathHelper;
using MarketWpfProject.Moduls;
using MarketWpfProject.Views.UserView;

namespace MarketWpfProject.ViewModels.UserUserControlViewModel
{
    public class MyBasketViewModel : INotifyPropertyChanged
    {
        private readonly string _userpath = $"{App.CurrentUser?.GmailService.Email}.json";
        public ObservableCollection<Product> Products { get; set; }
        public RelayCommand<Product> DeleteFromBasketCommand { get; }
        public RelayCommand<Product> IncreaseQuantityCommand { get; }
        public RelayCommand<Product> DecreaseQuantityCommand { get; }
        public RelayCommand MoveTrashCommand { get; }
        public RelayCommand PaymentCommand { get; }
        public RelayCommand SearchCommand { get; }

        private string? _searchTb;
        public string? SearchTb { get => _searchTb; set { _searchTb = value; OnPropertyChanged(nameof(SearchTb)); } }

        private decimal? _totalPrice;
        public decimal? TotalPrice { get => _totalPrice; set { _totalPrice = value; OnPropertyChanged(nameof(TotalPrice)); } }

        private bool _isPaymentEnabled;
        public bool IsPaymentEnabled
        {
            get => _isPaymentEnabled;
            set
            {
                _isPaymentEnabled = value;
                OnPropertyChanged(nameof(IsPaymentEnabled));
            }
        }

        private DispatcherTimer _timer;
        private string _currentTime;
        public string CurrentTime
        {
            get => _currentTime;
            set
            {
                _currentTime = value;
                OnPropertyChanged(nameof(CurrentTime));
            }
        }
        public MyBasketViewModel()
        {
            Products = new ObservableCollection<Product>();
            Products.CollectionChanged += Products_CollectionChanged;
            IsPaymentEnabled = Products.Any();
            SearchCommand = new RelayCommand(MyBasketSearchProduct);
            DeleteFromBasketCommand = new RelayCommand<Product>(MyBasketDelete);
            IncreaseQuantityCommand = new RelayCommand<Product>(IncreaseQuantity);
            DecreaseQuantityCommand = new RelayCommand<Product>(DecreaseQuantity);
            MoveTrashCommand = new RelayCommand(MoveTrash);
            PaymentCommand = new RelayCommand(OpenPaymentrWindow);
            MyBasketProductTotalPrice();
            LoadProduct();
            ActiveClockShow();
        }

        private void MoveTrash()
        {
            var result = MessageBox.Show(
                "Are you sure you want to delete all purchased items?",
                "Products delete",
                MessageBoxButton.YesNo,
                MessageBoxImage.Warning
            );

            if (result == MessageBoxResult.Yes)
            {
                var userProductList = new List<Product>();

                if (PathCheck.OpenOrClosed(_userpath))
                {
                    var existingProducts = DB.JsonRead<Product>(_userpath);
                    if (existingProducts is not null)
                        userProductList.AddRange(existingProducts);

                    if (PathCheck.OpenOrClosed("products.json"))
                    {
                        var productList = DB.JsonRead<Product>("products.json");
                        foreach (var product in userProductList)
                        {
                            var productInList = productList.FirstOrDefault(p => p.Name == product.Name);
                            if (productInList != null)
                            {
                                productInList.Count += product.Quantity;
                            }
                        }
                        DB.JsonWrite("products.json", productList);
                    }

                    DB.JsonWrite<Product>(_userpath, new List<Product>());
                }

                Products.Clear();
                TotalPrice = 0;

                MessageBox.Show("All products successfully deleted.", "Successful", MessageBoxButton.OK, MessageBoxImage.Information);
                RefreshProducts();
            }
        }
        private void OpenPaymentrWindow()
        {
            var pw = new PaymentWindow();
            pw.Show();
        }
        private void ActiveClockShow()
        {
            _timer = new DispatcherTimer
            {
                Interval = TimeSpan.FromSeconds(1)
            };
            _timer.Tick += (s, e) => CurrentTime = DateTime.Now.ToString("HH:mm:ss");
            _timer.Start();
        }
        private void Products_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            MyBasketProductTotalPrice();
            IsPaymentEnabled = Products.Any();
        }
        public decimal MyBasketProductTotalPrice()
        {
            TotalPrice = Products.Sum(p => (p.Quantity ?? 1) * (p.Price ?? 0));
            App.TotalAmount = (decimal)TotalPrice;
            return (decimal)TotalPrice;
        }
        private void IncreaseQuantity(Product product)
        {
            if (product == null) return;

            product.Quantity++;
            MyBasketProductTotalPrice();
            UpdateProductInJson(product);
        }

        private void DecreaseQuantity(Product product)
        {
            if (product == null || product.Quantity <= 1) return;

            product.Quantity--;
            MyBasketProductTotalPrice();
            UpdateProductInJson(product);
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
            if (product == null) return;

            var productList = DB.JsonRead<Product>("products.json");
            var userProductList = DB.JsonRead<Product>(_userpath);

            if (userProductList != null && productList != null)
            {
                var productInUserList = userProductList.FirstOrDefault(p => p.Name == product.Name);
                var productInList = productList.FirstOrDefault(p => p.Name == product.Name);

                if (productInUserList != null)
                {
                    if (productInList != null)
                    {
                        productInList.Count += productInUserList.Quantity;
                    }

                    userProductList.Remove(productInUserList);
                }

                DB.JsonWrite(_userpath, userProductList);
                DB.JsonWrite("products.json", productList);
            }

            Products.Remove(product);
            RefreshProducts();
        }
        private void UpdateProductInJson(Product product)
        {
            var userProductList = DB.JsonRead<Product>(_userpath);
            if (userProductList != null)
            {
                var productToUpdate = userProductList.FirstOrDefault(p => p.Name == product.Name);
                if (productToUpdate != null)
                {
                    productToUpdate.Quantity = product.Quantity;
                    DB.JsonWrite(_userpath, userProductList);
                }
            }
        }
        private void RefreshProducts()
        {
            Products.Clear();
            LoadProduct();
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
