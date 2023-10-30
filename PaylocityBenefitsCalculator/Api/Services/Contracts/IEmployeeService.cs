using System;
using Api.Dtos.Employee;
using Api.Models;

namespace Api.Services.Contracts
{
	public interface IEmployeeService
	{
        int? AddEmployee(EmployeeDto employee);
        Task<int?> AddEmployeeAsync(EmployeeDto employee);
        EmployeeDto? GetEmployeeById(int id);
        Task<EmployeeDto?> GetEmployeeByIdAsync(int id);
        List<EmployeeDto> GetEmployees();
        Task<List<EmployeeDto>> GetEmployeesAsync();
    }
}