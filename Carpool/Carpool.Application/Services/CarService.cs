using Carpool.Domain;
using Carpool.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Carpool.Application
{
    public class CarService : ICarService
    {
        private ICarRepository _carRepository;

        public CarService(ICarRepository carRepository)
        {
            _carRepository = carRepository;
        }

        public IEnumerable<Car> Cars => _carRepository.GetCars();

        public bool CanFitIntoACar(string licensePlates, List<Employee> selectedEmployees)
        {
            Car car = GetCar(licensePlates);

            if(car != null)
            {
                if(car.NumberOfSeats < selectedEmployees.Count)
                {
                    return false;
                }

                return true;
            }

            return false;
        }

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
