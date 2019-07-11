using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Carpool.Models
{
    public class CarSharing
    {
        public CarSharing(string startLocation, string endLocation, DateTime startDate, DateTime endDate, Car car, ICollection<Employee> employees)
        {
            StartLocation = startLocation;
            EndLocation = endLocation;
            StartDate = startDate;
            EndDate = endDate;
            Car = car;
            Employees = employees;
        }

        public string StartLocation { get; set; }
        public string EndLocation { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public Car Car { get; set; }
        public virtual ICollection<Employee> Employees { get; set; }
    }
}
