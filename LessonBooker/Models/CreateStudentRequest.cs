using LessonBooker.Enums;
using System.ComponentModel.DataAnnotations;

namespace LessonBooker.Models;

public class CreateStudentRequest
{
    [Required]
    public string Name { get; set; }
    [Required]
    public PlanTypeEnum PlanType { get; set; }
}
