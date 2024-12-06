using MarketWpfProject.Models;
using System.Windows;

namespace MarketDestkop
{
    public partial class App : Application
    {
        public const string UserPath = "Users.json";
        public const string ProductPath = "products.json";
        public static decimal TotalAmount = 0;
        public static User? CurrentUser { get; set; }
        public static string? Password { get; set; }

        public App()
        {
            CurrentUser = null;
            Password = string.Empty;
        }
    }
}