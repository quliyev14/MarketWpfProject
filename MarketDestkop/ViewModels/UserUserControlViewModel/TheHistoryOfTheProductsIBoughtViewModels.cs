using Argon;
using MarketDestkop;
using MarketWpfProject.Models;
using MarketWpfProject.Moduls;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;

namespace MarketWpfProject.ViewModels.UserUserControlViewModel
{
    public class TheHistoryOfTheProductsIBoughtViewModels : INotifyPropertyChanged
    {
        private string userHistoryFileName = $"{App.CurrentUser?.GmailService.Email}_PurchasedHistory.json";

        private ObservableCollection<DateTime?> _purchaseHistoryDates;
        private ObservableCollection<Product> _selectedDateProducts;
        private DateTime? _selectedDate;

        public ObservableCollection<DateTime?> PurchaseHistoryDates
        {
            get => _purchaseHistoryDates;
            set
            {
                _purchaseHistoryDates = value;
                OnPropertyChanged(nameof(PurchaseHistoryDates));
            }
        }

        public ObservableCollection<Product> SelectedDateProducts
        {
            get => _selectedDateProducts;
            set
            {
                _selectedDateProducts = value;
                OnPropertyChanged(nameof(SelectedDateProducts));
            }
        }

        public DateTime? SelectedDate
        {
            get => _selectedDate;
            set
            {
                _selectedDate = value;
                OnPropertyChanged(nameof(SelectedDate));
                LoadProductsForSelectedDate(_selectedDate);
            }
        }

        public TheHistoryOfTheProductsIBoughtViewModels()
        {
            PurchaseHistoryDates = new ObservableCollection<DateTime?>();
            SelectedDateProducts = new ObservableCollection<Product>();
            LoadPurchaseHistory();
        }

        private void LoadPurchaseHistory()
        {
            var history = JsonConvert.DeserializeObject<List<PurchaseHistory>>(File.ReadAllText(userHistoryFileName));
            foreach (var purchase in history)
            {
                PurchaseHistoryDates.Add(purchase.PurchaseDate);
            }
        }

        private void LoadProductsForSelectedDate(DateTime? date)
        {
            var history = JsonConvert.DeserializeObject<List<PurchaseHistory>>(File.ReadAllText(userHistoryFileName));
            var selectedHistory = history.FirstOrDefault(h => h.PurchaseDate == date);

            if (selectedHistory != null && selectedHistory.Products != null)
            {
                SelectedDateProducts.Clear();
                foreach (var product in selectedHistory.Products)
                {
                    SelectedDateProducts.Add(product);
                }
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
