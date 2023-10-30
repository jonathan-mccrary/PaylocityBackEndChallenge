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
            PaycheckPackageDto paycheckPackageDto = new PaycheckPackageDto()
            {
                Employee = _employees.FirstOrDefault(p => p.Id == 1),
                PaySplitType = PaySplitType.Evenly
            };
            paycheckPackageDto = PaycheckCalculator.CalculatePaycheck(paycheckPackageDto);
            //act
            var response = await HttpClient.GetAsync("/api/v1/paycheck/0/1");

            //assert
            await response.ShouldReturn(HttpStatusCode.OK, paycheckPackageDto);
        }

        [Fact]
        public async Task WhenAskedForEmployee_1_Paycheck_BenefitsBonusSplitType_ShouldReturnEmployeePaycheck()
        {
            //arrange
            PaycheckPackageDto paycheckPackageDto = new PaycheckPackageDto()
            {
                Employee = _employees.FirstOrDefault(p => p.Id == 1),
                PaySplitType = PaySplitType.BenefitsBonus
            };
            paycheckPackageDto = PaycheckCalculator.CalculatePaycheck(paycheckPackageDto);
            //act
            var response = await HttpClient.GetAsync("/api/v1/paycheck/1/1");

            //assert
            await response.ShouldReturn(HttpStatusCode.OK, paycheckPackageDto);
        }

        [Fact]
        public async Task WhenAskedForEmployee_2_Paycheck_EvenSplitType_ShouldReturnEmployeePaycheck()
        {
            //arrange
            PaycheckPackageDto paycheckPackageDto = new PaycheckPackageDto()
            {
                Employee = _employees.FirstOrDefault(p => p.Id == 2),
                PaySplitType = PaySplitType.Evenly
            };
            paycheckPackageDto = PaycheckCalculator.CalculatePaycheck(paycheckPackageDto);
            //act
            var response = await HttpClient.GetAsync("/api/v1/paycheck/0/2");

            //assert
            await response.ShouldReturn(HttpStatusCode.OK, paycheckPackageDto);
        }

        [Fact]
        public async Task WhenAskedForEmployee_2_Paycheck_BenefitsBonusSplitType_ShouldReturnEmployeePaycheck()
        {
            //arrange
            var employee = _employees.FirstOrDefault(p => p.Id == 2);
            PaycheckPackageDto paycheckPackageDto = new PaycheckPackageDto()
            {
                Employee = _employees.FirstOrDefault(p => p.Id == 2),
                PaySplitType = PaySplitType.BenefitsBonus
            };
            paycheckPackageDto = PaycheckCalculator.CalculatePaycheck(paycheckPackageDto);
            //act
            var response = await HttpClient.GetAsync("/api/v1/paycheck/1/2");

            //assert
            await response.ShouldReturn(HttpStatusCode.OK, paycheckPackageDto);
        }

        [Fact]
        public async Task WhenAskedForEmployee_3_Paycheck_EvenSplitType_ShouldReturnEmployeePaycheck()
        {
            //arrange
            PaycheckPackageDto paycheckPackageDto = new PaycheckPackageDto()
            {
                Employee = _employees.FirstOrDefault(p => p.Id == 3),
                PaySplitType = PaySplitType.Evenly
            };
            paycheckPackageDto = PaycheckCalculator.CalculatePaycheck(paycheckPackageDto);
            //act
            var response = await HttpClient.GetAsync("/api/v1/paycheck/0/3");

            //assert
            await response.ShouldReturn(HttpStatusCode.OK, paycheckPackageDto);
        }

        [Fact]
        public async Task WhenAskedForEmployee_3_Paycheck_BenefitsBonusSplitType_ShouldReturnEmployeePaycheck()
        {
            //arrange
            var employee = _employees.FirstOrDefault(p => p.Id == 3);
            PaycheckPackageDto paycheckPackageDto = new PaycheckPackageDto()
            {
                Employee = _employees.FirstOrDefault(p => p.Id == 3),
                PaySplitType = PaySplitType.BenefitsBonus
            };
            paycheckPackageDto = PaycheckCalculator.CalculatePaycheck(paycheckPackageDto);
            //act
            var response = await HttpClient.GetAsync("/api/v1/paycheck/1/3");

            //assert
            await response.ShouldReturn(HttpStatusCode.OK, paycheckPackageDto);
        }

        [Fact]
        public async Task WhenAskedForEmployee_4_Paycheck_EvenSplitType_ShouldReturnEmployeePaycheck()
        {
            //arrange
            PaycheckPackageDto paycheckPackageDto = new PaycheckPackageDto()
            {
                Employee = _employees.FirstOrDefault(p => p.Id == 4),
                PaySplitType = PaySplitType.Evenly
            };
            paycheckPackageDto = PaycheckCalculator.CalculatePaycheck(paycheckPackageDto);
            //act
            var response = await HttpClient.GetAsync("/api/v1/paycheck/0/4");

            //assert
            await response.ShouldReturn(HttpStatusCode.OK, paycheckPackageDto);
        }

        [Fact]
        public async Task WhenAskedForEmployee_4_Paycheck_BenefitsBonusSplitType_ShouldReturnEmployeePaycheck()
        {
            //arrange
            PaycheckPackageDto paycheckPackageDto = new PaycheckPackageDto()
            {
                Employee = _employees.FirstOrDefault(p => p.Id == 4),
                PaySplitType = PaySplitType.BenefitsBonus
            };
            paycheckPackageDto = PaycheckCalculator.CalculatePaycheck(paycheckPackageDto);
            //act
            var response = await HttpClient.GetAsync("/api/v1/paycheck/1/4");

            //assert
            await response.ShouldReturn(HttpStatusCode.OK, paycheckPackageDto);
        }

        [Fact]
        public async Task WhenEmployeeInfoAltered_ShouldReturnDifferentValues()
        {
            //arrange
            PaycheckPackageDto paycheckPackageDto = new PaycheckPackageDto()
            {
                Employee = _employees.FirstOrDefault(p => p.Id == 1),
                PaySplitType = PaySplitType.Evenly
            };
            paycheckPackageDto.Employee.Salary = 100000.00m;
            paycheckPackageDto = PaycheckCalculator.CalculatePaycheck(paycheckPackageDto);

            //act
            var response = await HttpClient.GetAsync("/api/v1/paycheck/0/1");

            //assert
            var apiResponse = JsonConvert.DeserializeObject<ApiResponse<PaycheckDto>>(await response.Content.ReadAsStringAsync());
            Assert.NotEqual(paycheckPackageDto.Paychecks.First().NetPay, apiResponse.Data.NetPay);
        }

        [Fact]
        public async Task WhenEmployeeDependentsAltered_ShouldReturnDifferentValues()
        {
            //arrange
            PaycheckPackageDto paycheckPackageDto = new PaycheckPackageDto()
            {
                Employee = _employees.FirstOrDefault(p => p.Id == 2),
                PaySplitType = PaySplitType.Evenly
            };
            paycheckPackageDto.Employee.Dependents = paycheckPackageDto.Employee.Dependents.Take(paycheckPackageDto.Employee.Dependents.Count - 1).ToList();
            paycheckPackageDto = PaycheckCalculator.CalculatePaycheck(paycheckPackageDto);
            //act
            var response = await HttpClient.GetAsync("/api/v1/paycheck/0/2");

            //assert
            var apiResponse = JsonConvert.DeserializeObject<ApiResponse<PaycheckDto>>(await response.Content.ReadAsStringAsync());
            Assert.NotEqual(paycheckPackageDto.Paychecks.First().NetPay, apiResponse.Data.NetPay);
        }

        [Fact]
        public async Task WhenEmployeeDependentsAgeAltered_ShouldReturnDifferentValues()
        {
            //arrange
            PaycheckPackageDto paycheckPackageDto = new PaycheckPackageDto()
            {
                Employee = _employees.FirstOrDefault(p => p.Id == 2),
                PaySplitType = PaySplitType.Evenly
            };
            paycheckPackageDto.Employee.Dependents.First().DateOfBirth = DateTime.Today.AddYears(-51);
            paycheckPackageDto = PaycheckCalculator.CalculatePaycheck(paycheckPackageDto);

            //act
            var response = await HttpClient.GetAsync("/api/v1/paycheck/0/2");

            //assert
            var apiResponse = JsonConvert.DeserializeObject<ApiResponse<PaycheckDto>>(await response.Content.ReadAsStringAsync());
            Assert.NotEqual(paycheckPackageDto.Paychecks.First().NetPay, apiResponse.Data.NetPay);
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










        [Fact]
        public async Task WhenAskedForEmployee_1_Paychecks_EvenSplitType_ShouldReturnEmployeePaycheck()
        {
            //arrange
            PaycheckPackageDto paycheckPackageDto = new PaycheckPackageDto()
            {
                Employee = _employees.FirstOrDefault(p => p.Id == 1),
                PaySplitType = PaySplitType.Evenly
            };
            paycheckPackageDto = PaycheckCalculator.CalculateYearOfPaychecks(2023, paycheckPackageDto);
            //act
            var response = await HttpClient.GetAsync("/api/v1/paycheck/0/1/2023");

            //assert
            await response.ShouldReturn(HttpStatusCode.OK, paycheckPackageDto);
        }

        [Fact]
        public async Task WhenAskedForEmployee_1_Paychecks_BenefitsBonusSplitType_ShouldReturnEmployeePaycheck()
        {
            //arrange
            PaycheckPackageDto paycheckPackageDto = new PaycheckPackageDto()
            {
                Employee = _employees.FirstOrDefault(p => p.Id == 1),
                PaySplitType = PaySplitType.BenefitsBonus
            };
            paycheckPackageDto = PaycheckCalculator.CalculateYearOfPaychecks(2023, paycheckPackageDto);
            //act
            var response = await HttpClient.GetAsync("/api/v1/paycheck/1/1/2023");

            //assert
            await response.ShouldReturn(HttpStatusCode.OK, paycheckPackageDto);
        }

        [Fact]
        public async Task WhenAskedForEmployee_2_Paychecks_EvenSplitType_ShouldReturnEmployeePaycheck()
        {
            //arrange
            PaycheckPackageDto paycheckPackageDto = new PaycheckPackageDto()
            {
                Employee = _employees.FirstOrDefault(p => p.Id == 2),
                PaySplitType = PaySplitType.Evenly
            };
            paycheckPackageDto = PaycheckCalculator.CalculateYearOfPaychecks(2023, paycheckPackageDto);
            //act
            var response = await HttpClient.GetAsync("/api/v1/paycheck/0/2/2023");

            //assert
            await response.ShouldReturn(HttpStatusCode.OK, paycheckPackageDto);
        }

        [Fact]
        public async Task WhenAskedForEmployee_2_Paychecks_BenefitsBonusSplitType_ShouldReturnEmployeePaycheck()
        {
            //arrange
            var employee = _employees.FirstOrDefault(p => p.Id == 2);
            PaycheckPackageDto paycheckPackageDto = new PaycheckPackageDto()
            {
                Employee = _employees.FirstOrDefault(p => p.Id == 2),
                PaySplitType = PaySplitType.BenefitsBonus
            };
            paycheckPackageDto = PaycheckCalculator.CalculateYearOfPaychecks(2023, paycheckPackageDto);
            //act
            var response = await HttpClient.GetAsync("/api/v1/paycheck/1/2/2023");

            //assert
            await response.ShouldReturn(HttpStatusCode.OK, paycheckPackageDto);
        }

        [Fact]
        public async Task WhenAskedForEmployee_3_Paychecks_EvenSplitType_ShouldReturnEmployeePaycheck()
        {
            //arrange
            PaycheckPackageDto paycheckPackageDto = new PaycheckPackageDto()
            {
                Employee = _employees.FirstOrDefault(p => p.Id == 3),
                PaySplitType = PaySplitType.Evenly
            };
            paycheckPackageDto = PaycheckCalculator.CalculateYearOfPaychecks(2023, paycheckPackageDto);
            //act
            var response = await HttpClient.GetAsync("/api/v1/paycheck/0/3/2023");

            //assert
            await response.ShouldReturn(HttpStatusCode.OK, paycheckPackageDto);
        }

        [Fact]
        public async Task WhenAskedForEmployee_3_Paychecks_BenefitsBonusSplitType_ShouldReturnEmployeePaycheck()
        {
            //arrange
            var employee = _employees.FirstOrDefault(p => p.Id == 3);
            PaycheckPackageDto paycheckPackageDto = new PaycheckPackageDto()
            {
                Employee = _employees.FirstOrDefault(p => p.Id == 3),
                PaySplitType = PaySplitType.BenefitsBonus
            };
            paycheckPackageDto = PaycheckCalculator.CalculateYearOfPaychecks(2023, paycheckPackageDto);
            //act
            var response = await HttpClient.GetAsync("/api/v1/paycheck/1/3/2023");

            //assert
            await response.ShouldReturn(HttpStatusCode.OK, paycheckPackageDto);
        }

        [Fact]
        public async Task WhenAskedForEmployee_4_Paychecks_EvenSplitType_ShouldReturnEmployeePaycheck()
        {
            //arrange
            PaycheckPackageDto paycheckPackageDto = new PaycheckPackageDto()
            {
                Employee = _employees.FirstOrDefault(p => p.Id == 4),
                PaySplitType = PaySplitType.Evenly
            };
            paycheckPackageDto = PaycheckCalculator.CalculateYearOfPaychecks(2023, paycheckPackageDto);
            //act
            var response = await HttpClient.GetAsync("/api/v1/paycheck/0/4/2023");

            //assert
            await response.ShouldReturn(HttpStatusCode.OK, paycheckPackageDto);
        }

        [Fact]
        public async Task WhenAskedForEmployee_4_Paychecks_BenefitsBonusSplitType_ShouldReturnEmployeePaycheck()
        {
            //arrange
            PaycheckPackageDto paycheckPackageDto = new PaycheckPackageDto()
            {
                Employee = _employees.FirstOrDefault(p => p.Id == 4),
                PaySplitType = PaySplitType.BenefitsBonus
            };
            paycheckPackageDto = PaycheckCalculator.CalculateYearOfPaychecks(2023, paycheckPackageDto);
            //act
            var response = await HttpClient.GetAsync("/api/v1/paycheck/1/4/2023");

            //assert
            await response.ShouldReturn(HttpStatusCode.OK, paycheckPackageDto);
        }

        [Fact]
        public async Task WhenEmployeeInfoAltered_Year_ShouldReturnDifferentValues()
        {
            //arrange
            PaycheckPackageDto paycheckPackageDto = new PaycheckPackageDto()
            {
                Employee = _employees.FirstOrDefault(p => p.Id == 1),
                PaySplitType = PaySplitType.Evenly
            };
            paycheckPackageDto.Employee.Salary = 100000.00m;
            paycheckPackageDto = PaycheckCalculator.CalculateYearOfPaychecks(2023, paycheckPackageDto);

            //act
            var response = await HttpClient.GetAsync("/api/v1/paycheck/0/1/2023");

            //assert
            var apiResponse = JsonConvert.DeserializeObject<ApiResponse<PaycheckDto>>(await response.Content.ReadAsStringAsync());
            Assert.NotEqual(paycheckPackageDto.Paychecks.First().NetPay, apiResponse.Data.NetPay);
        }

        [Fact]
        public async Task WhenEmployeeDependentsAltered_Year_ShouldReturnDifferentValues()
        {
            //arrange
            PaycheckPackageDto paycheckPackageDto = new PaycheckPackageDto()
            {
                Employee = _employees.FirstOrDefault(p => p.Id == 2),
                PaySplitType = PaySplitType.Evenly
            };
            paycheckPackageDto.Employee.Dependents = paycheckPackageDto.Employee.Dependents.Take(paycheckPackageDto.Employee.Dependents.Count - 1).ToList();
            paycheckPackageDto = PaycheckCalculator.CalculateYearOfPaychecks(2023, paycheckPackageDto);
            //act
            var response = await HttpClient.GetAsync("/api/v1/paycheck/0/2/2023");

            //assert
            var apiResponse = JsonConvert.DeserializeObject<ApiResponse<PaycheckDto>>(await response.Content.ReadAsStringAsync());
            Assert.NotEqual(paycheckPackageDto.Paychecks.First().NetPay, apiResponse.Data.NetPay);
        }

        [Fact]
        public async Task WhenEmployeeDependentsAgeAltered_Year_ShouldReturnDifferentValues()
        {
            //arrange
            PaycheckPackageDto paycheckPackageDto = new PaycheckPackageDto()
            {
                Employee = _employees.FirstOrDefault(p => p.Id == 2),
                PaySplitType = PaySplitType.Evenly
            };
            paycheckPackageDto.Employee.Dependents.First().DateOfBirth = DateTime.Today.AddYears(-51);
            paycheckPackageDto = PaycheckCalculator.CalculateYearOfPaychecks(2023, paycheckPackageDto);

            //act
            var response = await HttpClient.GetAsync("/api/v1/paycheck/0/2/2023");

            //assert
            var apiResponse = JsonConvert.DeserializeObject<ApiResponse<PaycheckDto>>(await response.Content.ReadAsStringAsync());
            Assert.NotEqual(paycheckPackageDto.Paychecks.First().NetPay, apiResponse.Data.NetPay);
        }

        [Fact]
        public async Task WhenAskedForNonExistentPaySplitType_Year_ShouldReturnBadRequest()
        {
            //arrange

            //act
            var response = await HttpClient.GetAsync($"/api/v1/paycheck/{int.MinValue}/1/2023");

            //assert
            await response.ShouldReturn(HttpStatusCode.BadRequest);
        }


        [Fact]
        public async Task WhenAskedForNonExistentEmployeePaycheck_Year_ShouldReturnNotFound()
        {
            //arrange

            //act
            var response = await HttpClient.GetAsync($"/api/v1/paycheck/0/{int.MinValue}/2023");

            //assert
            await response.ShouldReturn(HttpStatusCode.NotFound);
        }
    }
}

