﻿using MarketWpfProject.ViewModels.UserUserControlViewModel;
using System.Windows.Controls;

namespace MarketWpfProject.UserControls.UserUS
{
    public partial class ProductUS : UserControl
    {
        public ProductUS()
        {
            InitializeComponent();
            DataContext = new UserViewModel();
        }
    }
}
