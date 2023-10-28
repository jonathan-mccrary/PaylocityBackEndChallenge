using System;
using Api.DataAccess;
using Api.Dtos.Employee;
using Api.Dtos.Paycheck;
using Api.Models;
using Api.Services.Contracts;
using AutoMapper;

namespace Api.Services
{
	public class PaycheckService : IPaycheckService
    {
        private const int _payPeriods = 26;
        private const decimal _employeeBenefitsDeduction = 1000.00m;
        private const decimal _dependentBenefitsDeduction = 600.00m;
        private const decimal _additionalBenefitsDeductionThreshold = 80000.00m;
        private const decimal _additionalBenefitsPercentage = 0.02m;
        private const int _ageBasedBenefitsThreshold = 50;
        private const decimal _ageBasedBenefitsDeduction = 200.00m;
        private readonly PaySplitType _paySplitType;

        private readonly IBenefitsRepository _benefitsRepository;
        private readonly IMapper _mapper;

        public PaycheckService(IBenefitsRepository benefitsRepository,
            IMapper mapper)
        {
            _benefitsRepository = benefitsRepository;
            _mapper = mapper;
            _paySplitType = PaySplitType.Evenly;
        }

        public GetPaycheckDto? GetEmployeePaycheck(int employeeId)
        {
            var employee = _benefitsRepository.GetEmployeeById(employeeId);
            return this.CalculatePaycheck(employee);
        }

        public async Task<GetPaycheckDto?> GetEmployeePaycheckAsync(int employeeId)
        {
            var employee = await _benefitsRepository.GetEmployeeByIdAsync(employeeId);
            return this.CalculatePaycheck(employee);
        }

        private GetPaycheckDto? CalculatePaycheck(Employee? employee)
        {
            GetPaycheckDto paycheckDto = new();
            if (employee == null)
            {
                return null;
            }

            paycheckDto.EmployeeDto = _mapper.Map<GetEmployeeDto>(employee);
            paycheckDto.GrossPay = Math.Round(employee.Salary / _payPeriods, 2);

            if (_paySplitType == PaySplitType.Evenly)
            {
                paycheckDto.EmployeeBenefitDeduction = _employeeBenefitsDeduction * 12 / _payPeriods;
                paycheckDto.NumberOfDependents = employee.Dependents.Count;
                paycheckDto.DependentBenefitsDeduction = Math.Round((_dependentBenefitsDeduction * 12 / _payPeriods) * paycheckDto.NumberOfDependents, 2);
                paycheckDto.NumberOfDependentsOverAgeThreshold = employee.Dependents.Where(p => p.DateOfBirth.AddYears(_ageBasedBenefitsThreshold) < DateTime.Today).Count();
                paycheckDto.AgeBasedBenefitsDeduction = Math.Round((_ageBasedBenefitsDeduction * 12 / _payPeriods) * paycheckDto.NumberOfDependentsOverAgeThreshold, 2);
            }
            else
            {
                bool isThirdPayPeriod = DateTime.Today.AddDays(-28).Month == DateTime.Today.Month;
                paycheckDto.EmployeeBenefitDeduction = isThirdPayPeriod
                    ? 0.00m
                    : Math.Round(_employeeBenefitsDeduction * 12 / 24, 2);
                paycheckDto.NumberOfDependents = employee.Dependents.Count;
                paycheckDto.DependentBenefitsDeduction = isThirdPayPeriod
                    ? 0.00m
                    : Math.Round((_dependentBenefitsDeduction * 12 / 24) * paycheckDto.NumberOfDependents, 2);
                paycheckDto.NumberOfDependentsOverAgeThreshold = employee.Dependents.Where(p => p.DateOfBirth.AddYears(_ageBasedBenefitsThreshold) < DateTime.Today).Count();
                paycheckDto.AgeBasedBenefitsDeduction = isThirdPayPeriod
                    ? 0.00m
                    : (_ageBasedBenefitsDeduction * 12 / 24) * paycheckDto.NumberOfDependentsOverAgeThreshold;
            }
            //
            paycheckDto.AdditionalBenefitsCost = employee.Salary > _additionalBenefitsDeductionThreshold
                ? employee.Salary * _additionalBenefitsPercentage
                : 0.00m;

            paycheckDto.DeductionsTotal = paycheckDto.EmployeeBenefitDeduction
                + paycheckDto.DependentBenefitsDeduction
                + paycheckDto.AgeBasedBenefitsDeduction
                + paycheckDto.AdditionalBenefitsCost;

            paycheckDto.NetPay = paycheckDto.GrossPay - paycheckDto.DeductionsTotal;

            return paycheckDto;
        }
    }
}