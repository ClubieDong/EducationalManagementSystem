using EducationalManagementSystem.Client.Models.CourseModels;
using EducationalManagementSystem.Client.Models.UserModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducationalManagementSystem.Client.Services
{
    public interface IScoreService
    {
        Dictionary<Student, Score> GetScoreList(Examination examination);
        Dictionary<Student, Score> GetScoreList(Class c);
        List<Class> GetClassListOfTeacher(Teacher teacher);
        void InputScore(Class c);
    }

    public class ScoreServiceFactory
    {
        public static IScoreService ScoreService { get; } = new DatabaseScoreService();
    }

    public class DatabaseScoreService : IScoreService
    {
        public Dictionary<Student, Score> GetScoreList(Class c)
        {
            var cmd = DatabaseDataService.Command;
            var list = new List<(uint studentID, double Score)>();
            cmd.CommandText = $"SELECT Student, Score FROM Class_Score WHERE Class = {c.ID}";
            using (var reader = cmd.ExecuteReader())
                while (reader.Read())
                    list.Add(((uint)reader[0], (double)reader[1]));
            var result = new Dictionary<Student, Score>();
            foreach (var i in list)
            {
                var student = (Student)DataServiceFactory.DataService.CreateObject(i.studentID, typeof(Student));
                var score = new Score()
                {
                    Grade = i.Score
                };
                result.Add(student, score);
            }
            return result;
        }

        public Dictionary<Student, Score> GetScoreList(Examination examination)
        {
            var cmd = DatabaseDataService.Command;
            var list = new List<(uint studentID, double Score)>();
            cmd.CommandText = $"SELECT Student, Score FROM Examination_Score WHERE Examination = {examination.ID}";
            using (var reader = cmd.ExecuteReader())
                while (reader.Read())
                    list.Add(((uint)reader[0], (double)reader[1]));
            var result = new Dictionary<Student, Score>();
            foreach (var i in list)
            {
                var student = (Student)DataServiceFactory.DataService.CreateObject(i.studentID, typeof(Student));
                var score = new Score()
                {
                    Grade = i.Score
                };
                result.Add(student, score);
            }
            return result;
        }

        public List<Class> GetClassListOfTeacher(Teacher teacher)
        {
            var cmd = DatabaseDataService.Command;
            var idList = new List<uint>();
            cmd.CommandText = $"SELECT ID FROM Class WHERE Teacher = {teacher.ID}";
            using (var reader = cmd.ExecuteReader())
                while (reader.Read())
                    idList.Add((uint)reader[0]);
            var result = new List<Class>();
            foreach (var id in idList)
                result.Add((Class)DataServiceFactory.DataService.CreateObject(id, typeof(Class)));
            return result;
        }

        public void InputScore(Class c)
        {
            var cmd = DatabaseDataService.Command;
            cmd.CommandText = $"DELETE FROM Class_Score WHERE Class = {c.ID}";
            cmd.ExecuteNonQuery();
            foreach (var pair in c.ScoreList)
            {
                cmd.CommandText = $"INSERT INTO Class_Score (Class, Student, Score) VALUES ({c.ID}, {pair.Key.ID}, {pair.Value.Grade})";
                cmd.ExecuteNonQuery();
            }
            foreach (var examination in c.ExaminationList)
            {
                cmd.CommandText = $"DELETE FROM Examination_Score WHERE Examination = {examination.ID}";
                cmd.ExecuteNonQuery();
                foreach (var pair in c.ScoreList)
                {
                    cmd.CommandText = $"INSERT INTO Examination_Score (Examination, Student, Score) VALUES ({examination.ID}, {pair.Key.ID}, {pair.Value.Grade})";
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
