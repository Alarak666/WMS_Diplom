using AutoMapper;
using WMS.Data.DTO.StockDtos;
using WMS.Data.Entity.Stocks;

namespace WMS.Data.MapperProfiles;

public class PlaceParameterProfile : Profile
{
    public PlaceParameterProfile()
    {
        CreateMap<PlaceParameterDto, PlaceParameter>().ReverseMap();
    }
}