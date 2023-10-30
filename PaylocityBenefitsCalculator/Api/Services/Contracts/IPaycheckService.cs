using System;
using Api.Dtos.Paycheck;

namespace Api.Services.Contracts
{
	public interface IPaycheckService
	{
		PaycheckDto? GetEmployeePaycheck(int paycheckType, int employeeId);
        Task<PaycheckDto?> GetEmployeePaycheckAsync(int paycheckType, int employeeId);
    }
}

