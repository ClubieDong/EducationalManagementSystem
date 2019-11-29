using EducationalManagementSystem.Client.Models.CourseModels;
using System;
using System.Globalization;
using System.Windows.Data;

namespace EducationalManagementSystem.Client.Views.ValueConverters
{
    public class ScoreTypeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return null;
            switch ((Score.ScoreType)value)
            {
                case Score.ScoreType.Hundred:
                    return "百分制";
                case Score.ScoreType.Level:
                    return "等级制";
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
                case "百分制":
                    return Score.ScoreType.Hundred;
                case "等级制":
                    return Score.ScoreType.Level;
                default:
                    return null;
            }
        }
    }
}
