using AutoMapper;
using WMS.Data.DTO.EmployeeDtos;
using WMS.Data.Entity.Employees;

namespace WMS.Data.MapperProfiles;

public class EmployeeProfile : Profile
{
    public EmployeeProfile()
    {
        CreateMap<EmployeeDto, Employee>().ReverseMap();
    }
}