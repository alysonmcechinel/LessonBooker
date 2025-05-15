using System.ComponentModel.DataAnnotations;

namespace LessonBooker.Models;

public class BookingRequest
{
    [Required]
    public int ClassId { get; set; }
    [Required]
    public int StudentId { get; set; }
}
