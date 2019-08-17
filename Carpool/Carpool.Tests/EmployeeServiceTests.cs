using Carpool.Application;
using Carpool.Application.Services;
using Carpool.Domain.Interfaces;
using Carpool.Domain.Models;
using Carpool.Repositories;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Carpool.Tests
{
    public class EmployeeServiceTests
    {
        [Fact]
        public void HasDriverLicense()
        {
            // Arrange
            IEmployeeRepository employeeRepository = new FakeEmployeeRepository();
            IEmployeeService employeeService = new EmployeeService(employeeRepository);

            int[] passengersId = new int[]{ 1,2};

            // Act
            var result = employeeService.HasDriverLicense(passengersId); ;

            // Assert
            Assert.True(result);
        } 

        [Fact]
        public void CanGetEmployeeByIdArray()
        {
            // Arrange
            IEmployeeRepository employeeRepository = new FakeEmployeeRepository();
            IEmployeeService employeeService = new EmployeeService(employeeRepository);

            int[] passengersId = new int[]{ 1,2 };

            // Act
            List<Employee> result = employeeService.GetEmployeesByIds(passengersId);

            // Assert
            Assert.Equal("John Lennon", result[0].Name);
            Assert.Equal("Paul McCartney", result[1].Name);
        }

        [Fact]
        public void CanGetEmployeeByIdList()
        {
            // Arrange
            IEmployeeRepository employeeRepository = new FakeEmployeeRepository();
            IEmployeeService employeeService = new EmployeeService(employeeRepository);

            // Act
            int[] employeeIds = employeeService.GetEmployeeIds(employeeRepository.GetEmployees().Take(2).ToList());

            // Assert
            Assert.Equal(1, employeeIds[0]);
            Assert.Equal(2, employeeIds[1]);
        }

        [Fact]
        public void CanGetEmplyeesFromRepository()
        {
            // Arrange
            IEmployeeRepository employeeRepository = new FakeEmployeeRepository();
            IEmployeeService employeeService = new EmployeeService(employeeRepository);

            // Act
            List<Employee> employees = employeeService.Employees.ToList();

            // Assert
            Assert.Equal(25, employees.Count);
        }
    }
}
