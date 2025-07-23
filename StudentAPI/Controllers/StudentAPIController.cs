using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudentAPI.Model;
using StudentAPI.Repository;

namespace StudentAPI.Controllers
{
    // [Route("api/[controller]")]
    [Route("api/Students")]
    [ApiController]
    public class StudentAPIController : ControllerBase
    {
        [HttpGet("All", Name = "GetAllStudents")]
        public ActionResult<IEnumerable<Student>> GetAllStudents()
        {
            return Ok(StudentRepository.StudentList);
        }

        [HttpGet("Passed", Name = "GetPassedStudents")]
        public ActionResult<IEnumerable<Student>> GetPassedStudents()
        {
            var passedStudents = StudentRepository.StudentList.Where(student => student.Grade >= 50).ToList();
            return Ok(passedStudents);
        }

        [HttpGet("AverageGrades", Name = "GetAverageGrades")]
        public ActionResult<double> GetAverageGrades()
        {
            if (StudentRepository.StudentList.Count == 0)
                return NotFound("No Students Found!");

            var averageGrades = StudentRepository.StudentList.Average(student => student.Grade);
            return Ok(averageGrades);
        }
    }
}
