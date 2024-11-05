using MarketWpfProject.Moduls;
using MarketWpfProject.ViewModels.AdminPanelUserControlViewModel;
using System.Windows;

namespace MarketWpfProject.Views
{
    public partial class EditWindow : Window
    {
        public EditWindow(Product selectedProduct)
        {
            InitializeComponent();
            DataContext = new EditProductViewModel(selectedProduct);
        }
    }
}
