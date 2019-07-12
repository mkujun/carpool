using Carpool.Interfaces;
using Carpool.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Carpool.Repositories
{
    public class FakeCarSharingRepository : ICarSharingRepository
    {
        public IEnumerable<CarSharing> CarSharings => throw new NotImplementedException();

        public void SaveCarSharing(CarSharing carSharing)
        {
            // todo : finish this adding, find if that car sharing ride is possible...
            throw new NotImplementedException();
        }
    }
}
