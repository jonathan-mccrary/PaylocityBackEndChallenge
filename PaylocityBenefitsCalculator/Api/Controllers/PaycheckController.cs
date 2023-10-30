using Api.Dtos.Paycheck;
using Api.Models;
using Api.Services;
using Api.Services.Contracts;
using Api.Utilities;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Api.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class PaycheckController : ControllerBase
{
    private readonly IPaycheckService _paycheckService;
    private readonly IEmployeeService _employeeService;

    public PaycheckController(IPaycheckService paycheckService,
        IEmployeeService employeeService)
    {
        _paycheckService = paycheckService;
        _employeeService = employeeService;
        CreateTestingData.SaveTestEmployees(CreateTestingData.CreateEmployees(), _employeeService);
    }

    [SwaggerOperation(Summary = "Get paycheck by employee id")]
    [HttpGet("{paycheckType}/{employeeId}")]
    public async Task<ActionResult<ApiResponse<PaycheckDto>>> Get(int paycheckType, int employeeId)
    {
        try
        {
            var paycheckDto = await _paycheckService.GetEmployeePaycheckAsync(paycheckType, employeeId);

            if (paycheckDto == null)
            {
                return NotFound(
                    new ApiResponse<PaycheckDto>
                    {
                        Data = null,
                        Message = "Unable to calculate paycheck",
                        Success = false
                    });
            }

            return Ok(
                new ApiResponse<PaycheckDto>
                {
                    Data = paycheckDto,
                    Success = true
                });

        }
        catch (Exception ex)
        {
            return BadRequest(
                new ApiResponse<PaycheckDto>
                {
                    Error = ex.Message,
                    Message = $"Error occurred. Message: {ex.Message}",
                    Data = null,
                    Success = false
                });
        }
    }
}

