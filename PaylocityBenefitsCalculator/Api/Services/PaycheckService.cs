using Api.DataAccess;
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

        public PaycheckPackageDto? GetEmployeePaycheck(int paycheckType, int employeeId)
        {
            if (!PaycheckCalculator.IsValidPaySplitType(paycheckType))
            {
                throw new Exception("Invalid PaySplitType");
            }
            var employee = _benefitsRepository.GetEmployeeById(employeeId);
            if (employee == null)
            {
                return null;
            }

            PaycheckPackageDto paycheckPackageDto = new PaycheckPackageDto()
            {
                Employee = _mapper.Map<EmployeeDto>(employee),
                PaySplitType = (PaySplitType)paycheckType
            };

            return PaycheckCalculator.CalculatePaycheck(paycheckPackageDto);
        }

        public async Task<PaycheckPackageDto?> GetEmployeePaycheckAsync(int paycheckType, int employeeId)
        {
            if (!PaycheckCalculator.IsValidPaySplitType(paycheckType))
            {
                throw new Exception("Invalid PaySplitType");
            }
            var employee = await _benefitsRepository.GetEmployeeByIdAsync(employeeId);
            if (employee == null)
            {
                return null;
            }

            PaycheckPackageDto paycheckPackageDto = new PaycheckPackageDto()
            {
                Employee = _mapper.Map<EmployeeDto>(employee),
                PaySplitType = (PaySplitType)paycheckType
            };

            return PaycheckCalculator.CalculatePaycheck(paycheckPackageDto);
        }


        public PaycheckPackageDto? GetEmployeePaycheckForYear(int paycheckType, int employeeId, int year)
        {
            if (!PaycheckCalculator.IsValidPaySplitType(paycheckType))
            {
                throw new Exception("Invalid PaySplitType");
            }
            var employee = _benefitsRepository.GetEmployeeById(employeeId);
            if (employee == null)
            {
                return null;
            }

            PaycheckPackageDto paycheckPackageDto = new PaycheckPackageDto()
            {
                Employee = _mapper.Map<EmployeeDto>(employee),
                PaySplitType = (PaySplitType)paycheckType
            };

            return PaycheckCalculator.CalculateYearOfPaychecks(year, paycheckPackageDto);
        }

        public async Task<PaycheckPackageDto?> GetEmployeePaycheckForYearAsync(int paycheckType, int employeeId, int year)
        {
            if (!PaycheckCalculator.IsValidPaySplitType(paycheckType))
            {
                throw new Exception("Invalid PaySplitType");
            }
            var employee = await _benefitsRepository.GetEmployeeByIdAsync(employeeId);
            if (employee == null)
            {
                return null;
            }

            PaycheckPackageDto paycheckPackageDto = new PaycheckPackageDto()
            {
                Employee = _mapper.Map<EmployeeDto>(employee),
                PaySplitType = (PaySplitType)paycheckType
            };

            return PaycheckCalculator.CalculateYearOfPaychecks(year, paycheckPackageDto);
        }

       
    }
}