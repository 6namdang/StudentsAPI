using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Students.API.Data;
using Students.API.Models.Domain;
using Students.API.Models.DTO;
using Students.API.Repositories;

namespace Students.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MajorsController : ControllerBase
    {
        private readonly StudentsDbContext dbContext;
        private readonly IMapper mapper;
        private readonly IMajorRepository majorRepository;

        public MajorsController(StudentsDbContext dbContext, IMapper mapper, IMajorRepository majorRepository)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
            this.majorRepository = majorRepository;
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AddMajorRequestDto addMajorRequestDto)
        {
            var majorsDomainModel = mapper.Map<Major>(addMajorRequestDto);
            majorsDomainModel = await majorRepository.CreateAsync(majorsDomainModel);
            var majorsDto = mapper.Map<MajorDto>(majorsDomainModel);
            return Ok(majorsDto);           

        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var majorsDomainModel = await majorRepository.GetAllAsync();
            var majorsDto = mapper.Map<List<MajorDto>>(majorsDomainModel);
            return Ok(majorsDto);

        }
    }
}
