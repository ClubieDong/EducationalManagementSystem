using EducationalManagementSystem.Client.Models.CourseModels;
using EducationalManagementSystem.Client.Models.UserModels;
using EducationalManagementSystem.Client.Services;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace EducationalManagementSystem.Client.ViewModels
{
    public class AddClassViewModel : BindableBase
    {
        public AddClassViewModel()
        {
            AddLessonCommand = new DelegateCommand(AddLesson, CanAddLesson);
            RemoveLessonCommand = new DelegateCommand<Lesson>(RemoveLesson, CanRemoveLesson);
            ConfirmCommand = new DelegateCommand(Confirm, CanConfirm);
        }

        public MainWindowViewModel MainVM { get; set; }

        private Course _Course;
        public Course Course
        {
            get => _Course;
            set => SetProperty(ref _Course, value);
        }

        private Teacher _Teacher;
        public Teacher Teacher
        {
            get => _Teacher;
            set => SetProperty(ref _Teacher, value);
        }

        private string _Name;
        public string Name
        {
            get => _Name;
            set => SetProperty(ref _Name, value);
        }

        private uint? _MaxStudent;
        public uint? MaxStudent
        {
            get => _MaxStudent;
            set => SetProperty(ref _MaxStudent, value);
        }

        private DateTime? _ChooseStartDate;
        public DateTime? ChooseStartDate
        {
            get => _ChooseStartDate;
            set { _ = SetProperty(ref _ChooseStartDate, value); }
        }

        private DateTime? _ChooseStartTime;
        public DateTime? ChooseStartTime
        {
            get => _ChooseStartTime;
            set { _ = SetProperty(ref _ChooseStartTime, value); }
        }

        private DateTime? _ChooseEndDate;
        public DateTime? ChooseEndDate
        {
            get => _ChooseEndDate;
            set { _ = SetProperty(ref _ChooseEndDate, value); }
        }

        private DateTime? _ChooseEndTime;
        public DateTime? ChooseEndTime
        {
            get => _ChooseEndTime;
            set { _ = SetProperty(ref _ChooseEndTime, value); }
        }

        private Classroom _Classroom;
        public Classroom Classroom
        {
            get => _Classroom;
            set => SetProperty(ref _Classroom, value);
        }

        private DateTime? _LessonStartDate;
        public DateTime? LessonStartDate
        {
            get => _LessonStartDate;
            set => SetProperty(ref _LessonStartDate, value);
        }

        private DateTime? _LessonEndDate;
        public DateTime? LessonEndDate
        {
            get => _LessonEndDate;
            set => SetProperty(ref _LessonEndDate, value);
        }

        private DateTime? _LessonStartTime;
        public DateTime? LessonStartTime
        {
            get => _LessonStartTime;
            set => SetProperty(ref _LessonStartTime, value);
        }

        private DateTime? _LessonEndTime;
        public DateTime? LessonEndTime
        {
            get => _LessonEndTime;
            set => SetProperty(ref _LessonEndTime, value);
        }

        private bool _IsMondayChecked;
        public bool IsMondayChecked
        {
            get => _IsMondayChecked;
            set => SetProperty(ref _IsMondayChecked, value);
        }

        private bool _IsTuesdayChecked;
        public bool IsTuesdayChecked
        {
            get => _IsTuesdayChecked;
            set => SetProperty(ref _IsTuesdayChecked, value);
        }

        private bool _IsWednesdayChecked;
        public bool IsWednesdayChecked
        {
            get => _IsWednesdayChecked;
            set => SetProperty(ref _IsWednesdayChecked, value);
        }

        private bool _IsThursdayChecked;
        public bool IsThursdayChecked
        {
            get => _IsThursdayChecked;
            set => SetProperty(ref _IsThursdayChecked, value);
        }

        private bool _IsFridayChecked;
        public bool IsFridayChecked
        {
            get => _IsFridayChecked;
            set => SetProperty(ref _IsFridayChecked, value);
        }

        private bool _IsSaturdayChecked;
        public bool IsSaturdayChecked
        {
            get => _IsSaturdayChecked;
            set => SetProperty(ref _IsSaturdayChecked, value);
        }

        private bool _IsSundayChecked;
        public bool IsSundayChecked
        {
            get => _IsSundayChecked;
            set => SetProperty(ref _IsSundayChecked, value);
        }

        public List<Course> CourseList
        {
            get => Course.CourseList.Select(n => n.Value).ToList();
        }

        public List<Teacher> TeacherList
        {
            get => User.UserList.Where(n => n.Value is Teacher).Select(n => n.Value as Teacher).ToList();
        }

        public List<Classroom> ClassroomList
        {
            get => Classroom.ClassroomList.Select(n => n.Value).ToList();
        }

        private ObservableCollection<Lesson> _LessonList;
        public ObservableCollection<Lesson> LessonList
        {
            get
            {
                if (_LessonList == null)
                    _LessonList = new ObservableCollection<Lesson>();
                return _LessonList;
            }
            set => SetProperty(ref _LessonList, value);
        }

        public ICommand AddLessonCommand { get; }
        private void AddLesson()
        {
            if (Classroom == null)
                MessageBox.Show("请选择教室！");
            else if (LessonStartDate == null)
                MessageBox.Show("请选择开始日期！");
            else if (LessonEndDate == null)
                MessageBox.Show("请选择结束日期！");
            else if (LessonStartDate > LessonEndDate)
                MessageBox.Show("开始日期需要早于结束日期！");
            else if (LessonStartTime == null)
                MessageBox.Show("请选择上课时间！");
            else if (LessonEndTime == null)
                MessageBox.Show("请选择下课时间！");
            else if (LessonStartTime > LessonEndTime)
                MessageBox.Show("上课时间需要早于下课时间！");
            else
            {
                int count = 0;
                for (var date = LessonStartDate.Value; date <= LessonEndDate.Value; date = date.AddDays(1))
                {
                    bool chosen = false;
                    switch (date.DayOfWeek)
                    {
                        case DayOfWeek.Monday:
                            chosen = IsMondayChecked;
                            break;
                        case DayOfWeek.Tuesday:
                            chosen = IsTuesdayChecked;
                            break;
                        case DayOfWeek.Wednesday:
                            chosen = IsWednesdayChecked;
                            break;
                        case DayOfWeek.Thursday:
                            chosen = IsThursdayChecked;
                            break;
                        case DayOfWeek.Friday:
                            chosen = IsFridayChecked;
                            break;
                        case DayOfWeek.Saturday:
                            chosen = IsSaturdayChecked;
                            break;
                        case DayOfWeek.Sunday:
                            chosen = IsSundayChecked;
                            break;
                    }
                    if (chosen)
                    {
                        LessonList.Add(new Lesson()
                        {
                            Classroom = Classroom,
                            StartTime = date + LessonStartTime.Value.TimeOfDay,
                            EndTime = date + LessonEndTime.Value.TimeOfDay
                        });
                        ++count;
                    }
                }
                MessageBox.Show($"已成功添加{count}课时！");
            }
        }
        private bool CanAddLesson() => true;

        public ICommand RemoveLessonCommand { get; }
        private void RemoveLesson(Lesson lesson)
        {
            LessonList.Remove(lesson);
        }
        private bool CanRemoveLesson(Lesson lesson) => true;

        public ICommand ConfirmCommand { get; }
        private void Confirm()
        {
            if (Course == null)
                MessageBox.Show("请选择课程！");
            else if (Teacher == null)
                MessageBox.Show("请选择老师！");
            else if (string.IsNullOrEmpty(Name))
                MessageBox.Show("请输入课程名！");
            else if (MaxStudent == null)
                MessageBox.Show("请输入最大人数！");
            else if (ChooseStartDate == null)
                MessageBox.Show("请选择选课开始日期！");
            else if (ChooseStartTime == null)
                MessageBox.Show("请选择选课开始时间！");
            else if (ChooseEndDate == null)
                MessageBox.Show("请选择选课结束日期！");
            else if (ChooseEndTime == null)
                MessageBox.Show("请选择选课结束时间！");
            else if (ChooseStartDate + ChooseStartTime.Value.TimeOfDay > ChooseEndDate + ChooseEndTime.Value.TimeOfDay)
                MessageBox.Show("选课开始时间需要早于选课结束时间！");
            else if (LessonList.Count() == 0)
                MessageBox.Show("至少添加一个课时！");
            else
            {
                var c = (Class)DataServiceFactory.DataService.NewObject(typeof(Class));
                c.Course = Course;
                c.Teacher = Teacher;
                c.Name = Name;
                c.MaxStudent = MaxStudent;
                c.ChooseStartTime = ChooseStartDate + ChooseStartTime.Value.TimeOfDay;
                c.ChooseEndTime = ChooseEndDate + ChooseEndTime.Value.TimeOfDay;
                foreach (var i in LessonList)
                {
                    var lesson = (Lesson)DataServiceFactory.DataService.NewObject(typeof(Lesson));
                    lesson.Class = c;
                    lesson.Classroom = i.Classroom;
                    lesson.StartTime = i.StartTime;
                    lesson.EndTime = i.EndTime;
                }
                MessageBox.Show("教学班添加成功！");
            }
        }
        private bool CanConfirm() => true;

        public void Show()
        {
            DataServiceFactory.DataService.GetAllObjects(typeof(Course));
            DataServiceFactory.DataService.GetAllObjects(typeof(Teacher));
            DataServiceFactory.DataService.GetAllObjects(typeof(Classroom));
            RaisePropertyChanged(nameof(CourseList));
            RaisePropertyChanged(nameof(TeacherList));
            RaisePropertyChanged(nameof(ClassroomList));
        }
    }
}
