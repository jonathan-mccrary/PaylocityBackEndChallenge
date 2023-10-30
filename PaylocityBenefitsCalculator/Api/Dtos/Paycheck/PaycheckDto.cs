using Api.Dtos.Employee;

namespace Api.Dtos.Paycheck
{
	/// <summary>
	/// DTO for handling Paycheck data to be sent through the API
	/// </summary>
    public class PaycheckDto
	{
		public EmployeeDto? EmployeeDto { get; set; }
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

