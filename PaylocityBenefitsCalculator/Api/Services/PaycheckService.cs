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
        private const int _biWeeklyPayPeriods = 26;
        private const int _semiMonthyPayPeriods = 24;

        /*
         * For the sake of simplicity, the deduction values were stored in constants.
         * For a more scalable solution, these values shoudl be stored in a config file
         *      or in a database.
         */
        private const decimal _employeeBenefitsDeduction = 1000.00m;
        private const decimal _dependentBenefitsDeduction = 600.00m;
        private const decimal _additionalBenefitsDeductionThreshold = 80000.00m;
        private const decimal _additionalBenefitsPercentage = 0.02m;
        private const int _ageBasedBenefitsThreshold = 50;
        private const decimal _ageBasedBenefitsDeduction = 200.00m;

        private PaySplitType _paySplitType;

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
            _paySplitType = (PaySplitType)paycheckType;
            var employee = _benefitsRepository.GetEmployeeById(employeeId);
            return this.CalculatePaycheck(employee);
        }

        public async Task<PaycheckDto?> GetEmployeePaycheckAsync(int paycheckType, int employeeId)
        {
            _paySplitType = (PaySplitType)paycheckType;
            var employee = await _benefitsRepository.GetEmployeeByIdAsync(employeeId);
            return this.CalculatePaycheck(employee);
        }

        /// <summary>
        /// Business Rules:
        ///     26 paychecks per year with deductions spread as evenly as possible on each paycheck
        ///     employees have a base cost of $1,000 per month(for benefits)
        ///     each dependent represents an additional $600 cost per month(for benefits)
        ///     employees that make more than $80,000 per year will incur an additional 2% of their yearly salary in benefits costs
        ///     dependents that are over 50 years old will incur an additional $200 per month
        ///
        /// Assumptions:
        ///     The implementation of the calculation business rules was divided into two paths:
        ///         Evenly
        ///             Split the monthly deductions across the 26 paychecks.
        ///             This would mean that some months the employee would pay less than the monthly deduction,
        ///             but the total yearly deductions would be met across the 26 paychecks.
        ///         Benefits Bonus
        ///             Split the monthly deductions across 24 paychecks (12 months * 2 paychecks per month = 24 paychecks).
        ///             This would leave 2 months with 3 paychecks, and thus a "benefits bonus". This could be handled by spreading
        ///             the benefits across 3 checks or (the decision I have made) make the 3rd check in a month be free of
        ///             deductions.
        ///     I have seen both of these scenarios in the various organizations I have worked in. Given the requirements'
        ///     ambiguity for how to handle spreading monthly deductions across bi-weekly paychecks, I chose to implement
        ///     both solutions I have seen for this scenario. 
        /// </summary>
        /// <param name="employee"></param>
        /// <returns></returns>
        private PaycheckDto? CalculatePaycheck(Employee? employee)
        {
            PaycheckDto paycheckDto = new();
            if (employee == null)
            {
                return null;
            }

            paycheckDto.EmployeeDto = _mapper.Map<EmployeeDto>(employee);
            paycheckDto.GrossPay = Math.Round(employee.Salary / _biWeeklyPayPeriods, 2);

            if (_paySplitType == PaySplitType.Evenly)
            {
                //This is the split Evenly scenario described above.
                //The monthly deductions are spread evenly across all 26 pay periods.
                paycheckDto.EmployeeBenefitDeduction = Math.Round(_employeeBenefitsDeduction * 12 / _biWeeklyPayPeriods, 2);
                paycheckDto.NumberOfDependents = employee.Dependents.Count;
                paycheckDto.DependentBenefitsDeduction = Math.Round((_dependentBenefitsDeduction * 12 / _biWeeklyPayPeriods) * paycheckDto.NumberOfDependents, 2);
                paycheckDto.NumberOfDependentsOverAgeThreshold = employee.Dependents.Where(p => p.DateOfBirth.AddYears(_ageBasedBenefitsThreshold) < DateTime.Today).Count();
                paycheckDto.AgeBasedBenefitsDeduction = Math.Round((_ageBasedBenefitsDeduction * 12 / _biWeeklyPayPeriods) * paycheckDto.NumberOfDependentsOverAgeThreshold, 2);

                paycheckDto.AdditionalBenefitsCost = employee.Salary > _additionalBenefitsDeductionThreshold
                    ? Math.Round(employee.Salary * _additionalBenefitsPercentage / _biWeeklyPayPeriods, 2)
                    : 0.00m;
            }
            else
            {
                //This is the Benefits Bonus scenario described above.
                //First, it is determined whether this is the 3rd pay period in a given month.
                //If yes, then deductions are set to 0.00.
                //If no, then deductions are divided by 24 (12 months * 2 pay periods per month). 
                bool isThirdPayPeriod = DateTime.Today.AddDays(-28).Month == DateTime.Today.Month;
                paycheckDto.EmployeeBenefitDeduction = isThirdPayPeriod
                    ? 0.00m
                    : Math.Round(_employeeBenefitsDeduction * 12 / _semiMonthyPayPeriods, 2);
                paycheckDto.NumberOfDependents = employee.Dependents.Count;
                paycheckDto.DependentBenefitsDeduction = isThirdPayPeriod
                    ? 0.00m
                    : Math.Round((_dependentBenefitsDeduction * 12 / _semiMonthyPayPeriods) * paycheckDto.NumberOfDependents, 2);
                paycheckDto.NumberOfDependentsOverAgeThreshold = employee.Dependents.Where(p => p.DateOfBirth.AddYears(_ageBasedBenefitsThreshold) < DateTime.Today).Count();
                paycheckDto.AgeBasedBenefitsDeduction = isThirdPayPeriod
                    ? 0.00m
                    : (_ageBasedBenefitsDeduction * 12 / _semiMonthyPayPeriods) * paycheckDto.NumberOfDependentsOverAgeThreshold;

                paycheckDto.AdditionalBenefitsCost = isThirdPayPeriod
                    ? 0.00m
                    : employee.Salary > _additionalBenefitsDeductionThreshold
                        ? Math.Round(employee.Salary * _additionalBenefitsPercentage / _semiMonthyPayPeriods, 2)
                        : 0.00m;
            }

            /*
             * This is commented out becasue the requirements 
             *      "employees that make more than $80,000 per year will incur an additional 2% of their yearly salary in benefits costs"
             * are unclear whether the additional 2% is distributed at 2% per paycheck, per month (and split evenly across paychecks,
             * or per year (and split evenly across paychecks). The assumption made above is that the 2% would be applied per year
             * and distributed across all paychecks. The commented code below is for the assumption that the 2% of salary is deducted
             * for every single paycheck.
             */
            /*
            paycheckDto.AdditionalBenefitsCost = employee.Salary > _additionalBenefitsDeductionThreshold
                ? employee.Salary * _additionalBenefitsPercentage
                : 0.00m;
            */

            paycheckDto.DeductionsTotal = paycheckDto.EmployeeBenefitDeduction
                + paycheckDto.DependentBenefitsDeduction
                + paycheckDto.AgeBasedBenefitsDeduction
                + paycheckDto.AdditionalBenefitsCost;

            paycheckDto.NetPay = paycheckDto.GrossPay - paycheckDto.DeductionsTotal;

            return paycheckDto;
        }
    }
}