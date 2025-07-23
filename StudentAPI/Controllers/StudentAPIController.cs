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
            return Ok(StudentReopsitory.StudentList);
        }

        [HttpGet("Passed", Name = "GetPassedStudents")]
        public ActionResult<IEnumerable<Student>> GetPassedStudents()
        {
            var passedStudents = StudentReopsitory.StudentList.Where(student => student.Grade >= 50).ToList();
            return Ok(passedStudents);
        }
    }
}
