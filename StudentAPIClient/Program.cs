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
    }

    public class Student
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public int Grade { get; set; }
    }
}