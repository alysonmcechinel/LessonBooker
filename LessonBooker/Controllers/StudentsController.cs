using LessonBooker.Entity;
using Microsoft.AspNetCore.Mvc;

namespace LessonBooker.Controllers;

public class StudentsController : ControllerBase
{
    public StudentsController() { }

    [Host]
    public IActionResult CreateStudent(Students students)
    {
        var student = new Students(students.Name, students.PlanType);

        return Ok(student);
    }
}
