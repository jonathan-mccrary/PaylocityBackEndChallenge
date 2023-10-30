using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Api.Dtos.Employee;
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
    //task: make test pass
    public async Task WhenAskedForAnEmployee_ShouldReturnCorrectEmployee()
    {
        //arrange

        //act
        var response = await HttpClient.GetAsync("/api/v1/employees/1");

        //assert
        await response.ShouldReturn(HttpStatusCode.OK, _employees.FirstOrDefault(p => p.Id == 1));
    }
    
    [Fact]
    //task: make test pass
    public async Task WhenAskedForANonexistentEmployee_ShouldReturn404()
    {
        //arrange

        //act
        var response = await HttpClient.GetAsync($"/api/v1/employees/{int.MinValue}");

        //assert
        await response.ShouldReturn(HttpStatusCode.NotFound);
    }
}

