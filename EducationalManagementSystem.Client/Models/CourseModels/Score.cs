using System;

namespace EducationalManagementSystem.Client.Models.CourseModels
{
    public struct Score
    {
        public enum ScoreType
        {
            Hundred,
            Level
        }

        public enum ScoreLevel
        {
            A, B, C, D, E
        }

        public double Grade { get; set; }

        public ScoreLevel Level
        {
            get
            {
                if (Grade >= 95)
                    return ScoreLevel.A;
                else if (Grade >= 85)
                    return ScoreLevel.B;
                else if (Grade >= 75)
                    return ScoreLevel.C;
                else if (Grade >= 65)
                    return ScoreLevel.D;
                else
                    return ScoreLevel.E;
            }
            set
            {
                switch (value)
                {
                    case ScoreLevel.A:
                        Grade = 95;
                        break;
                    case ScoreLevel.B:
                        Grade = 85;
                        break;
                    case ScoreLevel.C:
                        Grade = 75;
                        break;
                    case ScoreLevel.D:
                        Grade = 65;
                        break;
                    case ScoreLevel.E:
                        Grade = 0;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }
    }
}
