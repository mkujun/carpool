using Carpool.Domain.Models;
using System.Collections.Generic;

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
