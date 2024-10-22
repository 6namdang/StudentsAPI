using Students.API.Models.Domain;

namespace Students.API.Repositories
{
    public interface IStudentRepository
    {
        Task<List<Student>> GetAllAsync(string? filterOn = null, string? filterQuery = null, int pageNumber =1, int pageSize = 1000);

        Task<Student> CreateAsync(Student student);

        Task<Student?> UpdateAsync(Guid id, Student student);
        Task <Student?> DeleteAsync(Guid id);
    }
}
