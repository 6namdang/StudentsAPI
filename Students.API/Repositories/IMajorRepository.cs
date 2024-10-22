using Students.API.Models.Domain;

namespace Students.API.Repositories
{
    public interface IMajorRepository
    {
        Task<Major> CreateAsync(Major major);
        Task<List<Major>> GetAllAsync();
    }
}
