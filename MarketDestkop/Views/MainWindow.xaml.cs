using MarketWpfProject.UserControls.UserUS;
using System.Windows;
using System.Windows.Controls;

namespace MarketWpfProject.Views
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
            LoadProductCatalog();
        }

        private void LoadProductCatalog()
        {
            var catologs = new string[]
            {
                    "Products"
            };

            foreach (var cat in catologs)
                CategoryListBox.Items.Add(cat);
        }

        private void CategoryListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CategoryListBox.SelectedItem is string selectedCategory)
                if (selectedCategory == "Products")
                    ContentFrame.Navigate(new ProductUS());
        }
    }
}