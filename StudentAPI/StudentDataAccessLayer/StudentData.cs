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

        public static List<StudentDTO> GetPassedStudents()
        {
            var PassedStudentsList = new List<StudentDTO>();

            using (SqlConnection connection = new SqlConnection(_connectionstring))
            {
                using (SqlCommand command = new SqlCommand("SP_GetPassedStudents", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    connection.Open();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            PassedStudentsList.Add(new StudentDTO(
                                reader.GetInt32(reader.GetOrdinal("Id")),
                                reader.GetString(reader.GetOrdinal("Name")),
                                reader.GetInt32(reader.GetOrdinal("Age")),
                                reader.GetInt32(reader.GetOrdinal("Grade"))
                            ));
                        }
                    }
                }
            }

            return PassedStudentsList;
        }

        public static double GetAverageGrade()
        {
            double averageGrade = 0;

            using (SqlConnection connection = new SqlConnection(_connectionstring))
            {
                using (SqlCommand command = new SqlCommand("SP_GetAverageGrade", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    connection.Open();

                    object result = command.ExecuteScalar();

                    if (result != DBNull.Value)
                        averageGrade = Convert.ToDouble(result);
                }
            }

            return averageGrade;
        }

        public static StudentDTO GetStudentByID(int ID)
        {
            using (SqlConnection connection = new SqlConnection(_connectionstring))
            {
                using (SqlCommand command = new SqlCommand("SP_GetStudentById", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@StudentId", ID);

                    connection.Open();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new StudentDTO(
                                reader.GetInt32(reader.GetOrdinal("Id")),
                                reader.GetString(reader.GetOrdinal("Name")),
                                reader.GetInt32(reader.GetOrdinal("Age")),
                                reader.GetInt32(reader.GetOrdinal("Grade"))
                            );
                        }

                        return null;
                    }
                }
            }
        }

        public static int AddNewStudent(StudentDTO sDTO)
        {
            using (SqlConnection connection = new SqlConnection(_connectionstring))
            {
                using (SqlCommand command = new SqlCommand("SP_AddStudent", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@Name", sDTO.Name);
                    command.Parameters.AddWithValue("@Age", sDTO.Age);
                    command.Parameters.AddWithValue("@Grade", sDTO.Grade);
                    
                    var OutputIdParam = new SqlParameter("@NewStudentId", SqlDbType.Int) { Direction = ParameterDirection.Output };
                    command.Parameters.Add(OutputIdParam);

                    connection.Open();
                    command.ExecuteNonQuery();

                    return (int)OutputIdParam.Value;
                }
            }
        }
    }
}
