using Api.Dtos.Employee;
using Api.Models;
using Api.Services.Contracts;
using Api.Utilities;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Api.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class EmployeesController : ControllerBase
{
    private readonly IEmployeeService _employeeService;

    public EmployeesController(IEmployeeService employeeService)
    {
        _employeeService = employeeService;
        CreateTestingData.SaveTestEmployees(CreateTestingData.CreateEmployees(), _employeeService);
    }

    [SwaggerOperation(Summary = "Get employee by id")]
    [HttpPost("{id}")]
    public async Task<ActionResult<ApiResponse<int?>>> AddEmployee(EmployeeDto employeeDto)
    {
        try
        {
            var id = await _employeeService.AddEmployeeAsync(employeeDto);
            if (id == null)
            {
                return BadRequest(
                    new ApiResponse<int?>
                    {
                        Data = id,
                        Message = "Unable to add employee",
                        Success = false
                    });
            }
            else if (id == -1)
            {
                return BadRequest(
                    new ApiResponse<int?>
                    {
                        Data = id,
                        Message = "Cannot add employee with more than one partner",
                        Success = false
                    });
            }

            return Ok(
                new ApiResponse<int?>
                {
                    Data = id,
                    Success = true
                });
        }
        catch (Exception ex)
        {
            return BadRequest(
                new ApiResponse<int?>
                {
                    Error = ex.Message,
                    Message = $"Error occurred. Message: {ex.Message}",
                    Data = null,
                    Success = false
                });
        }
    }

    [SwaggerOperation(Summary = "Get employee by id")]
    [HttpGet("{id}")]
    public async Task<ActionResult<ApiResponse<EmployeeDto>>> Get(int id)
    {
        try
        {
            var employee = await _employeeService.GetEmployeeByIdAsync(id);
            if (employee == null)
            {
                return NotFound(
                    new ApiResponse<EmployeeDto>
                    {
                        Data = null,
                        Message = "Employee not found",
                        Success = false
                    });
            }

            return Ok(
                new ApiResponse<EmployeeDto>
                {
                    Data = employee,
                    Success = true
                });
        }
        catch (Exception ex)
        {
            return BadRequest(new ApiResponse<EmployeeDto>
            {
                Error = ex.Message,
                Message = $"Error occurred. Message: {ex.Message}",
                Data = null,
                Success = false
            });
        }
        
    }

    [SwaggerOperation(Summary = "Get all employees")]
    [HttpGet("")]
    public async Task<ActionResult<ApiResponse<List<EmployeeDto>>>> GetAll()
    {
        try
        {
            var employees = await _employeeService.GetEmployeesAsync();
            if (employees == null)
            {
                return NotFound(
                    new ApiResponse<EmployeeDto>
                    {
                        Data = null,
                        Message = "Employees not found",
                        Success = false
                    });
            }

            return Ok(
                new ApiResponse<List<EmployeeDto>>
                {
                    Data = employees,
                    Success = true
                });
        }
        catch (Exception ex)
        {
            return BadRequest(
                new ApiResponse<List<EmployeeDto>>
                {
                    Error = ex.Message,
                    Message = $"Error occurred. Message: {ex.Message}",
                    Data = null,
                    Success = false
                });
        }
    }
}
