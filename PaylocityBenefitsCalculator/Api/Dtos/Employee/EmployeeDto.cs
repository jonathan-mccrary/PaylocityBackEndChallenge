using Api.Dtos.Dependent;

namespace Api.Dtos.Employee;

public class EmployeeDto
{
    public int? Id { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public decimal Salary { get; set; }
    public DateTime DateOfBirth { get; set; }
    public ICollection<DependentDto> Dependents { get; set; } = new List<DependentDto>();
}
