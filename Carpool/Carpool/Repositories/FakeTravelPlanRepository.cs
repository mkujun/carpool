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

        public void SaveTravelPlan(TravelPlanDTO travelPlanDTO)
        {
            TravelPlan updatedTravelPlan = TravelPlans.Where(tp => tp.Id == travelPlanDTO.Id).FirstOrDefault();

            updatedTravelPlan.StartDate = travelPlanDTO.StartDate;
            updatedTravelPlan.EndDate = travelPlanDTO.EndDate;
            updatedTravelPlan.StartLocation = travelPlanDTO.StartLocation;
            updatedTravelPlan.EndLocation = travelPlanDTO.EndLocation;
            updatedTravelPlan.SelectedCar = travelPlanDTO.SelectedCar;
            updatedTravelPlan.SelectedEmployees = travelPlanDTO.SelectedEmployees;
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

        public TravelPlanDTO MapTravelPlanToDTO(TravelPlan travelPlan, ICarRepository carRepository)
        {
            TravelPlanDTO travelPlanDTO = new TravelPlanDTO();

            travelPlanDTO.ListOfCars = carRepository.Cars.ToList();
            travelPlanDTO.Id = travelPlan.Id;
            travelPlanDTO.StartDate = travelPlan.StartDate;
            travelPlanDTO.EndDate = travelPlan.EndDate;
            travelPlanDTO.StartLocation = travelPlan.StartLocation;
            travelPlanDTO.EndLocation = travelPlan.EndLocation;
            travelPlanDTO.SelectedCarPlates = travelPlan.SelectedCar.Plates;
            travelPlanDTO.SelectedCar = travelPlan.SelectedCar;

            return travelPlanDTO;
        }
    }
}
