using Api.Dtos.Employee;
using Api.Models;

namespace Api.Dtos.Paycheck
{
    public class PaycheckPackageDto
	{
        public EmployeeDto? Employee { get; set; }
        public PaySplitType PaySplitType { get; set; }
        public List<PaycheckDto> Paychecks { get; set; } = new List<PaycheckDto>();
    }
}

