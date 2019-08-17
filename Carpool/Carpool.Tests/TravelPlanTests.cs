using Carpool.Domain.Models;
using System;
using System.Collections.Generic;
using Xunit;

namespace Carpool.Tests
{
    public class TravelPlanTests
    {
        [Fact]
        public void CanChangeStartLocation()
        {
            // Arrange
            var travelPlan = new TravelPlan
            {
                Id = 1,
                StartLocation = "Rijeka",
                EndLocation = "Zagreb",
                StartDate = new DateTime(2019, 8, 15),
                EndDate = new DateTime(2019, 8, 20),
                SelectedCar = new Car(),
                SelectedEmployees = new System.Collections.Generic.List<Employee>()
            };

            // Act
            travelPlan.StartLocation = "Zadar";

            // Assert
            Assert.Equal("Zadar", travelPlan.StartLocation);
        }

        [Fact]
        public void CanChangeEndLocation()
        {
            // Arrange
            var travelPlan = new TravelPlan
            {
                Id = 1,
                StartLocation = "Rijeka",
                EndLocation = "Zagreb",
                StartDate = new DateTime(2019, 8, 15),
                EndDate = new DateTime(2019, 8, 20),
                SelectedCar = new Car(),
                SelectedEmployees = new List<Employee>()
            };

            // Act
            travelPlan.EndLocation = "Zadar";

            // Assert
            Assert.Equal("Zadar", travelPlan.EndLocation);
        }

    }
}
