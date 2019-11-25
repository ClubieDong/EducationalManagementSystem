using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace EducationalManagementSystem.Client.ViewModels.ValueConverters
{
    public class VisibilityConverter : IValueConverter
    {
        public Type Type { get; set; }
        public bool Show { get; set; } = true;

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return Visibility.Collapsed;
            var type = value.GetType();
            if (type == Type || type.IsSubclassOf(Type))
                return Show ? Visibility.Visible : Visibility.Collapsed;
            else
                return Show ? Visibility.Collapsed : Visibility.Visible;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
