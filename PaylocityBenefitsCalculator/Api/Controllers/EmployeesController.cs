using Api.DataAccess;
using Api.Dtos.Dependent;
using Api.Dtos.Employee;
using Api.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Api.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class EmployeesController : ControllerBase
{
    private readonly IBenefitsRepository _benefitsRepository;
    private readonly IMapper _mapper;

    public EmployeesController(IBenefitsRepository benefitsRepository,
        IMapper mapper)
    {
        _benefitsRepository = benefitsRepository;
        _mapper = mapper;
    }

    [SwaggerOperation(Summary = "Get employee by id")]
    [HttpGet("{id}")]
    public async Task<ActionResult<ApiResponse<GetEmployeeDto>>> Get(int id)
    {
        try
        {
            var employee = await _benefitsRepository.GetEmployeeByIdAsync(id);

            var result = new ApiResponse<GetEmployeeDto>
            {
                Data = _mapper.Map<GetEmployeeDto>(employee),
                Success = true
            };

            return result;
        }
        catch (Exception ex)
        {
            var result = new ApiResponse<GetEmployeeDto>
            {
                Error = ex.Message,
                Message = $"Error occurred. Message: {ex.Message}",
                Data = null,
                Success = false
            };

            return result;
        }
        
    }

    [SwaggerOperation(Summary = "Get all employees")]
    [HttpGet("")]
    public async Task<ActionResult<ApiResponse<List<GetEmployeeDto>>>> GetAll()
    {
        //task: use a more realistic production approach
        var employees2 = new List<GetEmployeeDto>
        {
            new()
            {
                Id = 1,
                FirstName = "LeBron",
                LastName = "James",
                Salary = 75420.99m,
                DateOfBirth = new DateTime(1984, 12, 30)
            },
            new()
            {
                Id = 2,
                FirstName = "Ja",
                LastName = "Morant",
                Salary = 92365.22m,
                DateOfBirth = new DateTime(1999, 8, 10),
                Dependents = new List<GetDependentDto>
                {
                    new()
                    {
                        Id = 1,
                        FirstName = "Spouse",
                        LastName = "Morant",
                        Relationship = Relationship.Spouse,
                        DateOfBirth = new DateTime(1998, 3, 3)
                    },
                    new()
                    {
                        Id = 2,
                        FirstName = "Child1",
                        LastName = "Morant",
                        Relationship = Relationship.Child,
                        DateOfBirth = new DateTime(2020, 6, 23)
                    },
                    new()
                    {
                        Id = 3,
                        FirstName = "Child2",
                        LastName = "Morant",
                        Relationship = Relationship.Child,
                        DateOfBirth = new DateTime(2021, 5, 18)
                    }
                }
            },
            new()
            {
                Id = 3,
                FirstName = "Michael",
                LastName = "Jordan",
                Salary = 143211.12m,
                DateOfBirth = new DateTime(1963, 2, 17),
                Dependents = new List<GetDependentDto>
                {
                    new()
                    {
                        Id = 4,
                        FirstName = "DP",
                        LastName = "Jordan",
                        Relationship = Relationship.DomesticPartner,
                        DateOfBirth = new DateTime(1974, 1, 2)
                    }
                }
            }
        };

        try
        {

            var employees = await _benefitsRepository.GetEmployeesAsync();
            var result = new ApiResponse<List<GetEmployeeDto>>
            {
                Data = _mapper.Map<List<GetEmployeeDto>>(employees),
                Success = true
            };

            return result;
        }
        catch (Exception ex)
        {
            var result = new ApiResponse<List<GetEmployeeDto>>
            {
                Error = ex.Message,
                Message = $"Error occurred. Message: {ex.Message}",
                Data = null,
                Success = false
            };

            return result;
        }
    }
}
