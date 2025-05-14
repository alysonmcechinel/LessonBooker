using LessonBooker.Enums;

namespace LessonBooker.Entity;

public class Students
{
    public Students(string name, PlanType planType)
    {
        Name = name;
        PlanType = planType;
    }

    public int StudentId { get; private set; }
    public string Name { get; private set; }
    public PlanType PlanType { get; private set; }
}
