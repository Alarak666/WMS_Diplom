using AutoMapper;
using WMS.Data.DTO.PersonDtos;
using WMS.Data.Entity.Persons;

namespace WMS.Data.MapperProfiles;

public class PersonProfile : Profile
{
    public PersonProfile()
    {
        CreateMap<PersonDto, Person>().ReverseMap();
    }
}