using Api.Models;

namespace Api.DataAccess
{
    public interface IBenefitsRepository
	{
		int AddEmployee(Employee employee);
		Task<int> AddEmployeeAsync(Employee employee);
		Employee? GetEmployeeById(int id);
        Task<Employee?> GetEmployeeByIdAsync(int id);
        List<Employee> GetEmployees();
        Task<List<Employee>> GetEmployeesAsync();
    }
}

