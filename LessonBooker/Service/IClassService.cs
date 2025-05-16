using LessonBooker.Models;

namespace LessonBooker.Service;

public interface IClassService
{
    ClassResponse CreateClass(CreateClassRequest request);
}
