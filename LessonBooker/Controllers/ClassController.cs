using LessonBooker.Entities;
using LessonBooker.Models;
using LessonBooker.Persistence;
using LessonBooker.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LessonBooker.Controllers;

public class ClassController : ControllerBase
{
    private readonly IClassService _classService;

    public ClassController(IClassService classService)
    {
        _classService = classService;
    }

    [HttpPost("CreateClass")]
    public IActionResult CreateClass([FromBody] CreateClassRequest request)
    {
        try
        {
            var result = _classService.CreateStudent(request);
            return Ok(result);
        }
        catch (ArgumentException ex)
        {
            return NotFound(ex.Message);
        }
        catch (Exception ex)
        {
            return BadRequest($"Erro interno ao cadastrar Aula. Error: {ex.Message}");
        }
    }
}
