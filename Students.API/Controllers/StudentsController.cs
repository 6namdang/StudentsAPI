using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Students.API.Data;
using Students.API.Models.Domain;
using Students.API.Models.DTO;
using Students.API.Repositories;
using System.Net;

namespace Students.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly StudentsDbContext dbContext;
        private readonly IStudentRepository studentRepository;
        private readonly IMapper mapper;

        public StudentsController(StudentsDbContext dbContext, IStudentRepository studentRepository, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.studentRepository = studentRepository;
            this.mapper = mapper;
        }

        [HttpPost]
        //[Authorize(Roles = "Writer")]
        public async Task<IActionResult> Create([FromBody] AddStudentRequestDto addStudentRequestDto)
        {
            var studentsDomainModel = mapper.Map<Student>(addStudentRequestDto);

            studentsDomainModel = await studentRepository.CreateAsync(studentsDomainModel);

            var studentsDto = mapper.Map<StudentDto>(studentsDomainModel);

            return Ok(studentsDto);

        }
        [HttpGet]
        //[Authorize(Roles = "Reader")]

        public async Task<IActionResult> GetAll([FromQuery] string? filterOn, [FromQuery] string? filterQuery, 
            [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 1000 )
        {
            try
            {

                var studentsDomainModel = await studentRepository.GetAllAsync(filterOn, filterQuery, pageNumber, pageSize);
                //mapping
                var studentsDto = mapper.Map<List<StudentDto>>(studentsDomainModel);
                return Ok(studentsDto);
            }
            catch (Exception)
            {

                throw new Exception("Error");
            }
            
            

        }

        [HttpPut]
        [Route("{id:guid}")]
        //[Authorize(Roles = "Writer")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateStudentRequestDto updateStudentRequestDto)
        {
            var studentsDomainModel = mapper.Map<Student>(updateStudentRequestDto);
            studentsDomainModel = await studentRepository.UpdateAsync(id, studentsDomainModel);
            if (studentsDomainModel == null)
            {
                return NotFound();
            }

            return Ok(mapper.Map<StudentDto>(studentsDomainModel));
        }
        [HttpDelete]
        [Route("{id:guid}")]
        //[Authorize(Roles = "Writer")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var studentsDomainModel = await studentRepository.DeleteAsync(id);
            if (studentsDomainModel == null)
            {
                return NotFound();
            }
            return Ok(mapper.Map<StudentDto>(studentsDomainModel));
        }
    }
}
