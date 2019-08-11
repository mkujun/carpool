using Carpool.Domain;
using Carpool.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Carpool.Application
{
    public interface ICarService
    {
        IEnumerable<Car> Cars { get; }
        Car GetCar(string licensePlates);
        bool CanFitIntoACar(string licensePlates, List<Employee> selectedEmployees);
        bool CanFitIntoACar(string licensePlates, int[] employeesIds);
    }
}
