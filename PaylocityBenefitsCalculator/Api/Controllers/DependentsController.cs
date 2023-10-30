using Api.Dtos.Dependent;
using Api.Models;
using Api.Services.Contracts;
using Api.Utilities;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Api.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class DependentsController : ControllerBase
{
    private readonly IDependentService _dependentService;
    private readonly IEmployeeService _employeeService;

    public DependentsController(IDependentService dependentService,
        IEmployeeService employeeService)
    {
        _dependentService = dependentService;
        _employeeService = employeeService;
        CreateTestingData.SaveTestEmployees(CreateTestingData.CreateEmployees(), _employeeService);
    }

    [SwaggerOperation(Summary = "Get dependent by id")]
    [HttpGet("{id}")]
    public async Task<ActionResult<ApiResponse<DependentDto>>> Get(int id)
    {
        try
        {
            var dependent = await _dependentService.GetDependentByIdAsync(id);
            if (dependent == null)
            {
                return NotFound(
                    new ApiResponse<DependentDto>
                    {
                        Data = null,
                        Message = "Dependent not found",
                        Success = false
                    });
            }

            return Ok(
                new ApiResponse<DependentDto>
                {
                    Data = dependent,
                    Success = true
                });
        }
        catch (Exception ex)
        {
            return BadRequest(
                new ApiResponse<DependentDto>
                {
                    Error = ex.Message,
                    Message = $"Error occurred. Message: {ex.Message}",
                    Data = null,
                    Success = false
                });
        }
    }

    [SwaggerOperation(Summary = "Get all dependents")]
    [HttpGet("")]
    public async Task<ActionResult<ApiResponse<List<DependentDto>>>> GetAll()
    {
        try
        {
            var dependents = await _dependentService.GetDependentsAsync();
            if (dependents == null)
            {
                return NotFound(
                    new ApiResponse<List<DependentDto>>
                    {
                        Data = null,
                        Message = "Dependents not found",
                        Success = false
                    });
            }

            return Ok(
                new ApiResponse<List<DependentDto>>
                {
                    Data = dependents,
                    Success = true
                });
        }
        catch (Exception ex)
        {
            return BadRequest(
                new ApiResponse<List<DependentDto>>
                {
                    Error = ex.Message,
                    Message = $"Error occurred. Message: {ex.Message}",
                    Data = null,
                    Success = false
                });
        }
    }
}
