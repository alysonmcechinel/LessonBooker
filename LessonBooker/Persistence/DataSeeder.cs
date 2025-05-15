using LessonBooker.Entities;
using LessonBooker.Enums;

namespace LessonBooker.Persistence
{
    public static class DataSeeder
    {
        public static void Seed(LessonBookerDbContext dbContext)
        {
            var crossClass = new Classes("Cross", DateTime.Now, 5, ClassType.Cross);
            var pilatesClass = new Classes("Pilates", DateTime.Now, 1, ClassType.Pilates);
            var funcionalClass = new Classes("Funcional", DateTime.Now, 2, ClassType.FunctionalTraining);
            var funcionalDomClass = new Classes("Funcional de Domingo", DateTime.Now, 3, ClassType.FunctionalTraining);

            var studentJoao = new Students("Joao", PlanTypeEnum.Monthly);
            var studentMaria = new Students("Maria", PlanTypeEnum.Monthly);
            var studentJose = new Students("Jose", PlanTypeEnum.Quarterly);
            var studentDouglas = new Students("Douglas", PlanTypeEnum.Quarterly);
            var studentAndre = new Students("Andre", PlanTypeEnum.Annual);
            var studentVitor = new Students("Vitor", PlanTypeEnum.Annual);

            dbContext.Students.AddRange(studentJoao, studentMaria, studentJose, studentDouglas, studentAndre, studentVitor);
            dbContext.Classes.AddRange(crossClass, pilatesClass, funcionalClass, funcionalDomClass);
            dbContext.SaveChanges();

            AddStudentToClass(crossClass, studentJoao);
            AddStudentToClass(crossClass, studentMaria);
            AddStudentToClass(funcionalClass, studentJoao);
            AddStudentToClass(funcionalClass, studentJose);
            AddStudentToClass(funcionalDomClass, studentJoao);

            dbContext.SaveChanges();
        }

        // Privates

        private static void AddStudentToClass(Classes classEntity, Students student)
        {
            classEntity.AddStudent(student);
            student.AddClass(classEntity);
        }
    }
}
