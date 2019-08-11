using Carpool.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Carpool.Domain
{
    public interface ICarRepository
    {
        IEnumerable<Car> GetCars();
    }
}
