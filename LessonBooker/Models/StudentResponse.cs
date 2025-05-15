using LessonBooker.Enums;

namespace LessonBooker.Models;

public class StudentResponse
{
    public int StudentId { get; set; }
    public string Name { get; set; }
    public PlanTypeEnum PlanType { get; set; }
    public List<ClassResponse> Classes { get; set; }
}
