using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Students.API.Models.Domain;
using Students.API.Models.DTO;

namespace Students.API.Mapping
{
    public class AutoMapperProfile: Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Housing, HousingDto>().ReverseMap();
            CreateMap<AddHousingRequestDto, Housing>().ReverseMap();
            CreateMap<UpdateHousingRequestDto, Housing>().ReverseMap();
            CreateMap<AddMajorRequestDto, Major>().ReverseMap();
            CreateMap<Major, MajorDto>().ReverseMap();
            CreateMap<AddStudentRequestDto, Student>().ReverseMap();
            CreateMap<Student, StudentDto>().ReverseMap();
            CreateMap<UpdateStudentRequestDto, Student>().ReverseMap();
            
            
        }
    }
}
