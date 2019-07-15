using Carpool.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Carpool.Interfaces
{
    public interface ICarRepository
    {
        IEnumerable<Car> Cars { get; }
        Car GetCar(string licensePlates);
    }
}
