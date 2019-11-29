using EducationalManagementSystem.Client.Models;
using EducationalManagementSystem.Client.Models.CourseModels;
using EducationalManagementSystem.Client.Models.UserModels;
using EducationalManagementSystem.Client.Services;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace EducationalManagementSystem.Client.ViewModels
{
    public class InputScoreViewModel : BindableBase
    {
        public class DataType
        {
            public class ScoreDataType
            {
                public Examination Examination { get; set; }
                public Score? Score { get; set; }
            }
            public Student Student { get; set; }
            public List<ScoreDataType> ScoreList { get; set; }
            public Score? FinalScore { get; set; }
        }

        public InputScoreViewModel()
        {
            SubmitCommand = new DelegateCommand(Submit, CanSubmit);
        }

        public MainWindowViewModel MainVM { get; set; }

        private Class _Class;
        public Class Class
        {
            get => _Class;
            set
            {
                SetProperty(ref _Class, value);
                UpdateData();
            }
        }

        private List<Class> _ClassList;
        public List<Class> ClassList
        {
            get => _ClassList;
            set => SetProperty(ref _ClassList, value);
        }

        private List<DataType> _Data;
        public List<DataType> Data
        {
            get => _Data;
            set => SetProperty(ref _Data, value);
        }

        private void UpdateData()
        {
            if (Class == null)
                return;
            var result = new List<DataType>();
            foreach (var student in Class.StudentList)
            {
                var data = new DataType()
                {
                    Student = student,
                    ScoreList = new List<DataType.ScoreDataType>()
                };
                if (Class.ScoreList.TryGetValue(student, out Score tempScore))
                    data.FinalScore = tempScore;
                foreach (var examination in Class.ExaminationList)
                {
                    var scoreData = new DataType.ScoreDataType()
                    {
                        Examination = examination,
                    };
                    if (examination.ScoreList.TryGetValue(student, out tempScore))
                        scoreData.Score = tempScore;
                    data.ScoreList.Add(scoreData);
                }
                result.Add(data);
            }
            Data = result;
        }

        public ICommand SubmitCommand { get; }
        private void Submit()
        {
            Class.ScoreList.Clear();
            foreach (var examination in Class.ExaminationList)
                examination.ScoreList.Clear();
            foreach (var stu in Data)
                if (stu.FinalScore != null)
                    Class.ScoreList.Add(stu.Student, stu.FinalScore.Value);
            foreach (var stu in Data)
                foreach (var exam in stu.ScoreList)
                    if (exam.Score != null)
                        exam.Examination.ScoreList.Add(stu.Student, exam.Score.Value);
            ScoreServiceFactory.ScoreService.InputScore(Class);
            MessageBox.Show("录入成绩成功！");
        }
        private bool CanSubmit() => true;

        public void Show()
        {
            ClassList = ScoreServiceFactory.ScoreService.GetClassListOfTeacher((Teacher)MainVM.User);
        }
    }
}
