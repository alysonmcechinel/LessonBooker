using LessonBooker.Entities;
using LessonBooker.Models;
using LessonBooker.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LessonBooker.Controllers;

public class ClassController : ControllerBase
{
    private readonly LessonBookerDbContext _dbContext;

    public ClassController(LessonBookerDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    [HttpGet("GetAllClass")]
    public IActionResult GetAllClass()
    {
        var classes = _dbContext.Classes.Include(x => x.Students).ToList();

        if (!classes.Any())
            return BadRequest("Nenhuma aula cadastrada");

        return Ok(classes);
    }

    [HttpPost("CreateClass")]
    public IActionResult CreateClass([FromBody] CreateClassRequest request)
    {
        var classEntity = new Classes(request.Name, request.ClassDate, request.MaxParticipants, request.ClassType);

        _dbContext.Classes.Add(classEntity);
        _dbContext.SaveChanges();

        return Ok(classEntity);
    }
}
