using MarketWpfProject.Pages;
using MarketWpfProject.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace MarketWpfProject.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
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
                    ContentFrame.Navigate(new FruitVegatablePage());
                else if (selectedCategory == "Meat, Chichken, Fish")
                    ContentFrame.Navigate(new MeatChichkenFishPage());
                else if (selectedCategory == "Milk")
                    ContentFrame.Navigate(new MilkPage());
                else if (selectedCategory == "Cake, Bread")
                    ContentFrame.Navigate(new CakeBreadPage());
                else if (selectedCategory == "Drinks")
                    ContentFrame.Navigate(new DrinksPage());
                else if (selectedCategory == "Basic food")
                    ContentFrame.Navigate(new BasicFoodPage());
            }
        }
    }
}