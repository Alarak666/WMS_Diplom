using AutoMapper;
using WMS.Data.DTO.DivisionDtos;
using WMS.Data.Entity.Divisions;

namespace WMS.Data.MapperProfiles;

public class DivisionProfile : Profile
{
    public DivisionProfile()
    {
        CreateMap<DivisionDto, Division>().ReverseMap();
    }
}