using LessonBooker.Entities;
using LessonBooker.Models;
using LessonBooker.Persistence;
using LessonBooker.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LessonBooker.Controllers;

public class StudentsController : ControllerBase
{
    private readonly IStudentService _studentService;

    public StudentsController(IStudentService studentService) 
    {
        _studentService = studentService;
    }

    [HttpGet("GetAllStudents")]
    public IActionResult GetAllStudents()
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
            return BadRequest($"Erro interno ao buscar alunos. Error: {ex.Message}");
        }
    }

    [HttpPost("CreateStudent")]
    public IActionResult CreateStudent([FromBody] CreateStudentRequest request)
    {
        try
        {
            var result = _studentService.CreateStudent(request);
            return Ok(result);
        }
        catch (ArgumentException ex)
        {
            return NotFound(ex.Message);
        }
        catch (Exception ex)
        {
            return BadRequest($"Erro interno ao cadastrar alunos. Error: {ex.Message}");
        }
    }

    [HttpPost("Booking")]
    public IActionResult Booking(int idClass, int idStudent)
    {
        try
        {
            _studentService.BookStudentInClass(idClass, idStudent);
            return Ok("Aluno inscrito com sucesso na aula.");
        }
        catch (ArgumentException ex)
        {
            return NotFound(ex.Message);
        }
        catch (Exception ex)
        {
            return BadRequest($"Erro interno ao inserir aluno na aula. Error: {ex.Message}");
        }
    }
}
