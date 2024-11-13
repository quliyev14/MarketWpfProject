using MarketWpfProject.ViewModels.UserUserControlViewModel;
using System.Windows;

namespace MarketDestkop.Views
{
    public partial class RegisterWindow : Window
    {
        public RegisterWindow()
        {
            InitializeComponent();
            DataContext = new SignInViewModels();
        }
    }
}