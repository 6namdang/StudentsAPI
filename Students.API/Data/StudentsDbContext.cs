using Microsoft.EntityFrameworkCore;
using Students.API.Models.Domain;

namespace Students.API.Data
{
    public class StudentsDbContext: DbContext
    {
        public StudentsDbContext(DbContextOptions<StudentsDbContext> dbContextOptions) : base(dbContextOptions)
        {
            
        }
        public DbSet<Housing> Housings { get; set; }
        public DbSet<Major> Majors { get; set; }
        public DbSet<Student> Students { get; set; }
    }
}
