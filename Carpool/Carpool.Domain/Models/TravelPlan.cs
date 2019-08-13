using System;
using System.Collections.Generic;

namespace Carpool.Domain.Models
{
    public class TravelPlan
    {
        public TravelPlan()
        {

        }

        public TravelPlan(int id, string startLocation, string endLocation, DateTime startDate, DateTime endDate, Car selectedCar, List<Employee> selectedEmployees)
        {
            Id = id;
            StartLocation = startLocation;
            EndLocation = endLocation;
            StartDate = startDate;
            EndDate = endDate;
            SelectedCar = selectedCar;
            SelectedEmployees = selectedEmployees;
        }

        public int Id { get; set; }
        public string StartLocation { get; set; }
        public string EndLocation { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public List<Employee> SelectedEmployees { get; set; }
        public Car SelectedCar { get; set; }
    }
}
