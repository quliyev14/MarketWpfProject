using MarketWpfProject.Models;
using System.Windows;

namespace MarketDestkop
{
    public partial class App : Application
    {
        public static User? CurrentUser { get; set; }

        public App()
        {
            CurrentUser = null;
        }
    }
}