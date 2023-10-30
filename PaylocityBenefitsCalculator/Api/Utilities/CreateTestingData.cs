using Api.Dtos.Dependent;
using Api.Dtos.Employee;
using Api.Models;
using Api.Services.Contracts;

namespace Api.Utilities
{
    public static class CreateTestingData
	{
        public static void SaveTestEmployees(List<EmployeeDto> employeesDtos, IEmployeeService employeeService)
        {
            foreach (var employee in employeesDtos)
            {
                var existing = employeeService.GetEmployeeById(employee.Id.Value);
                if (existing == null)
                {
                    employeeService.AddEmployee(employee);
                }
            }
        }
        public static List<EmployeeDto> CreateEmployees()
        {
            return new List<EmployeeDto>
            {
                new()
                {
                    Id = 1,
                    FirstName = "LeBron",
                    LastName = "James",
                    Salary = 75420.99m,
                    DateOfBirth = new DateTime(1984, 12, 30)
                },
                new()
                {
                    Id = 2,
                    FirstName = "Ja",
                    LastName = "Morant",
                    Salary = 92365.22m,
                    DateOfBirth = new DateTime(1999, 8, 10),
                    Dependents = new List<DependentDto>
                    {
                        new()
                        {
                            Id = 1,
                            FirstName = "Spouse",
                            LastName = "Morant",
                            Relationship = Relationship.Spouse,
                            DateOfBirth = new DateTime(1998, 3, 3)
                        },
                        new()
                        {
                            Id = 2,
                            FirstName = "Child1",
                            LastName = "Morant",
                            Relationship = Relationship.Child,
                            DateOfBirth = new DateTime(2020, 6, 23)
                        },
                        new()
                        {
                            Id = 3,
                            FirstName = "Child2",
                            LastName = "Morant",
                            Relationship = Relationship.Child,
                            DateOfBirth = new DateTime(2021, 5, 18)
                        }
                    }
                },
                new()
                {
                    Id = 3,
                    FirstName = "Michael",
                    LastName = "Jordan",
                    Salary = 143211.12m,
                    DateOfBirth = new DateTime(1963, 2, 17),
                    Dependents = new List<DependentDto>
                    {
                        new()
                        {
                            Id = 4,
                            FirstName = "DP",
                            LastName = "Jordan",
                            Relationship = Relationship.DomesticPartner,
                            DateOfBirth = new DateTime(1974, 1, 2)
                        }
                    }
                },
                new()
                {
                    Id = 4,
                    FirstName = "Michael",
                    LastName = "Jackson",
                    Salary = 150000.00m,
                    DateOfBirth = new DateTime(1920, 1, 1),
                    Dependents = new List<DependentDto>
                    {
                        new()
                        {
                            Id = 5,
                            FirstName = "Elder",
                            LastName = "Jackson",
                            Relationship = Relationship.Child,
                            DateOfBirth = new DateTime(1940, 1, 1)
                        }
                    }
                }
            };
        }
    }
}

