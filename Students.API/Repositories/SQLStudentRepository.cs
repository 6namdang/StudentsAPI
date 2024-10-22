using Microsoft.EntityFrameworkCore;
using Students.API.Data;
using Students.API.Models.Domain;

namespace Students.API.Repositories
{
    public class SQLStudentRepository : IStudentRepository
    {
        private readonly StudentsDbContext dbContext;

        public SQLStudentRepository(StudentsDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<Student> CreateAsync(Student student)
        {
            await dbContext.AddAsync(student);
            await dbContext.SaveChangesAsync();
            return student;
        }

        public async Task<Student?> DeleteAsync(Guid id)
        {
            var existStudent = await dbContext.Students.FirstOrDefaultAsync(x => x.Id == id);
            if (existStudent == null)
            {
                return null;
            
            }
            dbContext.Students.Remove(existStudent);
            await dbContext.SaveChangesAsync();
            return existStudent;
        }

        public async Task<List<Student>> GetAllAsync(string? filterOn = null, string? filterQuery = null, int pageNumber = 1, int pageSize = 1000)
        {
            var students = dbContext.Students.Include("Major").Include("Housing").AsQueryable();
            if (string.IsNullOrEmpty(filterOn) == false && string.IsNullOrWhiteSpace(filterQuery) == false)
            {
                if (filterOn.Equals("FullName", StringComparison.CurrentCultureIgnoreCase) == false)
                {
                    students = students.Where(x => x.FullName.Contains(filterQuery));
                }
                //pagination: return custom results.
                

            }
            var skipResult = (pageNumber - 1) * pageSize;
            return await students.Skip(skipResult).Take(pageSize).ToListAsync();
        }

        public async Task<Student> UpdateAsync(Guid id, Student student)
        {
            var existStudent = await dbContext.Students.FirstOrDefaultAsync(x => x.Id == id);
            if ( existStudent == null)
            {
                return null;
            }
            existStudent.FullName = student.FullName;
            existStudent.Age = student.Age;
            existStudent.PhoneNumber = student.PhoneNumber;
            existStudent.Email = student.Email;
            existStudent.HousingId = student.HousingId;
            existStudent.MajorId= student.MajorId;

            await dbContext.SaveChangesAsync();
            return existStudent;
        }
    }
}
//authentication blocks people to access people connect to your api
//authorization allows users to get resources
//authentication jwt. server sends token to users.
//users-> website via login/register
//api generates token to send back to users. anytime users want to access resources in database, api will check users token
//and responds back with a result ( based on the users requirements)