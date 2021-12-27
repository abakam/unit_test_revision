using dotnetcore_web_api.Controllers;
using dotnetcore_web_api.Data.Models;
using dotnetcore_web_api.Repositories;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace dotnetcore_web_api.Test
{
    public class EmployeeControllerTest
    {
        private readonly Mock<IGenericRepository<Employee>> _service;

        public EmployeeControllerTest()
        {
            _service = new Mock<IGenericRepository<Employee>>();
        }
        
        [Fact] //Naming convention MethodName_expectedBehaviour_StateUnderTest
        public void GetEmployee_ListOfEmployees_EmployeeExistsInRepo()
        {
            //Arrange
            var employee = GetSampleEmployee();
            _service.Setup(x => x.GetAll()).Returns(GetSampleEmployee);
            var controller = new EmployeeController(_service.Object);

            //Act
            var actionResult = controller.GetEmployee();
            var result = actionResult.Result as OkObjectResult;
            var actual = result.Value as IEnumerable<Employee>;

            //Assert
            Assert.IsType<OkObjectResult>(result);
            Assert.Equal(GetSampleEmployee().Count(), actual.Count());
        }

        [Fact]
        public void GetEmployeeById_EmployeeObject_EmployeeWithSpecificIdExists()
        {
            //Arrange
            var employees = GetSampleEmployee();
            var firstEmployee = employees[0];
            _service.Setup(x => x.GetById((long)1)).Returns(firstEmployee);
            var controller = new EmployeeController(_service.Object);

            //Act
            var actionResult = controller.GetEmployeeById((long)1);
            var result = actionResult.Result as OkObjectResult;

            //Assert
            Assert.IsType<OkObjectResult>(result);

            Assert.Same(result.Value, firstEmployee);
        }

        [Fact]
        public void GetEmployeeById_ShouldReturnBadRequest_EmployeeWithIvalidIdNotExists()
        {
            //Arrange
            var employees = GetSampleEmployee();
            var firstEmployee = employees[0];
            _service.Setup(x => x.GetById((long)1)).Returns(firstEmployee);
            var controller = new EmployeeController(_service.Object);

            //Act
            var actionResult = controller.GetEmployeeById((long)2);
            var result = actionResult.Result;

            //Assert
            Assert.IsType<NotFoundObjectResult>(result);

        }

        [Theory]
        [InlineData(18)]
        [InlineData(20)]
        public void CheckIfUserCanBeVoter_True_AgeGreaterThan18(int age)
        {
            //Arrange
            var controller = new EmployeeController(null);

            //Act
            var actual = controller.CheckIfUserCanBeVoter(age);

            //Assert
            Assert.True(actual);
        }

        [Theory]
        [InlineData(17)]
        [InlineData(15)]
        public void CheckIfUserCanBeVoter_False_AgeLessThan18(int age)
        {
            //Arrange
            var controller = new EmployeeController(null);

            //Act
            var actual = controller.CheckIfUserCanBeVoter(age);

            //Assert
            Assert.False(actual);
        }

        [Fact]
        public void CreateEmployee_CreatedStatus_PassingEmployeeObjectToCreate()
        {
            //Arrange
            var employees = GetSampleEmployee();
            var newEmployee = employees[0];
            var controller = new EmployeeController(_service.Object);

            //Act
            var actionResult = controller.CreateEmployee(newEmployee);
            var result = actionResult.Result;

            //Assert
            Assert.IsType<CreatedAtActionResult>(result);
        }

        [Fact]
        public void CreateEmployee_BadRequest_PassingNullToCreate()
        {
            //Arrange
            var controller = new EmployeeController(_service.Object);

            //Act
            var actionResult = controller.CreateEmployee(null);
            var result = actionResult.Result;

            //Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }

        private List<Employee> GetSampleEmployee()
        {
            List<Employee> output = new List<Employee>
            {
                new Employee
                {
                    FirstName = "Jhon",
                    LastName = "Doe",
                    PhoneNumber = "01682616789",
                    DateOfBirth = DateTime.Now,
                    Email = "jhon@gmail.com",
                    EmployeeId = 1
                },
                new Employee
                {
                    FirstName = "Jhon1",
                    LastName = "Doe1",
                    PhoneNumber = "01682616787",
                    DateOfBirth = DateTime.Now,
                    Email = "jhon1@gmail.com",
                    EmployeeId = 4
                },
                new Employee
                {
                    FirstName = "Jhon2",
                    LastName = "Doe2",
                    PhoneNumber = "01682616787",
                    DateOfBirth = DateTime.Now,
                    Email = "jhon2@gmail.com",
                    EmployeeId = 5
                }
            };
            return output;
        }
    }
}
