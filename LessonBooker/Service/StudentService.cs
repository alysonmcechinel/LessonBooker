using LessonBooker.Entities;
using LessonBooker.Models;
using LessonBooker.Persistence;
using Microsoft.EntityFrameworkCore;

namespace LessonBooker.Service;

public class StudentService : IStudentService
{
    private readonly LessonBookerDbContext _dbContext;

    public StudentService(LessonBookerDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public void BookStudentInClass(int idClass, int idStudent)
    {
        var classEntity = _dbContext.Classes
            .Include(x => x.Students)
            .FirstOrDefault(x => x.ClassId == idClass);

        if (classEntity == null)
            throw new ArgumentException("Aula não encontrada!");

        var student = _dbContext.Students
            .Include(x => x.Classes)
            .FirstOrDefault(x => x.StudentId == idStudent);

        if (student == null)
            throw new ArgumentException("Aluno não encontrado!");

        classEntity.AddStudent(student);
        student.AddClass(classEntity);

        _dbContext.SaveChanges();
    }

    public StudentResponse CreateStudent(CreateStudentRequest request)
    {
        var student = new Students(request.Name, request.PlanType);

        _dbContext.Students.Add(student);
        _dbContext.SaveChanges();

        return new StudentResponse
        {
            StudentId = student.StudentId,
            Name = request.Name,
            PlanType = request.PlanType
        };
    }

    public List<StudentResponse> GetAllStudents()
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
            throw new ArgumentException("Nenhum estudante cadastrado");

        return students;
    }
}
