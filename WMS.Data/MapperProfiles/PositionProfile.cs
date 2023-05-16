using AutoMapper;
using WMS.Data.DTO.PositionDtos;
using WMS.Data.Entity.Positions;

namespace WMS.Data.MapperProfiles;

public class PositionProfile : Profile
{
    public PositionProfile()
    {
        CreateMap<PositionDto, Position>().ReverseMap();
    }
}