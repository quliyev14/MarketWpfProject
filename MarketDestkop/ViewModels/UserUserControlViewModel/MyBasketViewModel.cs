using GalaSoft.MvvmLight.Command;
using MarketWpfProject.Data;
using MarketWpfProject.Helper.PathHelper;
using MarketWpfProject.Moduls;

namespace MarketWpfProject.ViewModels.UserUserControlViewModel
{
    public class MyBasketViewModel
    {
        private readonly string path = "mypacket.json";
        public List<Product> Products { get; set; }
        public RelayCommand<Product> DeleteFromBasketCommand { get; set; }
        public MyBasketViewModel()
        {
            Products = new List<Product>();
            DeleteFromBasketCommand = new RelayCommand<Product>(MyBasketDelete);
            LoadProduct();
        }
        private async void MyBasketDelete(Product product)
        {
            if (Products is not null)
            {
                var products = await DB.JsonRead<Product>(path) ?? new List<Product>();

                if (products is not null)
                {
                    Products.Remove(product);
                    DB.JsonWrite<Product>(path, Products);
                }
            }
        }

        private async void LoadProduct()
        {
            if (PathCheck.OpenOrClosed(path))
            {
                var products = await DB.JsonRead<Product>(path) ?? throw new ArgumentNullException("Argument is null!");
                foreach (var product in products)
                    Products.Add(product);
            }
        }
    }
}
