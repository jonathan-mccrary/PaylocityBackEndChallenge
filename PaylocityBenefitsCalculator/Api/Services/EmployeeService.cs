using System;
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

        public int AddEmployee(Employee employee)
        {
            return _benefitsRepository.AddEmployee(employee);
        }

        public async Task<int> AddEmployeeAsync(Employee employee)
        {
            return await _benefitsRepository.AddEmployeeAsync(employee);
        }

        public GetEmployeeDto? GetEmployeeById(int id)
        {
            var employee = _benefitsRepository.GetEmployeeById(id);
            return _mapper.Map<GetEmployeeDto>(employee);
        }

        public async Task<GetEmployeeDto?> GetEmployeeByIdAsync(int id)
        {
            var employee = await _benefitsRepository.GetEmployeeByIdAsync(id);
            return _mapper.Map<GetEmployeeDto>(employee);
        }

        public List<GetEmployeeDto> GetEmployees()
        {
            var employees = _benefitsRepository.GetEmployees();

            return _mapper.Map<List<GetEmployeeDto>>(employees);
        }

        public async Task<List<GetEmployeeDto>> GetEmployeesAsync()
        {
            var employees = await _benefitsRepository.GetEmployeesAsync();

            return _mapper.Map<List<GetEmployeeDto>>(employees);
        }
    }
}

