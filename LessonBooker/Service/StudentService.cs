using LessonBooker.Entities;
using LessonBooker.Enums;
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

    public void BookStudentInClass(BookingRequest request)
    {
        var classEntity = _dbContext.Classes
            .Include(x => x.Students)
            .FirstOrDefault(x => x.ClassId == request.ClassId);

        if (classEntity == null)
            throw new ArgumentException("Aula não encontrada!");

        var student = _dbContext.Students
            .Include(x => x.Classes)
            .FirstOrDefault(x => x.StudentId == request.StudentId);

        if (student == null)
            throw new ArgumentException("Aluno não encontrado!");

        classEntity.AddStudent(student);
        student.AddClass(classEntity);

        _dbContext.SaveChanges();
    }

    public StudentResponse CreateStudent(CreateStudentRequest request)
    {
        if (!Enum.IsDefined(typeof(PlanTypeEnum), request.PlanType))
            throw new ArgumentException($"O Tipo do plano {request.PlanType} esta inválido");

        var student = new Students(request.Name, request.PlanType.Value);

        _dbContext.Students.Add(student);
        _dbContext.SaveChanges();

        return new StudentResponse
        {
            StudentId = student.StudentId,
            Name = request.Name,
            PlanType = request.PlanType.Value
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
            throw new ArgumentException("Nenhum estudante cadastrado!");

        return students;
    }

    public ReportStudentResponse GetStudentClassReport(int idStudent)
    {
        var student = _dbContext.Students
        .Include(x => x.Classes)
        .FirstOrDefault(i => i.StudentId == idStudent);

        if (student == null)
            throw new ArgumentException("Aluno não encontrado!");

        var currentMonth = DateTime.Now;

        var currentMonthClasses = student.Classes
            .Where(x => x.ClassDate.Month == currentMonth.Month && x.ClassDate.Year == currentMonth.Year)
            .ToList();

        var classTypeCounts = currentMonthClasses
            .GroupBy(x => x.ClassType)
            .Select(i => new ClassTypeReport
            {
                ClassType = i.Key,
                Count = i.Count()
            })
            .OrderByDescending(x => x.Count)
            .ToList();

        return new ReportStudentResponse
        {
            StudentName = student.Name,
            TotalClasses = currentMonthClasses.Count,
            MostFrequent = classTypeCounts
        };
    }
}
