using Api.Models;

namespace Api.DataAccess
{
    /// <summary>
    /// Interface (contract) for BenefitsRepository
    /// </summary>
    public interface IBenefitsRepository
	{
		int? AddEmployee(Employee employee);
		Task<int?> AddEmployeeAsync(Employee employee);
		Employee? GetEmployeeById(int id);
        Task<Employee?> GetEmployeeByIdAsync(int id);
        List<Employee> GetEmployees();
        Task<List<Employee>> GetEmployeesAsync();

        int? AddDependent(Dependent dependent);
        Task<int?> AddDependentAsync(Dependent dependent);
        Dependent? GetDependentById(int id);
        Task<Dependent?> GetDependentByIdAsync(int id);
        List<Dependent> GetDependents();
        Task<List<Dependent>> GetDependentsAsync();
    }
}

