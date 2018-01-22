﻿using System;
using System.Windows;
using System.Windows.Media;
using MahApps.Metro.Controls;
using MaterialDesignColors;
using MvxScaffolding.UI.Helpers;
using MvxScaffolding.UI.ViewModels;

namespace MvxScaffolding.UI.Views
{
    public partial class MainWindow : MetroWindow
    {
        public MainWindow()
        {
            InitializeComponent();

            DataContext = new MainViewModel();

            Brush themeBrush = MvxScaffoldingContext.TemplateType == TemplateType.MvxNative
                ? (Brush)FindResource("PrimaryColorBrush")
                : (Brush)FindResource("AccentHueColorBrush");

            var title = MvxScaffoldingContext.TemplateType == TemplateType.MvxNative
                ? "MvxScaffolding - MvvmCross Native"
                : "MvxScaffolding - MvvmCross Forms";

            WindowTitleBrush = themeBrush;
            StatusBar.Background = themeBrush;

            Title = title;
        }
    }
}
