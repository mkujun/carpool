using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Carpool.Models
{
    public class CarSharingViewModel
    {
        public CarSharingViewModel()
        {

        }

        public string StartLocation { get; set; }
        public string EndLocation { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }


        public List<Car> ListOfCars { get; set; }
        public List<SelectListItem> ListOfEmployees { get; set; }


        public string SelectedCarPlates { get; set; }
        public IEnumerable<Employee> SelectedEmployees { get; set; }
    }
}
