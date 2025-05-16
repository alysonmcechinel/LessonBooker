using LessonBooker.Enums;

namespace LessonBooker.Models;

public class ClassResponse
{
    public int ClassId { get; set; }
    public string Name { get; set; }
    public DateTime ClassDate { get; set; }
    public ClassTypeEnum ClassType { get; set; }
}
