using System;
using Api.Dtos.Employee;

namespace Api.Dtos.Paycheck
{
	public class GetPaycheckDto
	{
		public GetEmployeeDto? EmployeeDto { get; set; }
		public decimal GrossPay { get; set; }
		public decimal NetPay { get; set; }
		public decimal DeductionsTotal { get; set; }
		public decimal EmployeeBenefitDeduction { get; set; }
		public int NumberOfDependents { get; set; }
		public decimal DependentBenefitsDeduction { get; set; }
		public int NumberOfDependentsOverAgeThreshold { get; set; }
		public decimal AgeBasedBenefitsDeduction { get; set; }
		public decimal AdditionalBenefitsCost { get; set; }
	}
}

