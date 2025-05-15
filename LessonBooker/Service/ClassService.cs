using LessonBooker.Entities;
using LessonBooker.Models;
using LessonBooker.Persistence;

namespace LessonBooker.Service
{
    public class ClassService : IClassService
    {
        private readonly LessonBookerDbContext _dbContext;

        public ClassService(LessonBookerDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public ClassResponse CreateStudent(CreateClassRequest request)
        {
            var classEntity = new Classes(request.Name, request.ClassDate, request.MaxParticipants, request.ClassType);

            _dbContext.Classes.Add(classEntity);
            _dbContext.SaveChanges();

            return new ClassResponse
            {
                ClassId = classEntity.ClassId,
                Name = classEntity.Name,
                ClassDate = classEntity.ClassDate,
                ClassType = classEntity.ClassType
            };
        }
    }
}
