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
        }

        public bool IsCarAlreadyOnTheRide(string licensePlates, DateTime startDate, DateTime endDate)
        {
            List<TravelPlan> travelPlansWithSelectedCar = new List<TravelPlan>();

            foreach (var travelPlan in TravelPlans)
            {
                if(travelPlan.SelectedCar.Plates == licensePlates)
                {
                    travelPlansWithSelectedCar.Add(travelPlan);
                }
            }

            foreach (var travelPlan in travelPlansWithSelectedCar)
            {
                if (startDate < travelPlan.EndDate && travelPlan.StartDate < endDate)
                {
                    return true;
                }
            }

            return false;
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

        public List<TravelPlan> GetTravelPlansForMonth(int month, string selectedCarPlates)
        {
            if (selectedCarPlates != null)
            {
                List<TravelPlan> travelPlans = TravelPlans.Where(tp => tp.StartDate.Month == month && tp.SelectedCar.Plates == selectedCarPlates).OrderBy(tp => tp.StartDate).ToList();

                if(travelPlans != null)
                {
                    return travelPlans;
                }
                else
                {
                    return null;
                }

            }

            else
            {
                List<TravelPlan> travelPlans = TravelPlans.Where(tp => tp.StartDate.Month == month).OrderBy(tp => tp.SelectedCar.Name).ToList();

                if(travelPlans != null)
                {
                    return travelPlans;
                }
                else
                {
                    return null;
                }
            }
        }
    }
}
