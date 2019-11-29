using EducationalManagementSystem.Client.Models.ApplicationModels;
using System;
using System.Globalization;
using System.Windows.Data;

namespace EducationalManagementSystem.Client.Views.ValueConverters
{
    public class ApplicationTypeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return null;
            if (value is AddCourseApplication)
                return "添加课程";
            if (value is AddExaminationApplication)
                return "添加考试";
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
