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
            switch ((User.GenderType)value)
            {
                case User.GenderType.Male:
                    return "男";
                case User.GenderType.Female:
                    return "女";
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            switch ((string)value)
            {
                case "男":
                    return User.GenderType.Male;
                case "女":
                    return User.GenderType.Female;
                default:
                    return null;
            }
        }
    }
}
