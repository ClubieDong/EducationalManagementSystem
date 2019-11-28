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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace EducationalManagementSystem.Client.UserControls
{
    /// <summary>
    /// ObjectSelector.xaml 的交互逻辑
    /// </summary>
    public partial class ObjectSelector : UserControl
    {
        public ObjectSelector()
        {
            InitializeComponent();
        }

        public IEnumerable<object> ItemsSource
        {
            get => (IEnumerable<object>)GetValue(ItemsSourceProperty);
            set => SetValue(ItemsSourceProperty, value);
        }
        public static readonly DependencyProperty ItemsSourceProperty = DependencyProperty.Register("ItemsSource", typeof(IEnumerable<object>), typeof(ObjectSelector), new PropertyMetadata(null));

        public IEnumerable<object> FilteredItemsSource
        {
            get => (IEnumerable<object>)GetValue(FilteredItemsSourceProperty);
            private set => SetValue(FilteredItemsSourceProperty, value);
        }
        public static readonly DependencyProperty FilteredItemsSourceProperty = DependencyProperty.Register("FilteredItemsSource", typeof(IEnumerable<object>), typeof(ObjectSelector), new PropertyMetadata(null));

        public object SelectedItem
        {
            get => GetValue(SelectedItemProperty);
            set
            {
                SetValue(SelectedItemProperty, value);
                if (value != null)
                    txt.Text = value.ToString();
            }
        }
        public static readonly DependencyProperty SelectedItemProperty = DependencyProperty.Register("SelectedItem", typeof(object), typeof(ObjectSelector), new PropertyMetadata(null));

        public new Thickness Padding
        {
            get => (Thickness)GetValue(PaddingProperty);
            set => SetValue(PaddingProperty, value);
        }
        public static new readonly DependencyProperty PaddingProperty = DependencyProperty.Register("Padding", typeof(Thickness), typeof(ObjectSelector), new PropertyMetadata(new Thickness()));

        public double ListMaxHeight
        {
            get => (double)GetValue(ListMaxHeightProperty);
            set => SetValue(ListMaxHeightProperty, value);
        }
        public static readonly DependencyProperty ListMaxHeightProperty = DependencyProperty.Register("ListMaxHeight", typeof(double), typeof(ObjectSelector), new PropertyMetadata(100.0));

        private void UpdateFilteredItemsSource()
        {
            if (ItemsSource == null)
            {
                FilteredItemsSource = null;
                return;
            }
            string text = txt.Text.Trim();
            if (string.IsNullOrEmpty(text))
                FilteredItemsSource = ItemsSource;
            else
                FilteredItemsSource = ItemsSource.Where(n => n.ToString().Contains(text));
            if (FilteredItemsSource.Count() == 0)
            {
                tb.Text = "搜索无结果";
                lst.Visibility = Visibility.Collapsed;
            }
            else
            {
                tb.Text = "搜索结果：";
                lst.Visibility = Visibility.Visible;
            }
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            UpdateFilteredItemsSource();
        }

        private void OnTextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateFilteredItemsSource();
        }

        private void OnGotFocus(object sender, RoutedEventArgs e)
        {
            UpdateFilteredItemsSource();
        }

        private void OnLostFocus(object sender, RoutedEventArgs e)
        {
            string text = txt.Text.Trim();
            if (string.IsNullOrEmpty(text) || FilteredItemsSource.Count() == 0)
                SelectedItem = null;
            else
                SelectedItem = FilteredItemsSource.First();
        }

        private void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lst.SelectedItem != null)
                SelectedItem = lst.SelectedItem;
        }
    }
}
