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
    }
}
