using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Students.API.Data;
using Students.API.Models.Domain;
using Students.API.Models.DTO;
using Students.API.Repositories;

namespace Students.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    
    public class HousingsController : ControllerBase
    {
        private readonly ILogger<HousingsController> logger;
        private readonly StudentsDbContext dbContext;
        private readonly IHousingRepository housingRepository;
        private readonly IMapper mapper;

        public HousingsController(ILogger<HousingsController> logger,StudentsDbContext dbContext, IHousingRepository housingRepository, IMapper mapper)
        {
            this.logger = logger;
            this.dbContext = dbContext;
            this.housingRepository = housingRepository;
            this.mapper = mapper;
        }
        //GET ALL METHOD

        [HttpGet]
        //[Authorize(Roles = "Reader")]
        public async Task<IActionResult> GetAll()
        {
            logger.LogInformation("Getall Housings method is created");
            
            var housingsDomain = await housingRepository.GetAllAsync();
            var housingDto = mapper.Map<List<HousingDto>>(housingsDomain);

            return Ok(housingDto);
        }

        [HttpPost]
        //[Authorize(Roles = "Writer")]
        public async Task<IActionResult> Create([FromBody] AddHousingRequestDto addHousingRequestDto)
        {
            if (ModelState.IsValid)
            {
                var housingsDomainModel = mapper.Map<Housing>(addHousingRequestDto);

                housingsDomainModel = await housingRepository.CreateAsync(housingsDomainModel);

                var housingDto = mapper.Map<HousingDto>(housingsDomainModel);
                //return CreatedAtAction(nameof(GetById), new {id = housingDto.Id}, housingDto);
                return Ok(housingDto);
            }
            return BadRequest(ModelState);
        }

        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var housingDomainModel = await housingRepository.GetByIdAsync(id);

            if (housingDomainModel == null)
            {
                return NotFound();
            }
            return Ok(mapper.Map<HousingDto>(housingDomainModel));

        }

        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateHousingRequestDto updateHousingRequestDto)
        {
            //map dto to domain model
            var housingDomainModel = mapper.Map<Housing>(updateHousingRequestDto);
            housingDomainModel = await housingRepository.UpdateAsync(id, housingDomainModel);

            if (housingDomainModel == null) 
            {
                return NotFound();
            }
            
        var housingDto = mapper.Map<HousingDto>(housingDomainModel);
            return Ok(housingDto);
}
        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var housingDomainModel = await housingRepository.DeleteAsync(id);

            if (housingDomainModel == null)
            {
                return NotFound();
            }

            var housingDto = mapper.Map<HousingDto>(housingDomainModel);
            return Ok(housingDto);
        }
    }
    
}