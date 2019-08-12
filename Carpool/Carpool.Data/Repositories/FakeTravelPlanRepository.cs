using Carpool.Domain.Interfaces;
using Carpool.Domain.Models;
using System.Collections.Generic;

namespace Carpool.Repositories
{
    public class FakeTravelPlanRepository : ITravelPlanRepository
    {
        public List<TravelPlan> TravelPlans;

        public FakeTravelPlanRepository()
        {
            TravelPlans = new List<TravelPlan>();            
        }

        public List<TravelPlan> GetTravelPlans()
        {
            return TravelPlans;
        }
    }
}
