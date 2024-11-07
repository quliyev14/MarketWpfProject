﻿using MarketWpfProject.ViewModels;
using System.Windows;

namespace MarketWpfProject.Views
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            var viewmodel = new MainWindowViewModel(ContentFrame);
            DataContext = viewmodel;
        }
    }
}