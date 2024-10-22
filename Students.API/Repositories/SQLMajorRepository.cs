using Microsoft.EntityFrameworkCore;
using Students.API.Data;
using Students.API.Models.Domain;

namespace Students.API.Repositories
{
    public class SQLMajorRepository : IMajorRepository
    {
        private readonly StudentsDbContext dbContext;

        public SQLMajorRepository(StudentsDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<Major> CreateAsync(Major major)
        {
            await dbContext.Majors.AddAsync(major);
            await dbContext.SaveChangesAsync();
            return major;
        }

        public async Task<List<Major>> GetAllAsync()
        {
            return await dbContext.Majors.ToListAsync();
        }
    }
}
