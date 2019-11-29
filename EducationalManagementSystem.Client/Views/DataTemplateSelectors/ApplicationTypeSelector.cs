using EducationalManagementSystem.Client.Models.ApplicationModels;
using System.Windows;
using System.Windows.Controls;

namespace EducationalManagementSystem.Client.Views.DataTemplateSelectors
{
    public class ApplicationTypeSelector : DataTemplateSelector
    {
        public DataTemplate AddCourseApplicationTemplate { get; set; }

        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            if (item is AddCourseApplication)
                return AddCourseApplicationTemplate;
            return null;
        }
    }
}
