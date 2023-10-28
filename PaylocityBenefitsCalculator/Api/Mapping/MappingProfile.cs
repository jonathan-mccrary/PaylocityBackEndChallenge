using Api.Dtos.Employee;
using Api.Models;
using AutoMapper;

namespace Api.Mapping
{
    public class MappingProfile : Profile
	{
		public MappingProfile()
		{
			CreateMap<Employee, GetEmployeeDto>();
			CreateMap<Dependent, GetEmployeeDto>();
		}
	}
}