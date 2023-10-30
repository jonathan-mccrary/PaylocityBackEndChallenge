using System;
using Api.Dtos.Employee;
using Api.Utilities;
using System.Collections.Generic;
using Api.Models;
using System.Net;
using System.Threading.Tasks;
using Xunit;
using System.Linq;
using Newtonsoft.Json;
using Api.Dtos.Paycheck;

namespace ApiTests.IntegrationTests
{
	public class PaycheckIntegrationTests : IntegrationTest
    {
        private List<EmployeeDto> _employees;
        public PaycheckIntegrationTests()
        {
            _employees = CreateTestingData.CreateEmployees();
        }

        [Fact]
        public async Task WhenAskedForEmployee_1_Paycheck_EvenSplitType_ShouldReturnEmployeePaycheck()
        {
            //arrange
            var employee = _employees.FirstOrDefault(p => p.Id == 1);
            var paycheck = PaycheckCalculator.CalculatePaycheck(PaySplitType.Evenly, employee);
            //act
            var response = await HttpClient.GetAsync("/api/v1/paycheck/0/1");

            //assert
            await response.ShouldReturn(HttpStatusCode.OK, paycheck);
        }

        [Fact]
        public async Task WhenAskedForEmployee_1_Paycheck_BenefitsBonusSplitType_ShouldReturnEmployeePaycheck()
        {
            //arrange
            var employee = _employees.FirstOrDefault(p => p.Id == 1);
            var paycheck = PaycheckCalculator.CalculatePaycheck(PaySplitType.BenefitsBonus, employee);
            //act
            var response = await HttpClient.GetAsync("/api/v1/paycheck/1/1");

            //assert
            await response.ShouldReturn(HttpStatusCode.OK, paycheck);
        }

        [Fact]
        public async Task WhenAskedForEmployee_2_Paycheck_EvenSplitType_ShouldReturnEmployeePaycheck()
        {
            //arrange
            var employee = _employees.FirstOrDefault(p => p.Id == 2);
            var paycheck = PaycheckCalculator.CalculatePaycheck(PaySplitType.Evenly, employee);
            //act
            var response = await HttpClient.GetAsync("/api/v1/paycheck/0/2");

            //assert
            await response.ShouldReturn(HttpStatusCode.OK, paycheck);
        }

        [Fact]
        public async Task WhenAskedForEmployee_2_Paycheck_BenefitsBonusSplitType_ShouldReturnEmployeePaycheck()
        {
            //arrange
            var employee = _employees.FirstOrDefault(p => p.Id == 2);
            var paycheck = PaycheckCalculator.CalculatePaycheck(PaySplitType.BenefitsBonus, employee);
            //act
            var response = await HttpClient.GetAsync("/api/v1/paycheck/1/2");

            //assert
            await response.ShouldReturn(HttpStatusCode.OK, paycheck);
        }

        [Fact]
        public async Task WhenAskedForEmployee_3_Paycheck_EvenSplitType_ShouldReturnEmployeePaycheck()
        {
            //arrange
            var employee = _employees.FirstOrDefault(p => p.Id == 3);
            var paycheck = PaycheckCalculator.CalculatePaycheck(PaySplitType.Evenly, employee);
            //act
            var response = await HttpClient.GetAsync("/api/v1/paycheck/0/3");

            //assert
            await response.ShouldReturn(HttpStatusCode.OK, paycheck);
        }

        [Fact]
        public async Task WhenAskedForEmployee_3_Paycheck_BenefitsBonusSplitType_ShouldReturnEmployeePaycheck()
        {
            //arrange
            var employee = _employees.FirstOrDefault(p => p.Id == 3);
            var paycheck = PaycheckCalculator.CalculatePaycheck(PaySplitType.BenefitsBonus, employee);
            //act
            var response = await HttpClient.GetAsync("/api/v1/paycheck/1/3");

            //assert
            await response.ShouldReturn(HttpStatusCode.OK, paycheck);
        }

