using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Carpool.Models
{
    public class Car
    {
        public string Name { get; set; }
        public string CarType { get; set; }
        public string Color { get; set; }
        public string Plates { get; set; }
        public int NumberOfSeats { get; set; }
    }
}
