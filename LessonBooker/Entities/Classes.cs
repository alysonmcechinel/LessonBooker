using LessonBooker.Enums;
using System.ComponentModel.DataAnnotations;

namespace LessonBooker.Entities;

public class Classes
{
    public Classes(string name, DateTime classDate, int maxParticipants, ClassTypeEnum classType)
    {
        Name = name;
        ClassDate = classDate;
        MaxParticipants = maxParticipants;
        ClassType = classType;

        Students = new List<Students>();
    }

    [Key]
    public int ClassId { get; private set; }
    public string Name { get; private set; }
    public DateTime ClassDate { get; private set; }
    public int MaxParticipants { get; private set; }
    public ClassTypeEnum ClassType { get; private set; }
    public List<Students> Students { get; private set; }

    public bool IsClassFull() => Students.Count >= MaxParticipants;

    public bool StudentRegistered(int idStudent) => Students.Any(x => x.StudentId == idStudent);

    public void AddStudent(Students student)
    {
        if (IsClassFull())
            throw new ArgumentException("Aula Atingiu numero max de alunos");

        if (StudentRegistered(student.StudentId))
            throw new ArgumentException("Aluno já cadastrado nessa aula");

        Students.Add(student);
    }
}
