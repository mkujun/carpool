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
        public List<CarSharing> CarSharings { get; set; }

        public void SaveCarSharing(CarSharing carSharing)
        {
            // todo : finish this adding, find if that car sharing ride is possible...
            CarSharings.Add(new CarSharing(
                carSharing.StartLocation,
                carSharing.EndLocation,
                carSharing.StartDate,
                carSharing.EndDate
                ));
        }

    }
}
