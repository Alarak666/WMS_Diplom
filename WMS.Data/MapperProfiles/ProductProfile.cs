using AutoMapper;
using WMS.Data.DTO.ProductDtos;
using WMS.Data.Entity.Products;

namespace WMS.Data.MapperProfiles;

public class ProductProfile : Profile
{
    public ProductProfile()
    {
        CreateMap<ProductDto, Product>().ReverseMap();
    }
}