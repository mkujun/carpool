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
        void SaveTravelPlan(TravelPlanDTO travelPlanDTO);
        void DeleteTravelPlan(int travelPlanId);
        TravelPlan GetTravelPlan(int travelPlanId);
        List<Employee> GetSelectedEmployees(int travelPlanId);
        bool IsCarAlreadyOnTheRide(string licensePlates, DateTime startDate, DateTime endDate);
        List<TravelPlan> GetTravelPlansForMonth(int month, string licensePlates);
    }
}
