using LessonBooker.Entities;
using LessonBooker.Models;
using LessonBooker.Persistence;
using LessonBooker.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LessonBooker.Controllers;

[ApiController]
public class ClassController : ControllerBase
{
    private readonly IClassService _classService;

    public ClassController(IClassService classService)
    {
        _classService = classService;
    }

    /// <summary>
    /// Método para cadastrar aula
    /// </summary>
    /// <param name="request">CreateClassRequest: input de dados</param>
    [HttpPost("CreateClass")]
    [ProducesResponseType(typeof(ClassResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
    public IActionResult CreateClass([FromBody] CreateClassRequest request)
    {
        try
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = _classService.CreateClass(request);

            return Ok(result);
        }
        catch (ArgumentException ex)
        {
            return NotFound(ex.Message);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Erro interno ao cadastrar Aula. Error: {ex.Message}");
        }
    }
}
