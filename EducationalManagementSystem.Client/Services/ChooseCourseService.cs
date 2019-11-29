using EducationalManagementSystem.Client.Models.CourseModels;
using EducationalManagementSystem.Client.Models.UserModels;
using EducationalManagementSystem.Client.Services.Exceptions;
using System.Collections.Generic;

namespace EducationalManagementSystem.Client.Services
{
    public interface IChooseCourseService
    {
        List<Student> GetStudentList(Class c);
        List<Class> GetClassList(Student student);
        void ChooseCourse(Student student, List<Class> classList);
    }

    public class ChooseCourseServiceFactory
    {
        public static IChooseCourseService ChooseCourseService { get; } = new DatabaseChooseCourseService();
    }

    public class DatabaseChooseCourseService : IChooseCourseService
    {
        public List<Student> GetStudentList(Class c)
        {
            var cmd = DatabaseDataService.Command;
            var idList = new List<uint>();
            cmd.CommandText = $"SELECT Student FROM Class_Student WHERE Class = {c.ID}";
            using (var reader = cmd.ExecuteReader())
                while (reader.Read())
                    idList.Add((uint)reader[0]);
            var result = new List<Student>();
            foreach (var id in idList)
            {
                var data = (Student)DataServiceFactory.DataService.CreateObject(id, typeof(Student));
                result.Add(data);
            }
            return result;
        }

        public List<Class> GetClassList(Student student)
        {
            var cmd = DatabaseDataService.Command;
            var idList = new List<uint>();
            cmd.CommandText = $"SELECT Class FROM Class_Student WHERE Student = {student.ID}";
            using (var reader = cmd.ExecuteReader())
                while (reader.Read())
                    idList.Add((uint)reader[0]);
            var result = new List<Class>();
            foreach (var id in idList)
            {
                var data = (Class)DataServiceFactory.DataService.CreateObject(id, typeof(Class));
                result.Add(data);
            }
            return result;
        }

        public void ChooseCourse(Student student, List<Class> classList)
        {
            var activityList = student.ActivityList;
            var lessonList = new List<Lesson>();
            // 检查所选课程中是否有时间重叠的
            foreach (var c in classList)
                foreach (var lesson in c.LessonList)
                {
                    var checkResult = lesson.CheckOverlap(lessonList);
                    if (checkResult != null)
                        throw new ActivityOverlapException()
                        {
                            OverlappedActivity1 = checkResult,
                            OverlappedActivity2 = lesson,
                        };
                    lessonList.Add(lesson);
                }
            // 检查所选课程中是否有与学生已选课程时间重叠的
            foreach (var lesson in lessonList)
            {
                var checkResult = lesson.CheckOverlap(activityList);
                if (checkResult != null)
                    throw new ActivityOverlapException()
                    {
                        OverlappedActivity1 = checkResult,
                        OverlappedActivity2 = lesson,
                    };
            }
            // 确认无误，开始选课
            var cmd = DatabaseDataService.Command;
            foreach (var c in classList)
            {
                cmd.CommandText = $"INSERT INTO Class_Student (Class, Student) VALUES ({c.ID}, {student.ID})";
                cmd.ExecuteNonQuery();
                student.ClassList.Add(c);
                c.StudentList.Add(student);
            }
        }
    }
}
