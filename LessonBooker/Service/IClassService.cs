using LessonBooker.Models;

namespace LessonBooker.Service;

public interface IClassService
{
    ClassResponse CreateStudent(CreateClassRequest request);
}
