using EducationalManagementSystem.Client.Models.CourseModels;
using System;
using System.Globalization;
using System.Windows.Data;

namespace EducationalManagementSystem.Client.Views.ValueConverters
{
    public class PublicityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return null;
            switch ((Course.PublicityType)value)
            {
                case Course.PublicityType.Major:
                    return "仅限本专业";
                case Course.PublicityType.College:
                    return "仅限本学院";
                case Course.PublicityType.University:
                    return "全校学生";
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return null;
            switch ((string)value)
            {
                case "仅限本专业":
                    return Course.PublicityType.Major;
                case "仅限本学院":
                    return Course.PublicityType.College;
                case "全校学生":
                    return Course.PublicityType.University;
                default:
                    return null;
            }
        }
    }
}
