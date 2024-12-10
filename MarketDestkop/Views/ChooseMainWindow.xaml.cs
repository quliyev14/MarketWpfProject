using System.Windows;
using MarketWpfProject.ViewModels;

namespace MarketWpfProject.Views
{
    public partial class ChooseMainWindow : Window
    {
        public ChooseMainWindow()
        {
            InitializeComponent();
            DataContext = new ChooseMainViewModels();
        }
    }
}
