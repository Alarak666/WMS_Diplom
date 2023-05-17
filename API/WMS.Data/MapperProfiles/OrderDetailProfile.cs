using AutoMapper;
using WMS.Data.DTO.OrderDtos;
using WMS.Data.Entity.Orders;

namespace WMS.Data.MapperProfiles;

public class OrderDetailProfile : Profile
{
    public OrderDetailProfile()
    {
        CreateMap<OrderDetailDto, OrderDetail>().ReverseMap();
    }
}