using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetAllStudents() 
        { 
            string[] studentsNames = new string[]
            {
                "John Doe",
                "Jane Smith",
                "Alice Johnson"
            };

            return Ok(studentsNames); // Returns a 200 OK response with the list of students

        }
    }
}
