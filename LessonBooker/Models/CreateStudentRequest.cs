using LessonBooker.Enums;

namespace LessonBooker.Models;

public class CreateStudentRequest
{
    public string Name { get; set; }
    public PlanTypeEnum PlanType { get; set; }
}
