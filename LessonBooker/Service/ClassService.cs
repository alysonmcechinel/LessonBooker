using LessonBooker.Entities;
using LessonBooker.Enums;
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

        public ClassResponse CreateClass(CreateClassRequest request)
        {
            if (request.MaxParticipants <= 0)
                throw new ArgumentException($"A quantidade de participantes deve ser maior que zero");

            if (!Enum.IsDefined(typeof(ClassTypeEnum), request.ClassType))
                throw new ArgumentException($"O Tipo de aula {request.ClassType} esta inválido");

            var existsName = _dbContext.Classes.Any(x => x.Name.ToLower() == request.Name.ToLower());
            if (existsName)
                throw new ArgumentException($"Já existe uma aula cadastra com esse nome {request.Name}");

            var classEntity = new Classes(request.Name, request.ClassDate.Value, request.MaxParticipants.Value, request.ClassType.Value);

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
