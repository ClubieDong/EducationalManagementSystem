using EducationalManagementSystem.Client.Models.UserModels;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace EducationalManagementSystem.Client.ViewModels.ValueConverters
{
    public class GenderConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var gender = (User.GenderType?)value;
            if (!gender.HasValue)
                return string.Empty;
            else if (gender == User.GenderType.Male)
                return "男";
            else
                return "女";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
