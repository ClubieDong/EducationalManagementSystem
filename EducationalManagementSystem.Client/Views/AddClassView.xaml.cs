﻿using EducationalManagementSystem.Client.ViewModels;
using System.Windows;
using System.Windows.Controls;

namespace EducationalManagementSystem.Client.Views
{
    /// <summary>
    /// AddClassView.xaml 的交互逻辑
    /// </summary>
    public partial class AddClassView : UserControl
    {
        public AddClassView()
        {
            InitializeComponent();
        }

        private void VisibilityChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            var vm = (AddClassViewModel)DataContext;
            if (Visibility == Visibility.Visible)
                vm.Show();
        }
    }
}
