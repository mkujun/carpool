using Carpool.Domain.Models;
using System.Collections.Generic;

namespace Carpool.Domain.Interfaces
{
    public interface ITravelPlanRepository
    {
        List<TravelPlan> GetTravelPlans();
    }
}
