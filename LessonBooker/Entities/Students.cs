using LessonBooker.Enums;
using System.ComponentModel.DataAnnotations;

namespace LessonBooker.Entities;

public class Students
{
    public Students(string name, PlanTypeEnum planType)
    {
        Name = name;
        PlanType = planType;

        Classes = new List<Classes>();
    }

    [Key]
    public int StudentId { get; private set; }
    public string Name { get; private set; }
    public PlanTypeEnum PlanType { get; private set; }
    public List<Classes> Classes { get; private set; }

    public bool MaxClasses()
    {
        int maxClasses = PlanType switch
        {
            PlanTypeEnum.Monthly => 12,
            PlanTypeEnum.Quarterly => 20,
            _ => 30
        };

        return Classes.Count >= maxClasses;
    }

    public void AddClass(Classes classes)
    {
        if (MaxClasses())
            throw new InvalidOperationException("Seu plano atingiu o max de aulas no mês");

        Classes.Add(classes);
    }
}
