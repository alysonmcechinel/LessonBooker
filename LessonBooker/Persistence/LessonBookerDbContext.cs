using LessonBooker.Entities;
using Microsoft.EntityFrameworkCore;

namespace LessonBooker.Persistence;

public class LessonBookerDbContext : DbContext 
{
    public LessonBookerDbContext(DbContextOptions<LessonBookerDbContext> options) : base(options)
    {
        
    }

    public DbSet<Classes> Classes { get; set; }
    public DbSet<Students> Students { get; set; }
}
