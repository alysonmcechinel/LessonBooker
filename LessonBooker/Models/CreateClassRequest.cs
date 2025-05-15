using LessonBooker.Enums;

namespace LessonBooker.Models
{
    public class CreateClassRequest
    {
        public string Name { get; set; }
        public DateTime ClassDate { get; set; }
        public int MaxParticipants { get; set; }
        public ClassType ClassType { get; set; }
    }
}
