using Api.Dtos.Dependent;
using Api.Models;
using Api.Services.Contracts;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Api.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class DependentsController : ControllerBase
{
    private readonly IDependentService _dependentService;

    public DependentsController(IDependentService dependentService)
    {
        _dependentService = dependentService;
    }

    [SwaggerOperation(Summary = "Get dependent by id")]
    [HttpGet("{id}")]
    public async Task<ActionResult<ApiResponse<GetDependentDto>>> Get(int id)
    {
        try
        {
            var dependent = await _dependentService.GetDependentByIdAsync(id);

            var result = new ApiResponse<GetDependentDto>
            {
                Data = dependent,
                Success = true
            };

            return result;
        }
        catch (Exception ex)
        {
            var result = new ApiResponse<GetDependentDto>
            {
                Error = ex.Message,
                Message = $"Error occurred. Message: {ex.Message}",
                Data = null,
                Success = false
            };

            return result;
        }
    }

    [SwaggerOperation(Summary = "Get all dependents")]
    [HttpGet("")]
    public async Task<ActionResult<ApiResponse<List<GetDependentDto>>>> GetAll()
    {
        try
        {
            var dependents = await _dependentService.GetDependentsAsync();
            var result = new ApiResponse<List<GetDependentDto>>
            {
                Data = dependents,
                Success = true
            };

            return result;
        }
        catch (Exception ex)
        {
            var result = new ApiResponse<List<GetDependentDto>>
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
