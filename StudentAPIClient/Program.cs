using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace StudentAPIClient
{
    class Program
    {
        static readonly HttpClient httpClient = new HttpClient();

        static async Task Main(string[] args)
        {
            httpClient.BaseAddress = new Uri("https://localhost:7258/api/Students/");

            await GetAllStudents();

            await GetPassedStudents();

            await GetAverageGrades();

            await GetStudentByID(1);
            await GetStudentByID(44);
            await GetStudentByID(-1);

            var newStudent = new Student { Name = "Saad", Age = 21, Grade = 66 };
            await AddNewStudent(newStudent);

            await DeleteStudent(3);

            var updatedStudent = new Student { Name = "Omar", Age = 22, Grade = 100 };
            await UpdateStudent(1, updatedStudent);
        }

        static async Task GetAllStudents()
        {
            try
            {
                Console.WriteLine("-------------------------------");
                Console.WriteLine("\nFetching All Students..\n");

                var students = await httpClient.GetFromJsonAsync<List<Student>>("All");

                if (students != null)
                    foreach (var student in students)
                        Console.WriteLine($"ID: {student.ID}, Name: {student.Name}, Age: {student.Age}, Grade: {student.Grade}");
            } 
            catch (Exception ex)
            {
                Console.WriteLine("ERROR: " + ex.Message);
            }
        }

        static async Task GetPassedStudents()
        {
            try
            {
                Console.WriteLine("-------------------------------");
                Console.WriteLine("\nFetching Passed Students..\n");

                var students = await httpClient.GetFromJsonAsync<List<Student>>("Passed");

                if (students != null)
                    foreach (var student in students)
                        Console.WriteLine($"ID: {student.ID}, Name: {student.Name}, Age: {student.Age}, Grade: {student.Grade}");
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR: " + ex.Message);
            }
        }

        static async Task GetAverageGrades()
        {
            try
            {
                Console.WriteLine("-------------------------------");
                Console.WriteLine("\nFetching Students Average Grades..\n");

                var averageGrades = await httpClient.GetFromJsonAsync<float>("AverageGrades");

                Console.WriteLine("Average Grades: " + averageGrades);
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR: " + ex.Message);
            }
        }

        static async Task GetStudentByID(int ID)
        {
            try
            {
                Console.WriteLine("-------------------------------");
                Console.WriteLine($"\nFetching Student with ID {ID}..\n");

                var response = await httpClient.GetAsync($"{ID}");

                if (response.IsSuccessStatusCode)
                {
                    var student = await response.Content.ReadFromJsonAsync<Student>();

                    if (student != null)
                        Console.WriteLine($"ID: {student.ID}, Name: {student.Name}, Age: {student.Age}, Grade: {student.Grade}");
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                    Console.WriteLine($"Not Found: Student with ID {ID} not found.");

                else if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
                    Console.WriteLine($"Bad Request: Not accepted ID {ID}");

                else
                    Console.WriteLine($"Something Wrong! Status Code ERROR: " + response.StatusCode);
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR: " + ex.Message);
            }
        }

        static async Task AddNewStudent(Student newStudent)
        {
            try
            {
                Console.WriteLine("-------------------------------");
                Console.WriteLine("\nAdding New Student..\n");

                var response = await httpClient.PostAsJsonAsync("AddNewStudent", newStudent);

                if (response.IsSuccessStatusCode)
                {
                    var addedStudent = await response.Content.ReadFromJsonAsync<Student>();
                    Console.WriteLine($"Added Student Data | ID: {addedStudent.ID}, Name: {addedStudent.Name}, Age: {addedStudent.Age}, Grade: {addedStudent.Grade}");
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
                    Console.WriteLine("Bad Request: Invalid Student Data.");

                else
                    Console.WriteLine($"Something Wrong! Status Code ERROR: " + response.StatusCode);
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR: " + ex.Message);
            }
        }

        static async Task DeleteStudent(int ID)
        {
            try
            {
                Console.WriteLine("-------------------------------");
                Console.WriteLine($"\nDeleting student with ID {ID}..\n");

                var response = await httpClient.DeleteAsync($"{ID}");

                if (response.IsSuccessStatusCode)
                    Console.WriteLine($"Student with ID {ID} has been deleted.");

                else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                    Console.WriteLine($"Not Found: Student with ID {ID} not found.");

                else if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
                    Console.WriteLine($"Bad Request: Not accepted ID {ID}");

                else
                    Console.WriteLine($"Something Wrong! Status Code ERROR: " + response.StatusCode);
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR: " + ex.Message);
            }
        }

        static async Task UpdateStudent(int ID, Student updatedStudent)
        {
            try
            {
                Console.WriteLine("\n_____________________________");
                Console.WriteLine($"\nUpdating student with ID {ID}..\n");

                var response = await httpClient.PutAsJsonAsync($"{ID}", updatedStudent);

                if (response.IsSuccessStatusCode)
                {
                    var student = await response.Content.ReadFromJsonAsync<Student>();

                    Console.WriteLine($"Updated Student Data | ID: {student.ID}, Name: {student.Name}, Age: {student.Age}, Grade: {student.Grade}");
                }

                else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                    Console.WriteLine($"Student with ID {ID} not found.");

                else if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
                    Console.WriteLine("Failed to update student: Invalid data.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }
    }

    public class Student
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public int Grade { get; set; }
    }
}