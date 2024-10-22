using Microsoft.EntityFrameworkCore;
using Students.API.Data;
using Students.API.Models.Domain;

namespace Students.API.Repositories
{
    public class SQLHousingRepository : IHousingRepository
    {
        private readonly StudentsDbContext dbContext;

        public SQLHousingRepository(StudentsDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<List<Housing>> GetAllAsync()
        {
            return await dbContext.Housings.ToListAsync();
        }

        public async Task<Housing> CreateAsync(Housing housing)
        {
            await dbContext.Housings.AddAsync(housing);
            await dbContext.SaveChangesAsync();
            return housing;
        }

        public async Task<Housing?> UpdateAsync(Guid id, Housing housing)
        {
            //initialized existHousing

            var existHousing = await dbContext.Housings.FirstOrDefaultAsync(x => x.Id == id);
            //check if house exist
            if (existHousing == null)
            {
                return null;
            }
            existHousing.Location = housing.Location;
            existHousing.Name = housing.Name;
            existHousing.Price = housing.Price;

            await dbContext.SaveChangesAsync();
            return housing;
        }

        public async Task<Housing?> DeleteAsync(Guid id)
        {
            var existHousing = await dbContext.Housings.FirstOrDefaultAsync(x => x.Id == id);
            if (existHousing == null)
            {
                return null;
            }
            dbContext.Housings.Remove(existHousing);
            await dbContext.SaveChangesAsync();
            return(existHousing);
        }


        public async Task<Housing?> GetByIdAsync(Guid id)
        {
            return await dbContext.Housings.FirstOrDefaultAsync(x => x.Id ==id);
        }
    }
}
