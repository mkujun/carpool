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
            // todo : make data structure which will take a note of cars end employees
        }

        public string StartLocation { get; set; }
        public string EndLocation { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public virtual ICollection<Car> Car { get; set; }
        public virtual ICollection<Employee> Employee { get; set; }
    }
}