        [Fact]
        public async Task WhenAskedForEmployee_4_Paycheck_EvenSplitType_ShouldReturnEmployeePaycheck()
        {
            //arrange
            var employee = _employees.FirstOrDefault(p => p.Id == 4);
            var paycheck = PaycheckCalculator.CalculatePaycheck(PaySplitType.Evenly, employee);
            //act
            var response = await HttpClient.GetAsync("/api/v1/paycheck/0/4");

            //assert
            await response.ShouldReturn(HttpStatusCode.OK, paycheck);
        }

        [Fact]
        public async Task WhenAskedForEmployee_4_Paycheck_BenefitsBonusSplitType_ShouldReturnEmployeePaycheck()
        {
            //arrange
            var employee = _employees.FirstOrDefault(p => p.Id == 4);
            var paycheck = PaycheckCalculator.CalculatePaycheck(PaySplitType.BenefitsBonus, employee);
            //act
            var response = await HttpClient.GetAsync("/api/v1/paycheck/1/4");

            //assert
            await response.ShouldReturn(HttpStatusCode.OK, paycheck);
        }

        [Fact]
        public async Task WhenEmployeeInfoAltered_ShouldReturnDifferentValues()
        {
            //arrange
            var employee = _employees.FirstOrDefault(p => p.Id == 1);
            employee.Salary = 100000.00m;
            var paycheck = PaycheckCalculator.CalculatePaycheck(PaySplitType.Evenly, employee);
            //act
            var response = await HttpClient.GetAsync("/api/v1/paycheck/0/1");

            //assert
            var apiResponse = JsonConvert.DeserializeObject<ApiResponse<PaycheckDto>>(await response.Content.ReadAsStringAsync());
            Assert.NotEqual(paycheck.NetPay, apiResponse.Data.NetPay);
        }

        [Fact]
        public async Task WhenEmployeeDependentsAltered_ShouldReturnDifferentValues()
        {
            //arrange
            var employee = _employees.FirstOrDefault(p => p.Id == 2);
            employee.Dependents = employee.Dependents.Take(employee.Dependents.Count - 1).ToList();
            var paycheck = PaycheckCalculator.CalculatePaycheck(PaySplitType.Evenly, employee);
            //act
            var response = await HttpClient.GetAsync("/api/v1/paycheck/0/2");

            //assert
            var apiResponse = JsonConvert.DeserializeObject<ApiResponse<PaycheckDto>>(await response.Content.ReadAsStringAsync());
            Assert.NotEqual(paycheck.NetPay, apiResponse.Data.NetPay);
        }

        [Fact]
        public async Task WhenEmployeeDependentsAgeAltered_ShouldReturnDifferentValues()
        {
            //arrange
            var employee = _employees.FirstOrDefault(p => p.Id == 2);
            employee.Dependents.First().DateOfBirth = DateTime.Today.AddYears(-51);
            var paycheck = PaycheckCalculator.CalculatePaycheck(PaySplitType.Evenly, employee);
            //act
            var response = await HttpClient.GetAsync("/api/v1/paycheck/0/2");

            //assert
            var apiResponse = JsonConvert.DeserializeObject<ApiResponse<PaycheckDto>>(await response.Content.ReadAsStringAsync());
            Assert.NotEqual(paycheck.NetPay, apiResponse.Data.NetPay);
        }

        [Fact]
        public async Task WhenAskedForNonExistentPaySplitType_ShouldReturnBadRequest()
        {
            //arrange

            //act
            var response = await HttpClient.GetAsync($"/api/v1/paycheck/{int.MinValue}/1");

            //assert
            await response.ShouldReturn(HttpStatusCode.BadRequest);
        }


        [Fact]
        public async Task WhenAskedForNonExistentEmployeePaycheck_ShouldReturnNotFound()
        {
            //arrange

            //act
            var response = await HttpClient.GetAsync($"/api/v1/paycheck/0/{int.MinValue}");

            //assert
            await response.ShouldReturn(HttpStatusCode.NotFound);
        }
    }
}

