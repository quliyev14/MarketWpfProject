using GalaSoft.MvvmLight.Command;
using MarketDestkop;
using MarketWpfProject.Data;
using MarketWpfProject.Helper.PathHelper;
using MarketWpfProject.Moduls;

namespace MarketWpfProject.ViewModels.UserUserControlViewModel
{
    public class MyBasketViewModel
    {
        private readonly string _userpath = $"{App.CurrentUser?.GmailService.Email}.json";
        public List<Product> Products { get; set; }
        public RelayCommand<Product> DeleteFromBasketCommand { get; set; }
        public MyBasketViewModel()
        {
            Products = new List<Product>();
            DeleteFromBasketCommand = new RelayCommand<Product>(MyBasketDelete);
            LoadProduct();
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
    }
}
