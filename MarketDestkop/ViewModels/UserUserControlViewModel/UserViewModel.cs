using MarketWpfProject.Moduls;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace MarketWpfProject.ViewModels.UserUserControlViewModel
{
    public class UserViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<Product> Products { get; set; } = new ObservableCollection<Product>();

        public UserViewModel()
        {
            LoadProduct();
        }

        private void LoadProduct()
        {
            Products.Add(new Product("Drink 4", 5.0m, 12, string.Empty));
            Products.Add(new Product("Drink 2", 3.0m, 13, string.Empty));
            Products.Add(new Product("Drink 3", 2.0m, 14, string.Empty));
            Products.Add(new Product("Drink 4", 1.0m, 15, string.Empty));
            Products.Add(new Product("Drink 5", 6.0m, 16, string.Empty));
            Products.Add(new Product("Drink 6", 7.0m, 17, string.Empty));
            Products.Add(new Product("Drink 7", 8.0m, 18, string.Empty));
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}