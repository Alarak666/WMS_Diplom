using AutoMapper;
using WMS.Data.DTO.CountryDtos;
using WMS.Data.Entity.Countries;

namespace WMS.Data.MapperProfiles;

public class CountryProfile : Profile
{
    public CountryProfile()
    {
        CreateMap<CountryDto, Country>().ReverseMap();
    }
}