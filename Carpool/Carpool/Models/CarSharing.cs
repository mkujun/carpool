using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Carpool.Models
{
    public class CarSharing
    {
        public CarSharing()
        {

        }

        public CarSharing(string startLocation, string endLocation, DateTime startDate, DateTime endDate)
        {
            StartLocation = startLocation;
            EndLocation = endLocation;
            StartDate = startDate;
            EndDate = endDate;
        }

        public string StartLocation { get; set; }
        public string EndLocation { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        //public IEnumerable<Employee> Employees { get; set; }
        public int[] ListOfPassengersIds { get; set; }
        public string SelectedCarPlates { get; set; }
        public string Error { get; set; }
    }
}
