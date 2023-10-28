using Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Api.DataAccess
{
    public class BenefitsRepository : IBenefitsRepository
    {

        private readonly ApiContext _context;
        public BenefitsRepository(ApiContext context)
        {
            _context = context;
        }

        public int AddEmployee(Employee employee)
        {
            _context.Employees.Add(employee);
            _context.Dependents.AddRange(employee.Dependents);
            _context.SaveChanges();
            return employee.Id;
        }

        public async Task<int> AddEmployeeAsync(Employee employee)
        {
            _context.Employees.Add(employee);
            _context.Dependents.AddRange(employee.Dependents);
            await _context.SaveChangesAsync();
            return employee.Id;
        }

        public Employee? GetEmployeeById(int id)
        {
            return _context.Employees
                .Include(p => p.Dependents)
                .FirstOrDefault(p => p.Id == id);
        }

        public async Task<Employee?> GetEmployeeByIdAsync(int id)
        {
            return await _context.Employees
                .Include(p => p.Dependents)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public List<Employee> GetEmployees()
        {
            return _context.Employees
                .Include(p => p.Dependents)
                .ToList();
        }

        public async Task<List<Employee>> GetEmployeesAsync()
        {
            return await _context.Employees
                .Include(p => p.Dependents)
                .ToListAsync();
        }
    }
}

