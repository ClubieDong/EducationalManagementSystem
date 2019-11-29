using EducationalManagementSystem.Client.Models.ApplicationModels;
using System;
using System.Globalization;
using System.Windows.Data;

namespace EducationalManagementSystem.Client.Views.ValueConverters
{
    public class ApplicationStateConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return null;
            switch ((Application.AuditState)value)
            {
                case Application.AuditState.Auditing:
                    return "待审核";
                case Application.AuditState.Approved:
                    return "审核通过";
                case Application.AuditState.Disapproved:
                    return "审核未通过";
                case Application.AuditState.Cancelled:
                    return "已取消";
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
                case "待审核":
                    return Application.AuditState.Auditing;
                case "审核通过":
                    return Application.AuditState.Approved;
                case "审核未通过":
                    return Application.AuditState.Disapproved;
                case "已取消":
                    return Application.AuditState.Cancelled;
                default:
                    return null;
            }
        }
    }
}
