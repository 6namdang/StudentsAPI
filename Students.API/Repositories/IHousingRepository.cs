using Students.API.Models.Domain;

namespace Students.API.Repositories
{
    public interface IHousingRepository
    {
        Task<List<Housing>> GetAllAsync();
        Task<Housing> CreateAsync(Housing housing);

       Task<Housing?> UpdateAsync(Guid id, Housing housing);

        Task<Housing?> DeleteAsync(Guid id);

        Task<Housing?> GetByIdAsync(Guid id);
    }
}
