using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Api.Dtos.Dependent;
using Api.Dtos.Employee;
using Api.Models;
using Api.Utilities;
using Xunit;

namespace ApiTests.IntegrationTests;

public class EmployeeIntegrationTests : IntegrationTest
{
    private List<EmployeeDto> _employees;
    public EmployeeIntegrationTests()
    {
        _employees = CreateTestingData.CreateEmployees();
    }

    [Fact]
    public async Task WhenAskedForAllEmployees_ShouldReturnAllEmployees()
    {
        //arrange

        //act
        var response = await HttpClient.GetAsync("/api/v1/employees");

        //assert
        await response.ShouldReturn(HttpStatusCode.OK, _employees);
    }

    [Fact]
    public async Task WhenAskedForAnEmployee_ShouldReturnCorrectEmployee()
    {
        //arrange

        //act
        var response = await HttpClient.GetAsync("/api/v1/employees/1");

        //assert
        await response.ShouldReturn(HttpStatusCode.OK, _employees.FirstOrDefault(p => p.Id == 1));
    }
    
    [Fact]
    public async Task WhenAskedForANonexistentEmployee_ShouldReturn404()
    {
        //arrange

        //act
        var response = await HttpClient.GetAsync($"/api/v1/employees/{int.MinValue}");

        //assert
        await response.ShouldReturn(HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task WhenAddingTwoPartners_ShouldReturnBadRequest()
    {
        //arrange
        var employee = new EmployeeDto()
        {
            Id = 6,
            FirstName = "Test",
            LastName = "Employee",
            Salary = 100000.00m,
            DateOfBirth = new DateTime(1999, 8, 10),
            Dependents = new List<DependentDto>
            {
                new()
                {
                    Id = 1,
                    FirstName = "Spouse",
                    LastName = "Employee",
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
        };
        //act
        var response = await HttpClient.PostAsync($"/api/v1/employees",
            new StringContent(
                JsonSerializer.Serialize(employee),
                Encoding.UTF8,
                "application/json"));

        //assert
        await response.ShouldReturn(HttpStatusCode.BadRequest);
    }
}

