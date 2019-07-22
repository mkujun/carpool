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

            Car clio = new Car("Clio", "Renault Clio", "Yellow", "ZG 456-PD", 4);
            Car skoda = new Car( "Green Skoda", "Skoda Octavia", "Green", "RI 312-AC" , 4);

            TravelPlans.Add(new TravelPlan(1,"Rijeka", "Zagreb", new DateTime(2019, 7, 1), new DateTime(2019, 7, 10), clio, selectedEmployees));
            TravelPlans.Add(new TravelPlan(2,"Crikvenica", "Zagreb", DateTime.Now, DateTime.Today, skoda, selectedEmployees));
            TravelPlans.Add(new TravelPlan(3,"Liverpool", "Rijeka", DateTime.Now, DateTime.Today, clio, selectedEmployees));
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

        public List<TravelPlan> GetTravelPlansForMonth(int month)
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
