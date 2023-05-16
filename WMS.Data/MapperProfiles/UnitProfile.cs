using AutoMapper;
using WMS.Data.DTO.UnitDtos;
using WMS.Data.Entity.Units;

namespace WMS.Data.MapperProfiles;

public class UnitProfile : Profile
{
    public UnitProfile()
    {
        CreateMap<UnitDto, Unit>().ReverseMap();
    }
}