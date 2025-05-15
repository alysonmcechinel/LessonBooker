using LessonBooker.Models;

namespace LessonBooker.Service;

public interface IStudentService
{
    void BookStudentInClass(int idClass, int idStudent);
    StudentResponse CreateStudent(CreateStudentRequest request);
    List<StudentResponse> GetAllStudents();
}
