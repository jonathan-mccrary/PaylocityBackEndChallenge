using Api.Dtos.Paycheck;
using Api.Models;
using Api.Services.Contracts;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Api.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class PaycheckController : ControllerBase
{
    private readonly IPaycheckService _paycheckService;
    
    public PaycheckController(IPaycheckService paycheckService)
    {
        _paycheckService = paycheckService;
        
    }

    [SwaggerOperation(Summary = "Get paycheck by employee id")]
    [HttpGet("{employeeId}")]
    public async Task<ActionResult<ApiResponse<GetPaycheckDto>>> Get(int employeeId)
    {
        try
        {
            var paycheckDto = await _paycheckService.GetEmployeePaycheckAsync(employeeId);

            if (paycheckDto == null)
            {
                return new ApiResponse<GetPaycheckDto>
                {
                    Data = null,
                    Message = "Unable to calculate paycheck",
                    Success = false
                };
            }

            return new ApiResponse<GetPaycheckDto>
            {
                Data = paycheckDto,
                Success = true
            };

        }
        catch (Exception ex)
        {
            return new ApiResponse<GetPaycheckDto>
            {
                Error = ex.Message,
                Message = $"Error occurred. Message: {ex.Message}",
                Data = null,
                Success = false
            };

        }

    }

}

