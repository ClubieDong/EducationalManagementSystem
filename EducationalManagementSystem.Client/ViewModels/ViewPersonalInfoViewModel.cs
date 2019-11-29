using EducationalManagementSystem.Client.Models.UserModels;
using Prism.Mvvm;

namespace EducationalManagementSystem.Client.ViewModels
{
    public class ViewPersonalInfoViewModel : BindableBase
    {
        public MainWindowViewModel MainVM { get; set; }

        private User _User;
        public User User
        {
            get => _User;
            set => SetProperty(ref _User, value);
        }

        public void Show()
        {
            User = MainVM.User;
        }
        public void Hide()
        {
            User = null;
        }
    }
}
