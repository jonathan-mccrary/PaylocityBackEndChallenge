using System;
using Api.Dtos.Paycheck;

namespace Api.Services.Contracts
{
	public interface IPaycheckService
	{
        PaycheckPackageDto? GetEmployeePaycheck(int paycheckType, int employeeId);
        Task<PaycheckPackageDto?> GetEmployeePaycheckAsync(int paycheckType, int employeeId);
        PaycheckPackageDto? GetEmployeePaycheckForYear(int paycheckType, int employeeId, int year);
        Task<PaycheckPackageDto?> GetEmployeePaycheckForYearAsync(int paycheckType, int employeeId, int year);
    }
}

