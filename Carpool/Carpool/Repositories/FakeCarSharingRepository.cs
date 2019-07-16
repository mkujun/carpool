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

        public FakeCarSharingRepository()
        {
            CarSharings.Add(new CarSharing("Rijeka", "Zagreb", DateTime.Now, DateTime.Today));
            CarSharings.Add(new CarSharing("Crikvenica", "Zagreb", DateTime.Now, DateTime.Today));
        }

        public bool IsCarAlreadyOnTheRide(string licensePlates, DateTime startDate, DateTime endDate)
        {
            if (CarSharings != null)
            {
                List<CarSharing> carOnTheRoad = CarSharings.Where(c => c.SelectedCarPlates == licensePlates).ToList(); 

                return true;
            }
            else
            {
                return false;
            }

            /*
            if(carAlreadyOnTheRoad)
            {
                return true;
            }

            else
            {
                return false;
            }
            */
        }

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
