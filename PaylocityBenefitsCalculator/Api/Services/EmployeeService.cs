using Api.DataAccess;
using Api.Dtos.Employee;
using Api.Models;
using Api.Services.Contracts;
using AutoMapper;

namespace Api.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IBenefitsRepository _benefitsRepository;
        private readonly IMapper _mapper;

        public EmployeeService(IBenefitsRepository benefitsRepository,
            IMapper mapper)
        {
            _benefitsRepository = benefitsRepository;
            _mapper = mapper;
        }

        public int? AddEmployee(EmployeeDto employee)
        {
            if (employee.Dependents.Count > 0)
            {
                var partnersCount = employee.Dependents.Where(p => p.Relationship == Relationship.DomesticPartner
                    || p.Relationship == Relationship.Spouse).Count();
                if (partnersCount > 1)
                {
                    return -1;
                }
            }

            return _benefitsRepository.AddEmployee(_mapper.Map<EmployeeDto, Employee>(employee));
        }

        public async Task<int?> AddEmployeeAsync(EmployeeDto employee)
        {
            if (employee.Dependents.Count > 0)
            {
                var partnersCount = employee.Dependents.Where(p => p.Relationship == Relationship.DomesticPartner
                    || p.Relationship == Relationship.Spouse).Count();
                if (partnersCount > 1)
                {
                    return -1;
                }
            }
            return await _benefitsRepository.AddEmployeeAsync(_mapper.Map<EmployeeDto, Employee>(employee));
        }

        public EmployeeDto? GetEmployeeById(int id)
        {
            var employee = _benefitsRepository.GetEmployeeById(id);
            return _mapper.Map<Employee, EmployeeDto>(employee);
        }

        public async Task<EmployeeDto?> GetEmployeeByIdAsync(int id)
        {
            var employee = await _benefitsRepository.GetEmployeeByIdAsync(id);
            return _mapper.Map<Employee, EmployeeDto>(employee);
        }

        public List<EmployeeDto> GetEmployees()
        {
            var employees = _benefitsRepository.GetEmployees();

            return _mapper.Map<List<Employee>, List<EmployeeDto>>(employees);
        }

        public async Task<List<EmployeeDto>> GetEmployeesAsync()
        {
            var employees = await _benefitsRepository.GetEmployeesAsync();

            return _mapper.Map<List<Employee>, List<EmployeeDto>>(employees);
        }
    }
}

