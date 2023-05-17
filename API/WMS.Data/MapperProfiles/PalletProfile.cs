using AutoMapper;
using WMS.Data.DTO.StockDtos;
using WMS.Data.Entity.Stocks;

namespace WMS.Data.MapperProfiles;

public class PalletProfile : Profile
{
    public PalletProfile()
    {
        CreateMap<PalletDto, Pallet>().ReverseMap();
    }
}