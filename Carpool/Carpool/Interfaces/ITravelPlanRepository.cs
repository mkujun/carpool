using Carpool.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Carpool.Interfaces
{
    public interface ITravelPlanRepository
    {
        //IEnumerable<CarSharing> CarSharings { get; set; }
        List<TravelPlan> TravelPlans { get; set; }
        void SaveTravelPlan(TravelPlan carSharing);
        void DeleteTravelPlan(int travelPlanId);
    }
}
