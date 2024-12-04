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
        public RelayCommand SearchCommand { get; set; }

        private string? _searchTb;
        public string? SearchTb { get => _searchTb; set { _searchTb = value; OnPropertyChanged(nameof(SearchTb)); } }

        private decimal? _totalPrice;
        public decimal? TotalPrice { get => _totalPrice; set { _totalPrice = value; OnPropertyChanged(nameof(TotalPrice)); } }

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
                Products.Clear();

                if (PathCheck.OpenOrClosed(_userpath))
                    DB.JsonWrite<Product>(_userpath, Products);
                TotalPrice = 0;
                MessageBox.Show("All product succesfult be deleted.", "Succesfuly", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
                MessageBox.Show("Products could not be deleted.", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
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
        private void Products_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e) => MyBasketProductTotalPrice();
        public decimal MyBasketProductTotalPrice()
        {
            TotalPrice = Products.Sum(p => (p.Quantity ?? 1) * (p.Price ?? 0));
            App.TotalAmount = (decimal)TotalPrice;
            return (decimal)TotalPrice;
        }
        private void IncreaseQuantity(Product product)
        {
            if (product != null && product.Quantity < 30)
            {
                product.Quantity++;
                MyBasketProductTotalPrice();
                DB.JsonWrite<Product>(_userpath, Products);
            }
        }
        private void DecreaseQuantity(Product product)
        {
            if (product != null && product.Quantity > 1)
            {
                product.Quantity--;
                MyBasketProductTotalPrice();
                DB.JsonWrite<Product>(_userpath, Products);
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
