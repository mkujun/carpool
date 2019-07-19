using Carpool.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Carpool.Interfaces
{
    public interface ITravelPlanRepository
    {
        List<TravelPlan> TravelPlans { get; set; }
        void SaveTravelPlan(TravelPlan travelPlan);
        void EditTravelPlan(TravelPlan travelPlan);
        void DeleteTravelPlan(int travelPlanId);
        TravelPlan GetTravelPlan(int travelPlanId);
    }
}
