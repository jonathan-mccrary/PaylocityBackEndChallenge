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

        public int AddDependent(Dependent dependent)
        {
            _context.Dependents.Add(dependent);
            _context.SaveChanges();
            return dependent.Id;
        }

        public async Task<int> AddDependentAsync(Dependent dependent)
        {
            _context.Dependents.Add(dependent);
            await _context.SaveChangesAsync();
            return dependent.Id;
        }

        public Dependent? GetDependentById(int id)
        {
            return _context.Dependents
               .FirstOrDefault(p => p.Id == id);
        }

        public async Task<Dependent?> GetDependentByIdAsync(int id)
        {
            return await _context.Dependents
               .FirstOrDefaultAsync(p => p.Id == id);
        }
        
        public List<Dependent> GetDependents()
        {
            return _context.Dependents
                .ToList();
        }

        public async Task<List<Dependent>> GetDependentsAsync()
        {
            return await _context.Dependents
                .ToListAsync();
        }
    }
}