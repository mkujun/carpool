using Carpool.Interfaces;
using Carpool.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Carpool.Repositories
{
    public class FakeTravelPlanRepository : ITravelPlanRepository
    {
        public List<TravelPlan> TravelPlans { get; set; }

        public FakeTravelPlanRepository()
        {
            TravelPlans = new List<TravelPlan>();
            List<Employee> selectedEmployees = new List<Employee>();
            selectedEmployees.Add(new Employee(3, "Johnny Cash", true));
            selectedEmployees.Add(new Employee(4, "David Bowie", false));

            TravelPlans.Add(new TravelPlan(1,"Rijeka", "Zagreb", DateTime.Now, DateTime.Today, selectedEmployees));
            TravelPlans.Add(new TravelPlan(2,"Crikvenica", "Zagreb", DateTime.Now, DateTime.Today, selectedEmployees));
        }

        public bool IsCarAlreadyOnTheRide(string licensePlates, DateTime startDate, DateTime endDate)
        {
            if (TravelPlans != null)
            {
                List<TravelPlan> carOnTheRoad = TravelPlans.Where(c => c.SelectedCarPlates == licensePlates).ToList(); 

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

        public void SaveTravelPlan(TravelPlan travelPlan)
        {
            /*
            // todo : finish this adding, find if that car sharing ride is possible...
            TravelPlans.Add(new TravelPlan(
                travelPlan.StartLocation,
                travelPlan.EndLocation,
                travelPlan.StartDate,
                travelPlan.EndDate
                ));
            */
        }

        public void DeleteTravelPlan(int travelPlanId)
        {
            TravelPlan travelPlanForDelete = TravelPlans.Where(tp => tp.Id == travelPlanId).FirstOrDefault(); 

            if (travelPlanForDelete != null)
            {
                TravelPlans.Remove(travelPlanForDelete);
            }
        }
    }
}
