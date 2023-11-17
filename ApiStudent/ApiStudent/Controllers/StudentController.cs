using Microsoft.AspNetCore.Mvc;
using ApiStudent.Models;
using ApiStudent.Helpers;
using ApiStudent.Mensajes;
using ApiStudent.Services;
using Microsoft.AspNetCore.Cors;

namespace ApiStudent.Controllers
{
    [EnableCors("CorsApi")]
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private IUserService _userService;
        private Constantes constantes = new Constantes();

        public StudentController(IUserService userService)

        {
            _userService = userService;
        }

        [HttpGet("GetAllStudents")]
        public async Task<IActionResult> GetAllStudents()
        {
            var student = await _userService.GetAllStudents();
            return Ok(student);
        }

        [HttpPost("GetStudentId")]
        public async Task<IActionResult> GetStudents(Student student)
        {
            if (student.Id == 0)
            {
                return NotFound();
            }
            else
            {
                var studentId = await _userService.GetStudent(student.Id);
                return Ok(studentId);
            }
        }


        [HttpPut("UpdateStudentId")]
        public async Task<IActionResult> UpdateStudent(int id, Student students)
        {
            if (id != students.Id)
            {
                return BadRequest(new AppException(constantes.estudianteNoexiste));
            }
            return Ok(await _userService.UpdateStudent(id, students));

        }

        [HttpPost("CreateStudent")]
        public async Task<IActionResult> createStudent(Student students)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            else
            {
                return Ok(await _userService.createStudent(students));
            }
        }

        [HttpPost("DeleteStudentId")]
        public async Task<IActionResult> DeleteStudents(Student students)
        {
            if (students.Id == 0)
            {
                return NotFound(new AppException(constantes.estudianteNoexiste));
            }
            return Ok(await _userService.DeleteStudent(students.Id));
        }
    }
}
