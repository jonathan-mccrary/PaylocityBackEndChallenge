using Api.Dtos.Employee;
using Api.Dtos.Paycheck;
using Api.Models;

namespace Api.Utilities
{
    public static class PaycheckCalculator
	{
        /*
         * For the sake of simplicity, the deduction values were stored in constants.
         * For a more scalable solution, these values shoudl be stored in a config file
         *      or in a database.
         */
        private const int _biWeeklyPayPeriods = 26;
        private const int _semiMonthyPayPeriods = 24;
        private const decimal _employeeBenefitsDeduction = 1000.00m;
        private const decimal _dependentBenefitsDeduction = 600.00m;
        private const decimal _additionalBenefitsDeductionThreshold = 80000.00m;
        private const decimal _additionalBenefitsPercentage = 0.02m;
        private const int _ageBasedBenefitsThreshold = 50;
        private const decimal _ageBasedBenefitsDeduction = 200.00m;

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
        ///     I have seen both of these scenarios in the various organizations I have worked in, so I chose to implement
        ///     both solutions I have seen for this scenario. 
        /// </summary>
        /// <param name="employee"></param>
        /// <returns></returns>
        public static PaycheckPackageDto CalculatePaycheck(PaycheckPackageDto paycheckPackageDto, DateTime? payDay = null)
        {
            PaycheckDto paycheckDto = new()
            {
                PayDay = payDay ?? DateTime.Today,
                GrossPay = Math.Round(paycheckPackageDto.Employee.Salary / _biWeeklyPayPeriods, 2)
            };
            
            if (paycheckPackageDto.PaySplitType == PaySplitType.Evenly)
            {
                //This is the split Evenly scenario described above.
                //The monthly deductions are spread evenly across all 26 pay periods.
                paycheckDto.EmployeeBenefitDeduction = Math.Round(_employeeBenefitsDeduction * 12 / _biWeeklyPayPeriods, 2);
                paycheckDto.NumberOfDependents = paycheckPackageDto.Employee.Dependents.Count;
                paycheckDto.DependentBenefitsDeduction = Math.Round((_dependentBenefitsDeduction * 12 / _biWeeklyPayPeriods) * paycheckDto.NumberOfDependents, 2);
                paycheckDto.NumberOfDependentsOverAgeThreshold = paycheckPackageDto.Employee.Dependents.Where(p => p.DateOfBirth.AddYears(_ageBasedBenefitsThreshold) < paycheckDto.PayDay).Count();
                paycheckDto.AgeBasedBenefitsDeduction = Math.Round((_ageBasedBenefitsDeduction * 12 / _biWeeklyPayPeriods) * paycheckDto.NumberOfDependentsOverAgeThreshold, 2);

                paycheckDto.AdditionalBenefitsCost = paycheckPackageDto.Employee.Salary > _additionalBenefitsDeductionThreshold
                    ? Math.Round(paycheckPackageDto.Employee.Salary * _additionalBenefitsPercentage / _biWeeklyPayPeriods, 2)
                    : 0.00m;
            }
            else
            {
                //This is the Benefits Bonus scenario described above.
                //First, it is determined whether this is the 3rd pay period in a given month.
                //If yes, then deductions are set to 0.00.
                //If no, then deductions are divided by 24 (12 months * 2 pay periods per month). 
                bool isThirdPayPeriod = paycheckDto.PayDay.AddDays(-28).Month == paycheckDto.PayDay.Month;
                paycheckDto.EmployeeBenefitDeduction = isThirdPayPeriod
                    ? 0.00m
                    : Math.Round(_employeeBenefitsDeduction * 12 / _semiMonthyPayPeriods, 2);
                paycheckDto.NumberOfDependents = paycheckPackageDto.Employee.Dependents.Count;
                paycheckDto.DependentBenefitsDeduction = isThirdPayPeriod
                    ? 0.00m
                    : Math.Round((_dependentBenefitsDeduction * 12 / _semiMonthyPayPeriods) * paycheckDto.NumberOfDependents, 2);
                paycheckDto.NumberOfDependentsOverAgeThreshold = paycheckPackageDto.Employee.Dependents.Where(p => p.DateOfBirth.AddYears(_ageBasedBenefitsThreshold) < paycheckDto.PayDay).Count();
                paycheckDto.AgeBasedBenefitsDeduction = isThirdPayPeriod
                    ? 0.00m
                    : (_ageBasedBenefitsDeduction * 12 / _semiMonthyPayPeriods) * paycheckDto.NumberOfDependentsOverAgeThreshold;

                paycheckDto.AdditionalBenefitsCost = isThirdPayPeriod
                    ? 0.00m
                    : paycheckPackageDto.Employee.Salary > _additionalBenefitsDeductionThreshold
                        ? Math.Round(paycheckPackageDto.Employee.Salary * _additionalBenefitsPercentage / _semiMonthyPayPeriods, 2)
                        : 0.00m;
            }

            paycheckDto.DeductionsTotal = paycheckDto.EmployeeBenefitDeduction
                + paycheckDto.DependentBenefitsDeduction
                + paycheckDto.AgeBasedBenefitsDeduction
                + paycheckDto.AdditionalBenefitsCost;

            paycheckDto.NetPay = paycheckDto.GrossPay - paycheckDto.DeductionsTotal;

            var lastPaycheck = paycheckPackageDto.Paychecks.LastOrDefault();
            if (lastPaycheck != null)
            {
                paycheckDto.GrossPayYTD = lastPaycheck.GrossPayYTD + paycheckDto.GrossPay;
                paycheckDto.NetPayYTD = lastPaycheck.NetPayYTD + paycheckDto.NetPay;
                paycheckDto.DeductionsTotalYTD = lastPaycheck.DeductionsTotalYTD + paycheckDto.DeductionsTotal;
                paycheckDto.EmployeeBenefitDeductionYTD = lastPaycheck.EmployeeBenefitDeductionYTD + paycheckDto.EmployeeBenefitDeduction;
                paycheckDto.DependentBenefitsDeductionYTD = lastPaycheck.DependentBenefitsDeductionYTD + paycheckDto.DependentBenefitsDeduction;
                paycheckDto.AgeBasedBenefitsDeductionYTD = lastPaycheck.AgeBasedBenefitsDeductionYTD + paycheckDto.AgeBasedBenefitsDeduction;
                paycheckDto.AdditionalBenefitsCostYTD = lastPaycheck.AdditionalBenefitsCostYTD + paycheckDto.AdditionalBenefitsCost;
            }
            else
            {
                paycheckDto.GrossPayYTD = paycheckDto.GrossPay;
                paycheckDto.NetPayYTD = paycheckDto.NetPay;
                paycheckDto.DeductionsTotalYTD = paycheckDto.DeductionsTotal;
                paycheckDto.EmployeeBenefitDeductionYTD = paycheckDto.EmployeeBenefitDeduction;
                paycheckDto.DependentBenefitsDeductionYTD = paycheckDto.DependentBenefitsDeduction;
                paycheckDto.AgeBasedBenefitsDeductionYTD = paycheckDto.AgeBasedBenefitsDeduction;
                paycheckDto.AdditionalBenefitsCostYTD = paycheckDto.AdditionalBenefitsCost;
            }

            paycheckPackageDto.Paychecks.Add(paycheckDto);
            return paycheckPackageDto;
        }

        public static PaycheckPackageDto CalculateYearOfPaychecks(int year, PaycheckPackageDto paycheckPackageDto)
        {
            DateTime payDay = GetFirstFriday(year);
            while (payDay.Year == year)
            {
                paycheckPackageDto = CalculatePaycheck(paycheckPackageDto, payDay);
                payDay = payDay.AddDays(14);
            }

            return paycheckPackageDto;
        }

        public static bool IsValidPaySplitType(int input)
        {
            PaySplitType[] values = Enum.GetValues<PaySplitType>();
            List<int> intValues = new List<int>();
            foreach(var value in values)
            {
                intValues.Add((int)value);
            }
            return intValues.Contains(input);
        }

        public static DateTime GetFirstFriday(int year)
        {
            DateTime date = DateTime.MinValue;
            for (int i = 1; i < 8; i++)
            {
                date = new DateTime(year, 1, i);
                if (date.DayOfWeek == DayOfWeek.Friday)
                {
                    break;
                }
            }
            return date;
        }
    }
}

