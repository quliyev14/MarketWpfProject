using MarketWpfProject.ViewModels;
using MarketWpfProject.ViewModels.UserUserControlViewModel;
using System.Windows;

namespace MarketDestkop.Views
{
    /// <summary>
    /// Interaction logic for RegisterWindow.xaml
    /// </summary>
    public partial class RegisterWindow : Window
    {
        public RegisterWindow()
        {
            InitializeComponent();
            DataContext = new SignInViewModels();
        }
    }
}