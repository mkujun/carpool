using Carpool.Application;
using Carpool.Application.Services;
using Carpool.Data;
using Carpool.Domain;
using Carpool.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace Carpool.Tests
{
    public class CarServiceTests
    {
        [Fact]
        public void CanGetCarByLicensePlates()
        {
            // Arrange
            ICarRepository carRepository = new FakeCarRepository();
            ICarService carService = new CarService(carRepository);

            // Act
            var result = carService.GetCar("RI 123-AB");

            // Assert
            Assert.Equal("Mustang", result.Name);
        }

        [Fact]
        public void CanEmployeeIdsFitIntoCar()
        {
            // Arrange
            ICarRepository carRepository = new FakeCarRepository();
            ICarService carService = new CarService(carRepository);
            int[] passengersId = new int[]{ 1,2};

            // Act
            var result = carService.CanFitIntoACar("RI 123-AB", passengersId);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void CanListOfEmployeesFitIntoCar()
        {
            // Arrange
            ICarRepository carRepository = new FakeCarRepository();
            ICarService carService = new CarService(carRepository);

            List<Employee> employees = new List<Employee>();
            employees.Add(new Employee(1, "Marko", true));

            // Act
            var result = carService.CanFitIntoACar("RI 123-AB", employees);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void CanGetCarsFromRepository()
        {
            // Arrange
            ICarRepository carRepository = new FakeCarRepository();
            ICarService carService = new CarService(carRepository);

            // Act
            List<Car> cars = carService.Cars.ToList();

            // Assert
            Assert.Equal(10, cars.Count);
        }
    }
}
