using GalaSoft.MvvmLight.Command;
using MarketDestkop;
using MarketWpfProject.Data;
using MarketWpfProject.Helper.PathHelper;
using MarketWpfProject.Moduls;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Threading;

namespace MarketWpfProject.ViewModels.UserUserControlViewModel
{
    public class UserViewModel : INotifyPropertyChanged
    {
        private string _productPath = App.ProductPath;
        private string _userFileName = $"{App.CurrentUser?.GmailService.Email}.json";
        public ObservableCollection<Product> Products { get; set; } = new();
        public RelayCommand SearchCommand { get; }
        public RelayCommand<Product> IncreaseQuantityCommand { get; }
        public RelayCommand<Product> DecreaseQuantityCommand { get; }
        public RelayCommand<Product> AddToPacketCommand { get; }

        private string? _searchTb;
        public string? SearchTb { get => _searchTb; set { _searchTb = value; OnPropertyChanged(nameof(SearchTb)); } }

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

        public UserViewModel()
        {
            LoadProduct();
            SearchCommand = new RelayCommand(SearchProduct);
            AddToPacketCommand = new RelayCommand<Product>(AddProductToUserPacket);
            IncreaseQuantityCommand = new RelayCommand<Product>(IncreaseQuantity);
            DecreaseQuantityCommand = new RelayCommand<Product>(DecreaseQuantity);
            ActiveClockShow();
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
        private void AddProductToUserPacket(Product product)
        {
            var productList = new List<Product>();
            if (PathCheck.OpenOrClosed(_userFileName))
            {
                var existingProducts = DB.JsonRead<Product>(_userFileName);
                if (existingProducts is not null)
                    productList.AddRange(existingProducts);
            }
            productList.Add(product);
            DB.JsonWrite(_userFileName, productList);
        }
        private void LoadProduct()
        {
            if (PathCheck.OpenOrClosed(_productPath))
            {
                var products = DB.JsonRead<Product>(_productPath) ?? throw new ArgumentNullException("Argument is null!");
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

            if (PathCheck.OpenOrClosed(_productPath))
            {
                var matchedProducts = DB.JsonRead<Product>(_productPath).Where(p => p.Name!.ToLower().Contains(SearchTb.ToLower())).ToList() ?? throw new Exception();

                if (matchedProducts.Any()) matchedProducts.ForEach(p => Products.Add(p));
            }
            SearchTb = string.Empty;
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}