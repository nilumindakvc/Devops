using agent.DTOs;
using agent.entityClasses;
using AutoMapper;

namespace agent.MapperConfig
{
    public class configMapper : Profile
    {
        public configMapper()
        {
            CreateMap<Agency, AgencyInDTO>().ReverseMap();
            CreateMap<Region, RegionDTO>().ReverseMap();
            CreateMap<Country, CountryDTO>().ReverseMap();
            CreateMap<AgencyCountry, AgencyCountryDTO>().ReverseMap();
            CreateMap<Job, JobOutDTO>().ReverseMap();
            CreateMap<JobCategory, CategoryInDTO>().ReverseMap();
            CreateMap<JobCategory, CategoryOutDTO>().ReverseMap();
            CreateMap<Agency, AgencyOutDTO>().ReverseMap();
            CreateMap<Job, tempory>().ReverseMap();
            CreateMap<AgencyReview, ReviewDTO>().ReverseMap();
            CreateMap<User, UserDTO>().ReverseMap();
            CreateMap<User, UserOutDTO>().ReverseMap();
            CreateMap<Job, JobInDTO>().ReverseMap();
        }
    }
}