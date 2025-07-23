using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudentAPI.Model;
using StudentAPI.Repository;
using StudentAPIBusinessLayer;
using StudentDataAccessLayer;

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
        public ActionResult<IEnumerable<StudentDTO>> GetAllStudents()
        {
            //if (StudentRepository.StudentList.Count == 0)
            //    return NotFound("No Students Found!");

            //return Ok(StudentRepository.StudentList);

            List<StudentDTO> StudentsList = StudentAPIBusinessLayer.Student.GetAllStudents();

            if (StudentsList.Count == 0 )
                return NotFound("No Students Found!");

            return Ok(StudentsList);
        }

        [HttpGet("Passed", Name = "GetPassedStudents")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<IEnumerable<StudentDTO>> GetPassedStudents()
        {
            //if (StudentRepository.StudentList.Count == 0)
            //    return NotFound("No Students Found!");

            //var passedStudents = StudentRepository.StudentList.Where(student => student.Grade >= 50).ToList();

            //if (passedStudents.Count == 0)
            //    return NotFound("No Passed Students Found!");

            //return Ok(passedStudents);

            List<StudentDTO> PassedStudentsList = StudentAPIBusinessLayer.Student.GetPassedStudents();

            if (PassedStudentsList.Count == 0)
                return NotFound("No Students Found!");

            return Ok(PassedStudentsList);
        }

        [HttpGet("AverageGrades", Name = "GetAverageGrades")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<double> GetAverageGrades()
        {
            //if (StudentRepository.StudentList.Count == 0)
            //    return NotFound("No Students Found!");

            //var averageGrades = StudentRepository.StudentList.Average(student => student.Grade);
            //return Ok(averageGrades);

            double averageGrade = StudentAPIBusinessLayer.Student.GetAverageGrade();
            return Ok(averageGrade);
        }

        [HttpGet("{ID}", Name = "GetStudentByID")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<StudentDTO> GetStudentByID(int ID)
        {
            if (ID < 1)
                return BadRequest($"Not Accepted ID {ID}");

            // var student = StudentRepository.StudentList.FirstOrDefault(student => student.ID == ID);
            StudentAPIBusinessLayer.Student student = StudentAPIBusinessLayer.Student.Find(ID);

            if (student == null)
                return NotFound($"Student with ID {ID} not found!");

            return Ok(student.sDTO);
        }

        [HttpPost("AddNewStudent", Name = "AddNewStudent")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<StudentDTO> AddNewStudent(StudentDTO newStudent)
        {
            if (newStudent == null || string.IsNullOrEmpty(newStudent.Name) || newStudent.Age < 0 || newStudent.Grade < 0)
                return BadRequest("Invalid Student Data!");

            // newStudent.ID = StudentRepository.StudentList.Count > 0 ? StudentRepository.StudentList.Max(student => student.ID) + 1 : 1;
            // StudentRepository.StudentList.Add(newStudent);

            StudentAPIBusinessLayer.Student student = new StudentAPIBusinessLayer.Student(newStudent);
            student.Save();

            newStudent.ID = student.ID;

            return CreatedAtRoute("GetStudentByID", new { ID = newStudent.ID }, newStudent);
        }

        [HttpDelete("{ID}", Name = "DeleteStudent")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult DeleteStudent(int ID)
        {
            if (ID < 1)
                return BadRequest($"Not Accepted ID {ID}");

            // var student = StudentRepository.StudentList.FirstOrDefault(student => student.ID == ID);
            //if (student == null)
            //    return NotFound($"Student with ID {ID} not found!");

            //StudentRepository.StudentList.Remove(student);
            //return Ok($"Student with ID {ID} has been deleted!");

            if (StudentAPIBusinessLayer.Student.DeleteStudent(ID))
                return Ok($"Student with ID {ID} has been deleted!");
            else
                return NotFound($"Student with ID {ID} not found!");
        }

        [HttpPut("{ID}", Name = "UpdateStudent")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<StudentDTO> UpdateStudent(int ID, StudentDTO updatedStudent)
        {
            if (ID < 1 || updatedStudent == null || string.IsNullOrEmpty(updatedStudent.Name) || updatedStudent.Age < 0 || updatedStudent.Grade < 0)
                return BadRequest("Invalid Student Data!");

            // var student = StudentRepository.StudentList.FirstOrDefault(student => student.ID == ID);
            StudentAPIBusinessLayer.Student student = StudentAPIBusinessLayer.Student.Find(ID);

            if (student == null)
                return NotFound($"Student with ID {ID} not found!");

            student.Name = updatedStudent.Name;
            student.Age = updatedStudent.Age;
            student.Grade = updatedStudent.Grade;

            student.Save();

            return Ok(student.sDTO);
        }
    }
}
