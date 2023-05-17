using AutoMapper;
using WMS.Data.DTO.IdentityDtos;
using WMS.Data.Entity.Identity;

namespace WMS.Data.MapperProfiles;

public class ApplicationUserSettingProfile : Profile
{
    public ApplicationUserSettingProfile()
    {
        CreateMap<ApplicationUserSettingDto, ApplicationUserSetting>().ReverseMap();
    }
}