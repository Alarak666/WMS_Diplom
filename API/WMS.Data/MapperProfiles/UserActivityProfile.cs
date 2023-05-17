using AutoMapper;
using WMS.Data.DTO.IdentityDtos;
using WMS.Data.Entity.Identity;

namespace WMS.Data.MapperProfiles;

public class UserActivityProfile : Profile
{
    public UserActivityProfile()
    {
        CreateMap<UserActivityDto, UserActivity>().ReverseMap();
    }
}