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
        public string Object { get; set; }
        public bool Negated { get; set; } = false;

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return Visibility.Collapsed;
            if (Object != null)
            {
                if (value.ToString() == Object)
                    return Negated ? Visibility.Collapsed : Visibility.Visible;
                else
                    return Negated ? Visibility.Visible : Visibility.Collapsed;
            }
            if (Type != null)
            {
                var type = value.GetType();
                if (type == Type || type.IsSubclassOf(Type))
                    return Negated ? Visibility.Collapsed : Visibility.Visible;
                else
                    return Negated ? Visibility.Visible : Visibility.Collapsed;
            }
            throw new ArgumentOutOfRangeException();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
