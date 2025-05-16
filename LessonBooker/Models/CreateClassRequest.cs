using LessonBooker.Enums;
using System.ComponentModel.DataAnnotations;

namespace LessonBooker.Models
{
    public class CreateClassRequest
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public DateTime? ClassDate { get; set; }
        [Required]
        public int? MaxParticipants { get; set; }
        [Required]
        public ClassTypeEnum? ClassType { get; set; }
    }
}
