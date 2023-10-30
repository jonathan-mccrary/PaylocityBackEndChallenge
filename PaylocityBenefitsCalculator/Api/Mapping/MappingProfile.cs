using Api.Dtos.Dependent;
using Api.Dtos.Employee;
using Api.Models;
using AutoMapper;

namespace Api.Mapping
{
	/// <summary>
	/// Automapper implentation for mapping to and from the DTOs and Models
	/// </summary>
    public class MappingProfile : Profile
	{
		public MappingProfile()
		{
			CreateMap<Employee, EmployeeDto>();
			CreateMap<Dependent, DependentDto>();

            CreateMap<EmployeeDto, Employee>();
            CreateMap<DependentDto, Dependent>();
        }
	}
}