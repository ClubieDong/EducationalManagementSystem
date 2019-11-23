using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;

namespace EducationalManagementSystem.Client.ViewModels.ValueConverters
{
    public class PasswordBoxesConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            var oldPwd = (PasswordBox)values[0];
            var newPwd = (PasswordBox)values[1];
            var confirmPwd = (PasswordBox)values[2];
            return new PasswordBox[] { oldPwd, newPwd, confirmPwd };
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
