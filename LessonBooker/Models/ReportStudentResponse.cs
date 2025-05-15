using LessonBooker.Enums;

namespace LessonBooker.Models;

public class ReportStudentResponse
{
    public string StudentName { get; set; }
    public int TotalClasses { get; set; }
    public List<ClassTypeReport> MostFrequent { get; set; }
}

public class ClassTypeReport
{
    public ClassType ClassType { get; set; }
    public int Count { get; set; }
}
