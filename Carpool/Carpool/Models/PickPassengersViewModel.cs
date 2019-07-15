using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Carpool.Models
{
    public class PickPassengersViewModel
    {
        public PickPassengersViewModel()
        {

        }

        public PickPassengersViewModel( string startLocation, string endLocation, DateTime? startDate, DateTime? endDate, string selectedCarPlates)
        {
            StartLocation = startLocation;
            EndLocation = endLocation;
            StartDate = startDate;
            EndDate = endDate;
            SelectedCarPlates = selectedCarPlates;
        }

        public string StartLocation { get; set; }
        public string EndLocation { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        public string SelectedCarPlates { get; set; }
        public int SelectedEmployeesId { get; set; }
        public List<Employee> ListOfEmployees { get; set; }
    }
}
