using Carpool.Domain.Interfaces;
using Carpool.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
