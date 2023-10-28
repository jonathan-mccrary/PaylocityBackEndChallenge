using System;
using Api.Dtos.Paycheck;

namespace Api.Services.Contracts
{
	public interface IPaycheckService
	{
		GetPaycheckDto? GetEmployeePaycheck(int employeeId);
        Task<GetPaycheckDto?> GetEmployeePaycheckAsync(int employeeId);
    }
}

