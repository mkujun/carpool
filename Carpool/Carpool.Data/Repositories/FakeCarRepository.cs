using Carpool.Domain;
using Carpool.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Carpool.Data
{
    public class FakeCarRepository : ICarRepository
    {
        // ef context goes here for more complex requirements...

        public IEnumerable<Car> Cars => new List<Car>
        {
            new Car { Name = "Mustang", CarType = "Ford Mustang", Color = "Red", NumberOfSeats = 2, Plates = "RI 123-AB" },
            new Car { Name = "Green Skoda", CarType = "Skoda Octavia", Color = "Green", NumberOfSeats = 4, Plates = "RI 312-AC" },
            new Car { Name = "Clio", CarType = "Renault Clio", Color = "Yellow", NumberOfSeats = 4, Plates = "ZG 456-PD" },
            new Car { Name = "Impala", CarType = "Chevrolet Impala", Color = "Black", NumberOfSeats = 4, Plates = "ZG 256-PA" },
            new Car { Name = "Taxi Peugeot 406", CarType = "Peugeot 406", Color = "White", NumberOfSeats = 4, Plates = "ZD 786-YE" },
            new Car { Name = "Fico", CarType = "Fiat 500", Color = "Flourescent", NumberOfSeats = 4, Plates = "ZD 106-BE" },
            new Car { Name = "Peglica", CarType = "Fiat 124", Color = "Purple", NumberOfSeats = 2, Plates = "ST 126-BE" },
            new Car { Name = "Passat", CarType = "Volkswagen Passat", Color = "Blue", NumberOfSeats = 6, Plates = "DU 826-BE" },
            new Car { Name = "Golf", CarType = "Volkswagen Golf", Color = "Orange", NumberOfSeats = 4, Plates = "DU 126-CE" },
            new Car { Name = "Mini", CarType = "Mini Morris", Color = "Brown", NumberOfSeats = 2, Plates = "KA 126-CA" }
        };

        public IEnumerable<Car> GetCars()
        {
            return Cars;
        }
    }
}
