﻿using MarketWpfProject.ViewModels;
using MarketWpfProject.ViewModels.AdminPanelUserControlViewModel;
using System.Windows;

namespace MarketWpfProject.Views
{
    public partial class AdminPanelWindow : Window
    {
        public AdminPanelWindow()
        {
            InitializeComponent();
            DataContext = new AdminPanelViewModel();
        }
    }
}