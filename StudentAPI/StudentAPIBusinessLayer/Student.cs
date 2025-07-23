using System.Data;
using StudentDataAccessLayer;

namespace StudentAPIBusinessLayer
{
    public class Student
    {
        public static List<StudentDTO> GetAllStudents()
        {
            return StudentData.GetAllStudents();
        }

        public static List<StudentDTO> GetPassedStudents()
        {
            return StudentData.GetPassedStudents();
        }

        public static double GetAverageGrade()
        {
            return StudentData.GetAverageGrade();
        }
    }
}
