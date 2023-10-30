﻿using Api.DataAccess;
using Api.Dtos.Employee;
using Api.Dtos.Paycheck;
using Api.Models;
using Api.Services.Contracts;
using Api.Utilities;
using AutoMapper;

namespace Api.Services
{
    public class PaycheckService : IPaycheckService
    {
        private readonly IBenefitsRepository _benefitsRepository;
        private readonly IMapper _mapper;

        public PaycheckService(IBenefitsRepository benefitsRepository,
            IMapper mapper)
        {
            _benefitsRepository = benefitsRepository;
            _mapper = mapper;
            
        }

        public PaycheckDto? GetEmployeePaycheck(int paycheckType, int employeeId)
        {
            if (!PaycheckCalculator.IsValidPaySplitType(paycheckType))
            {
                throw new Exception("Invalid PaySplitType");
            }
            var employee = _benefitsRepository.GetEmployeeById(employeeId);
            return PaycheckCalculator.CalculatePaycheck((PaySplitType)paycheckType, _mapper.Map<EmployeeDto>(employee));
        }

        public async Task<PaycheckDto?> GetEmployeePaycheckAsync(int paycheckType, int employeeId)
        {
            if (!PaycheckCalculator.IsValidPaySplitType(paycheckType))
            {
                throw new Exception("Invalid PaySplitType");
            }
            var employee = await _benefitsRepository.GetEmployeeByIdAsync(employeeId);
            return PaycheckCalculator.CalculatePaycheck((PaySplitType)paycheckType, _mapper.Map<EmployeeDto>(employee));
        }
    }
}