using AutoMapper;
using WMS.Data.DTO.StockDtos;
using WMS.Data.Entity.Stocks;

namespace WMS.Data.MapperProfiles;

public class AreaTypeProfile : Profile
{
    public AreaTypeProfile()
    {
        CreateMap<AreaTypeDto, AreaType>().ReverseMap();
    }
}