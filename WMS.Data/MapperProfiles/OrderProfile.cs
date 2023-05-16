using AutoMapper;
using WMS.Data.DTO.OrderDtos;
using WMS.Data.Entity.Orders;

namespace WMS.Data.MapperProfiles;

public class OrderProfile : Profile
{
    public OrderProfile()
    {
        CreateMap<OrderDto, Order>().ReverseMap();
    }
}