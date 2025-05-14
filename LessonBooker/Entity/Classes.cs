using LessonBooker.Enums;

namespace LessonBooker.Entity;

public class Classes
{
    public int ClassesId { get; set; }
    public string Name { get; set; }
    public DateTime ClassDate { get; set; }
    public int MaxParticipants { get; set; }
    public ClassType ClassType { get; set; }
    public List<Students> Students { get; set; }
}
