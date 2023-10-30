using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Api.Dtos.Dependent;
using Api.Dtos.Employee;
using Api.Models;
using Api.Utilities;
using Xunit;

namespace ApiTests.IntegrationTests;

public class DependentIntegrationTests : IntegrationTest
{
    private List<EmployeeDto> _employees;
    private List<DependentDto> _dependents;
    public DependentIntegrationTests()
    {
        _employees = CreateTestingData.CreateEmployees();
        _dependents = new List<DependentDto>();
        foreach (var employee in _employees)
        {
            _dependents.AddRange(employee.Dependents);
        }
    }

    [Fact]
    public async Task WhenAskedForAllDependents_ShouldReturnAllDependents()
    {
        //arrange

        //act
        var response = await HttpClient.GetAsync("/api/v1/dependents");

        //assert
        await response.ShouldReturn(HttpStatusCode.OK, _dependents);
    }

    [Fact]
    public async Task WhenAskedForADependent_ShouldReturnCorrectDependent()
    {
        //arrange

        //act
        var response = await HttpClient.GetAsync("/api/v1/dependents/1");

        //assert
        await response.ShouldReturn(HttpStatusCode.OK, _dependents.FirstOrDefault(p => p.Id == 1));
    }

    [Fact]
    public async Task WhenAskedForANonexistentDependent_ShouldReturn404()
    {
        //arrange

        //act
        var response = await HttpClient.GetAsync($"/api/v1/dependents/{int.MinValue}");

        //assert
        await response.ShouldReturn(HttpStatusCode.NotFound);
    }
}

