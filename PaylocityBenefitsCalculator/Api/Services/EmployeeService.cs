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

        /// <summary>
        /// If it is found that an employee has more than one partner (spouse/domestic partner),
        /// -1 is returned. This indicates to the controller that the business rule was violated.
        /// This is a simple way to signify this--a more extensible solution would be to create a
        /// new return type indicating the result of the insert.
        /// </summary>
        /// <param name="employee"></param>
        /// <returns></returns>
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

        /// <summary>
        /// If it is found that an employee has more than one partner (spouse/domestic partner),
        /// -1 is returned. This indicates to the controller that the business rule was violated.
        /// This is a simple way to signify this--a more extensible solution would be to create a
        /// new return type indicating the result of the insert.
        /// </summary>
        /// <param name="employee"></param>
        /// <returns></returns>
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

