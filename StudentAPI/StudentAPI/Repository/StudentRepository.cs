using StudentAPI.Model;

namespace StudentAPI.Repository
{
    public class StudentRepository
    {
        public static List<Student> StudentList = new List<Student>
        {
            new Student{ID = 1, Name = "Ali", Age = 20, Grade = 32},
            new Student{ID = 2, Name = "Ahmed", Age = 21, Grade = 95},
            new Student{ID = 3, Name = "Khaled", Age = 19, Grade = 45},
            new Student{ID = 4, Name = "Fasil", Age = 18, Grade = 98}
        };
    }
}
