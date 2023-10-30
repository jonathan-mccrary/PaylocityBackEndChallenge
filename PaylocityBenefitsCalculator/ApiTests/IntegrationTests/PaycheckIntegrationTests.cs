using System;
using Api.Dtos.Employee;
using Api.Utilities;
using System.Collections.Generic;

namespace ApiTests.IntegrationTests
{
	public class PaycheckIntegrationTests : IntegrationTest
    {
        private List<EmployeeDto> _employees;
        public PaycheckIntegrationTests()
        {
            _employees = CreateTestingData.CreateEmployees();
        }
	}
}

