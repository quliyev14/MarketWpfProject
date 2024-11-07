using GalaSoft.MvvmLight.Command;
using MarketWpfProject.Data;
using MarketWpfProject.Helper.PathHelper;
using MarketWpfProject.Moduls;
using System.Collections.ObjectModel;

namespace MarketWpfProject.ViewModels.UserUserControlViewModel
{
    public class MyBasketViewModel
    {
        private readonly string path = "mypacket.json";
        public ObservableCollection<Product> Products { get; set; } = new ObservableCollection<Product>();

        public RelayCommand<Product> DeleteFromBasketCommand { get; set; }


        public MyBasketViewModel()
        {
            DeleteFromBasketCommand = new RelayCommand<Product>(MyBasketDelete);
            LoadProduct();
        }
        private void MyBasketDelete(Product product)
        {
            if (Products is not null)
            {
                var products = DB.JsonRead<Product>(path) ?? new List<Product>();

                if (products is not null)
                {
                    Products.Remove(product);
                    DB.JsonWrite<Product>(path, "mypacket.log", Products);
                }
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
    }
}
