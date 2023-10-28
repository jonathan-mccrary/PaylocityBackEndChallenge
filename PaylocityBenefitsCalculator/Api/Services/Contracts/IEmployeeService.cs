using System;
using Api.Dtos.Employee;
using Api.Models;

namespace Api.Services.Contracts
{
	public interface IEmployeeService
	{
        int AddEmployee(Employee employee);
        Task<int> AddEmployeeAsync(Employee employee);
        GetEmployeeDto? GetEmployeeById(int id);
        Task<GetEmployeeDto?> GetEmployeeByIdAsync(int id);
        List<GetEmployeeDto> GetEmployees();
        Task<List<GetEmployeeDto>> GetEmployeesAsync();
    }
}