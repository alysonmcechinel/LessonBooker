using LessonBooker.Models;

namespace LessonBooker.Service;

public interface IStudentService
{
    void BookStudentInClass(BookingRequest request);
    StudentResponse CreateStudent(CreateStudentRequest request);
    List<StudentResponse> GetAllStudents();
    ReportStudentResponse GetStudentClassReport(int idStudent);
}
