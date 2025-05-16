using LessonBooker.Entities;
using LessonBooker.Models;
using LessonBooker.Persistence;
using LessonBooker.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace LessonBooker.Controllers;

[ApiController]
public class StudentsController : ControllerBase
{
    private readonly IStudentService _studentService;

    public StudentsController(IStudentService studentService) 
    {
        _studentService = studentService;
    }

    /// <summary>
    /// Método para consultar todos os alunos cadastrados
    /// </summary>
    [HttpGet("GetAllStudents")]
    [ProducesResponseType(typeof(StudentResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
    public ActionResult<List<StudentResponse>> GetAllStudents()
    {
        try
        {
            var result = _studentService.GetAllStudents();
            return Ok(result);
        }
        catch (ArgumentException ex)
        {
            return NotFound(ex.Message);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Erro interno ao buscar alunos. Error: {ex.Message}");
        }
    }

    /// <summary>
    /// Método para cadastrar aluno
    /// </summary>
    /// <param name="request">CreateStudentRequest: input de dados</param>
    [HttpPost("CreateStudent")]
    [ProducesResponseType(typeof(StudentResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
    public IActionResult CreateStudent([FromBody] CreateStudentRequest request)
    {
        try
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = _studentService.CreateStudent(request);

            return Ok(result);
        }
        catch (ArgumentException ex)
        {
            return NotFound(ex.Message);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Erro interno ao cadastrar alunos. Error: {ex.Message}");
        }
    }

    /// <summary>
    /// Método para agendamento de aluno em uma aula
    /// </summary>
    /// <param name="request">BookingRequest: input de dados</param>
    [HttpPost("Booking")]
    [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
    public IActionResult Booking([FromBody] BookingRequest request)
    {
        try
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _studentService.BookStudentInClass(request);

            return Ok("Aluno inscrito com sucesso na aula.");
        }
        catch (ArgumentException ex)
        {
            return NotFound(ex.Message);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Erro interno ao inserir aluno na aula. Error: {ex.Message}");
        }
    }

    /// <summary>
    /// Método para consultar relatório por aluno
    /// </summary>
    /// <param name="request">idStudent: input de dados</param>
    [HttpGet("{idStudent}/report")]
    [ProducesResponseType(typeof(ReportStudentResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
    public ActionResult<ReportStudentResponse> GetStudentClassReport(int idStudent)
    {
        try
        {
            if (idStudent < 0)
                return BadRequest("ID aluno inválido");

            var result = _studentService.GetStudentClassReport(idStudent);
            return Ok(result);
        }
        catch (ArgumentException ex)
        {
            return NotFound(ex.Message);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Erro ao gerar relatorio. Error: {ex.Message}");
        }
    }
}
