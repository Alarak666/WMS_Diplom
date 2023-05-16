using AutoMapper;
using WMS.Data.DTO.StockDtos;
using WMS.Data.Entity.Stocks;

namespace WMS.Data.MapperProfiles;

public class RegionProfile : Profile
{
    public RegionProfile()
    {
        CreateMap<RegionDto, Region>().ReverseMap();
    }
}