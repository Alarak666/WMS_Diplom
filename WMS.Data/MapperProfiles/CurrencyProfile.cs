using AutoMapper;
using WMS.Data.DTO.CurrencyDtos;
using WMS.Data.Entity.Currencies;

namespace WMS.Data.MapperProfiles;

public class CurrencyProfile : Profile
{
    public CurrencyProfile()
    {
        CreateMap<CurrencyDto, Currency>().ReverseMap();
    }
}