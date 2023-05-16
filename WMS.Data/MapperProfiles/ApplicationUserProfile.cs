using AutoMapper;
using WMS.Data.DTO.IdentityDtos;
using WMS.Data.Entity.Identity;

namespace WMS.Data.MapperProfiles;

public class ApplicationUserProfile : Profile
{
    public ApplicationUserProfile()
    {
        CreateMap<ApplicationUserDto, ApplicationUser>().ReverseMap();
    }
}