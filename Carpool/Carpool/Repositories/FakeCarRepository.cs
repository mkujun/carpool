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
            new Car { Name = "Green Skoda", CarType = "Skoda Octavia", Color = "Green", NumberOfSeats = 4, Plates = "RI 312-AC" },
            new Car { Name = "Clio", CarType = "Renault Clio", Color = "Yellow", NumberOfSeats = 4, Plates = "ZG 456-PD" }
        };

        public bool CanFitIntoACar(string licensePlates, int[] employeesIds)
        {
            Car car = GetCar(licensePlates);

            if(car != null)
            {
                if(car.NumberOfSeats < employeesIds.Length)
                {
                    return false;
                }

                return true;
            }

            return false;
        }

        public Car GetCar(string licensePlates)
        {
            Car car = Cars.Where(c => c.Plates == licensePlates).FirstOrDefault();

            return car;
        }
    }
}
