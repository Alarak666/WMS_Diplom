using AutoMapper;
using WMS.Data.DTO.VendorCustomerDtos;
using WMS.Data.Entity.VendorCustomers;

namespace WMS.Data.MapperProfiles;

public class VendorCustomerProfile : Profile
{
    public VendorCustomerProfile()
    {
        CreateMap<VendorCustomerDto, VendorCustomer>().ReverseMap();
    }
}