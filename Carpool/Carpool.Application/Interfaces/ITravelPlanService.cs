using Carpool.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Carpool.Application
{
    public interface ITravelPlanService
    {
        void SaveTravelPlan(TravelPlanDTO travelPlanDTO);
        void DeleteTravelPlan(int travelPlanId);
        TravelPlan GetTravelPlan(int travelPlanId);
        List<Employee> GetSelectedEmployees(int travelPlanId);
        bool IsCarAlreadyOnTheRide(string licensePlates, DateTime startDate, DateTime endDate);
        List<TravelPlan> GetTravelPlansForMonth(int month, string licensePlates);
        List<TravelPlan> GetTravelPlans();
        TravelPlanDTO MapTravelPlanToDTO(TravelPlan travelPlan, ICarService carRepository);
        TravelPlan MapDTOToTravelPlan(TravelPlanDTO travelPlanDTO, ICarService carRepository);
    }
}
