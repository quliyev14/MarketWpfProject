using MarketWpfProject.Pages;
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
                    "Fruit, Vegatable",
                    "Meat, Chichken, Fish",
                    "Milk",
                    "Cake, Bread",
                    "Drinks",
                    "Basic food",
            };

            foreach (var cat in catologs)
                CategoryListBox.Items.Add(cat);
        }

        private void CategoryListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CategoryListBox.SelectedItem is string selectedCategory)
            {
                if (selectedCategory == "Fruit, Vegatable")
                    ContentFrame.Navigate(new FruitVegetableUS());
                else if (selectedCategory == "Meat, Chichken, Fish") return;
                //ContentFrame.Navigate(new MeatChichkenFishPage());
                else if (selectedCategory == "Milk") return;
                //ContentFrame.Navigate(new MilkPage());
                else if (selectedCategory == "Cake, Bread") return;
                //ContentFrame.Navigate(new CakeBreadPage());
                else if (selectedCategory == "Drinks") return;

                //ContentFrame.Navigate(new DrinksPage());
                else if (selectedCategory == "Basic food") return;
                //ContentFrame.Navigate(new BasicFoodPage());
            }
        }
    }
}