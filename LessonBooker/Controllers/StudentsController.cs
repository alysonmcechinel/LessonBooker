using LessonBooker.Entities;
using LessonBooker.Models;
using LessonBooker.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LessonBooker.Controllers;

public class StudentsController : ControllerBase
{
    private readonly LessonBookerDbContext _dbContext;

    public StudentsController(LessonBookerDbContext dbContext) 
    {
        _dbContext = dbContext;
    }

    [HttpGet("GetAllStudents")]
    public IActionResult GetAllStudents()
    {
        var students = _dbContext.Students
        .Include(x => x.Classes)
        .Select(s => new StudentResponse
        {
            StudentId = s.StudentId,
            Name = s.Name,
            PlanType = s.PlanType,
            Classes = s.Classes.Select(c => new ClassResponse
            {
                ClassId = c.ClassId,
                Name = c.Name,
                ClassDate = c.ClassDate,
                ClassType = c.ClassType
            }).ToList()
        }).ToList();

        if (!students.Any())
            return BadRequest("Nenhum estudante cadastrado");

        return Ok(students);
    }

    [HttpPost("CreateStudent")]
    public IActionResult CreateStudent([FromBody] CreateStudentRequest request)
    {
        var student = new Students(request.Name, request.PlanType);

        _dbContext.Students.Add(student);
        _dbContext.SaveChanges();

        return Ok(student);
    }

    [HttpPost("Booking")]
    public IActionResult Booking(int idClass, int idStudent)
    {
        var classEntity = _dbContext.Classes
            .Include(x => x.Students)
            .FirstOrDefault(x => x.ClassId == idClass);

        if (classEntity == null)
            return BadRequest("Aula não encontrada!");

        var student = _dbContext.Students
            .Include(x => x.Classes)
            .FirstOrDefault(x => x.StudentId == idStudent);

        if (student == null)
            return BadRequest("Aluno não encontrado");

        if (classEntity.IsClassFull())
            return BadRequest("Aula Atingiu numero max de alunos");

        if (student.MaxClasses())
            return BadRequest("Seu plano atingiu o max de aulas no mês");

        classEntity.AddStudent(student);
        student.AddClass(classEntity);

        _dbContext.SaveChanges();

        return Ok("Cadastrado com sucesso");
    }
}
