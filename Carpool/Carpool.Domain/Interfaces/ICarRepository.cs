using Carpool.Domain.Models;
using System.Collections.Generic;

namespace Carpool.Domain
{
    public interface ICarRepository
    {
        IEnumerable<Car> GetCars();
    }
}
