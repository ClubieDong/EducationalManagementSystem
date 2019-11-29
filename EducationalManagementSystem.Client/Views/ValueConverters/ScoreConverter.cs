using EducationalManagementSystem.Client.Models.CourseModels;
using System;
using System.Globalization;
using System.Windows.Data;

namespace EducationalManagementSystem.Client.Views.ValueConverters
{
    public class ScoreConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return null;
            var score = (Score)value;
            return $"{score.Level} / {score.Grade}";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var str = (string)value;
            if (string.IsNullOrEmpty(str))
                return null;
            var result = new Score();
            switch (str)
            {
                case "A":
                    result.Level = Score.ScoreLevel.A;
                    break;
                case "B":
                    result.Level = Score.ScoreLevel.B;
                    break;
                case "C":
                    result.Level = Score.ScoreLevel.C;
                    break;
                case "D":
                    result.Level = Score.ScoreLevel.D;
                    break;
                case "E":
                    result.Level = Score.ScoreLevel.E;
                    break;
                default:
                    if (double.TryParse(str, out double grade))
                        result.Grade = grade;
                    else
                        throw new ArgumentOutOfRangeException();
                    break;
            }
            return result;
        }
    }
}
