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
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<IEnumerable<Student>> GetAllStudents()
        {
            if (StudentRepository.StudentList.Count == 0)
                return NotFound("No Students Found!");

            return Ok(StudentRepository.StudentList);
        }

        [HttpGet("Passed", Name = "GetPassedStudents")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<IEnumerable<Student>> GetPassedStudents()
        {
            if (StudentRepository.StudentList.Count == 0)
                return NotFound("No Students Found!");

            var passedStudents = StudentRepository.StudentList.Where(student => student.Grade >= 50).ToList();

            if (passedStudents.Count == 0)
                return NotFound("No Passed Students Found!");

            return Ok(passedStudents);
        }

        [HttpGet("AverageGrades", Name = "GetAverageGrades")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<double> GetAverageGrades()
        {
            if (StudentRepository.StudentList.Count == 0)
                return NotFound("No Students Found!");

            var averageGrades = StudentRepository.StudentList.Average(student => student.Grade);
            return Ok(averageGrades);
        }

        [HttpGet("{ID}", Name = "GetStudentByID")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<Student> GetStudentByID(int ID)
        {
            if (ID < 1)
                return BadRequest($"Not Accepted ID {ID}");

            var student = StudentRepository.StudentList.FirstOrDefault(student => student.ID == ID);

            if (student == null)
                return NotFound($"Student with ID {ID} not found!");

            return Ok(student);
        }
    }
}
