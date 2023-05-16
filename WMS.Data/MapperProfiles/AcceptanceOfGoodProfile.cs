using AutoMapper;
using WMS.Data.DTO.StockDtos;
using WMS.Data.Entity.Stocks;

namespace WMS.Data.MapperProfiles;

public class AcceptanceOfGoodProfile : Profile
{
    public AcceptanceOfGoodProfile()
    {
        CreateMap<AcceptanceOfGoodDto, AcceptanceOfGood>().ReverseMap();
    }
}