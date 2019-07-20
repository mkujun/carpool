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
            selectedEmployees.Add(new Employee(1, "John Lennon", true));

            Car selectedCar = new Car("Clio", "Renault Clio", "Yellow", "ZG 456-PD", 4);

            TravelPlans.Add(new TravelPlan(1,"Rijeka", "Zagreb", DateTime.Now, DateTime.Today, selectedCar, selectedEmployees));
            TravelPlans.Add(new TravelPlan(2,"Crikvenica", "Zagreb", DateTime.Now, DateTime.Today, selectedCar, selectedEmployees));
            TravelPlans.Add(new TravelPlan(3,"Liverpool", "Rijeka", DateTime.Now, DateTime.Today, selectedCar, selectedEmployees));
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
            TravelPlan updatedTravelPlan = TravelPlans.Where(tp => tp.Id == travelPlan.Id).FirstOrDefault();

            updatedTravelPlan.StartDate = travelPlan.StartDate;
            updatedTravelPlan.EndDate = travelPlan.EndDate;
            updatedTravelPlan.StartLocation = travelPlan.StartLocation;
            updatedTravelPlan.EndLocation = travelPlan.EndLocation;
            updatedTravelPlan.SelectedCar = travelPlan.SelectedCar;
            updatedTravelPlan.SelectedEmployees = travelPlan.SelectedEmployees;
        }

        public void DeleteTravelPlan(int travelPlanId)
        {
            TravelPlan travelPlanForDelete = TravelPlans.Where(tp => tp.Id == travelPlanId).FirstOrDefault(); 

            if (travelPlanForDelete != null)
            {
                TravelPlans.Remove(travelPlanForDelete);
            }
        }

        public TravelPlan GetTravelPlan(int travelPlanId)
        {
            TravelPlan travelPlan = TravelPlans.Where(tp => tp.Id == travelPlanId).FirstOrDefault(); 

            if (travelPlan != null)
            {
                return travelPlan;
            }

            else
            {
                return null;
            }
        }

        public List<Employee> GetSelectedEmployees(int travelPlanId)
        {
            TravelPlan travelPlan = TravelPlans.Where(tp => tp.Id == travelPlanId).FirstOrDefault(); 

            if (travelPlan != null)
            {
                return travelPlan.SelectedEmployees;
            }

            else
            {
                return null;
            }

        }
    }
}
