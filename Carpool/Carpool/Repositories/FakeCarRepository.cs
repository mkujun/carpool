using Carpool.Interfaces;
using Carpool.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Carpool.Repositories
{
    public class FakeCarRepository : ICarRepository
    {
        public IEnumerable<Car> Cars => new List<Car>
        {
            new Car { Name = "Mustang", CarType = "Ford Mustang", Color = "Red", NumberOfSeats = 2, Plates = "RI 123-AB" },
            new Car { Name = "Green Skoda", CarType = "Skoda Octavia", Color = "Green", NumberOfSeats = 4, Plates = "RI 312-AC" }
        };
    }
}
