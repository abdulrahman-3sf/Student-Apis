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
        public class Student
        {
            public int ID { get; set; }
            public string Name { get; set; }
            public int Age { get; set; }
            public int Grade { get; set; }
        }

        static HttpClient httpClient = new HttpClient();

        static async Task GetAllStudents()
        {
            try
            {
                Console.WriteLine("-------------------------------");
                Console.WriteLine("\nFetching All Students..\n");

                var students = await httpClient.GetFromJsonAsync<List<Student>>("students");

                if (students != null)
                    foreach (var student in students)
                        Console.WriteLine($"ID: {student.ID}, Name: {student.Name}, Age: {student.Age}, Grade: {student.Grade}");
            } 
            catch (Exception ex)
            {
                Console.WriteLine("ERROR: " + ex.Message);
            }
        }

        static async Task Main(string[] args)
        {
            httpClient.BaseAddress = new Uri("https://localhost:7258/api/Students");

            await GetAllStudents();
        }
    }
}