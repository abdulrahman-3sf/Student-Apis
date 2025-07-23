using System.Data;
using Azure.Core;
using StudentDataAccessLayer;

namespace StudentAPIBusinessLayer
{
    public class Student
    {
        public enum enMode { AddNew = 0, Update = 1 };
        public enMode Mode = enMode.AddNew;

        public StudentDTO sDTO
        {
            get { return new StudentDTO(this.ID, this.Name, this.Age, this.Grade); }
        }

        public int ID { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public int Grade { get; set; }

        public Student(StudentDTO sDTO, enMode Mode = enMode.AddNew)
        {
            this.ID = sDTO.ID;
            this.Name = sDTO.Name;
            this.Age = sDTO.Age;
            this.Grade = sDTO.Grade;
            this.Mode = Mode;
        }

        private bool _AddNewStudent()
        {
            this.ID = StudentData.AddNewStudent(sDTO);

            return (ID != -1);
        }

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

        public static Student Find(int ID)
        {
            StudentDTO sDTO = StudentData.GetStudentByID(ID);

            if (sDTO != null)
                return new Student(sDTO, enMode.Update);

            else
                return null;
        }

        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewStudent())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    else
                        return false;

                default:
                    return false;
            }
        }
    }
}
