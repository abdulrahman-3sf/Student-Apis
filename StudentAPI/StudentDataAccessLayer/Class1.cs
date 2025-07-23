using System;
using System.Data;
using Microsoft.Data.SqlClient;

namespace StudentDataAccessLayer
{
    public class StudentDTO
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public int Grade { get; set; }

        public StudentDTO(int ID, string Name, int Age, int Grade)
        {
            this.ID = ID;
            this.Name = Name;
            this.Age = Age;
            this.Grade = Grade;
        }
    }

    public class StudentData
    {
        static string _connectionstring = "Server=localhost;Database=StudentsDB;User Id=sa;Password=123456;Encrypt=False;TrustServerCertificate=True;Connection Timeout=30;";
        
        public static List<StudentDTO> GetAllStudents()
        {
            var StudentsList = new List<StudentDTO>();

            using (SqlConnection connection = new SqlConnection(_connectionstring))
            {
                using (SqlCommand command = new SqlCommand("SP_GetAllStudents", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    connection.Open();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            StudentsList.Add(new StudentDTO(
                                reader.GetInt32(reader.GetOrdinal("Id")),
                                reader.GetString(reader.GetOrdinal("Name")),
                                reader.GetInt32(reader.GetOrdinal("Age")),
                                reader.GetInt32(reader.GetOrdinal("Grade"))
                            ));
                        }
                    }
                }
            }

            return StudentsList;
        }
    }
}
