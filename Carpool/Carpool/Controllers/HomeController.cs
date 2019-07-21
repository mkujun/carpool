using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Carpool.Models;
using Carpool.Interfaces;

namespace Carpool.Controllers
{
    public class HomeController : Controller
    {
        private IEmployeeRepository employeeRepository;
        private ICarRepository carRepository;
        private ITravelPlanRepository travelPlanRepository;

        public HomeController(IEmployeeRepository employeeRepo, ICarRepository carRepo, ITravelPlanRepository travelPlanRepo)
        {
            employeeRepository = employeeRepo;
            carRepository = carRepo;
            travelPlanRepository = travelPlanRepo;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Employees()
        {
            IEnumerable<Employee> employees = employeeRepository.Employees;

            return View(employees);
        }

        public IActionResult Cars()
        {
            IEnumerable<Car> cars = carRepository.Cars;

            return View(cars);
        }

        public IActionResult Carpools()
        {
            List<TravelPlan> travelPlans = travelPlanRepository.TravelPlans;

            return View(travelPlans);
        }

        [HttpGet]
        public IActionResult CreateTravelPlan()
        {
            TravelPlan travelPlan = new TravelPlan();

            travelPlan.ListOfCars = carRepository.Cars.ToList();

            return View(travelPlan);
        }

        [HttpPost]
        public IActionResult CreateTravelPlan(TravelPlan travelPlan)
        {
            bool isCarOnRide = travelPlanRepository.IsCarAlreadyOnTheRide(travelPlan.SelectedCarPlates, travelPlan.StartDate, travelPlan.EndDate);

            if(isCarOnRide)
            {
                ModelState.AddModelError("CarOnTheRide", "");
            }

            if (ModelState.IsValid)
            {
                if (travelPlanRepository.TravelPlans.Count == 0)
                {
                    travelPlan.Id = 1;
                }
                else
                {
                    travelPlan.Id = travelPlanRepository.TravelPlans.Last().Id + 1;
                }

                travelPlan.SelectedCar = carRepository.GetCar(travelPlan.SelectedCarPlates);

                travelPlanRepository.TravelPlans.Add(travelPlan);

                return RedirectToAction("PickPassengers", travelPlan);
            }

            else
            {
                travelPlan.ListOfCars = carRepository.Cars.ToList();

                return View(travelPlan);
            }
        }

        [HttpGet]
        public IActionResult EditTravelPlan(int id)
        {
            TravelPlan travelPlan = travelPlanRepository.GetTravelPlan(id);

            travelPlan.ListOfCars = carRepository.Cars.ToList();

            return View(travelPlan);
        }

        [HttpPost]
        public IActionResult EditTravelPlan(TravelPlan travelPlan)
        {
            TravelPlan travelPlanForEdit = travelPlanRepository.TravelPlans.Where(p => p.Id == travelPlan.Id).FirstOrDefault();

            bool canFitIntoACar = carRepository.CanFitIntoACar(travelPlan.SelectedCarPlates, travelPlanForEdit.SelectedEmployees);

            if(!canFitIntoACar)
            {
                ModelState.AddModelError("CarIsFull", "");
            }

            if (ModelState.IsValid)
            {
                travelPlan.SelectedEmployees = travelPlanRepository.GetSelectedEmployees(travelPlan.Id);
                travelPlan.SelectedCar = carRepository.GetCar(travelPlan.SelectedCarPlates);
                travelPlanRepository.SaveTravelPlan(travelPlan);

                return RedirectToAction("Carpools");
            }

            else
            {
                TravelPlan plan = travelPlanRepository.TravelPlans.Where(p => p.Id == travelPlan.Id).FirstOrDefault();

                plan.SelectedCarPlates = plan.SelectedCar.Plates;

                return View(plan);
            }
        }

        [HttpGet]
        public IActionResult PickPassengers(int id)
        {
            TravelPlan travel = travelPlanRepository.GetTravelPlan(id);

            if (travel != null)
            {
                travel.ListOfEmployees = employeeRepository.Employees.ToList();
                travel.SelectedEmployees = travelPlanRepository.GetSelectedEmployees(travel.Id);
                travel.SelectedCarPlates = travel.SelectedCar.Plates;
            }

            return View(travel);
        }

        public RedirectToActionResult DeleteTravelPlan(int id)
        {
            travelPlanRepository.DeleteTravelPlan(id);

            return RedirectToAction("Carpools");
        }

        [HttpPost]
        public IActionResult SaveRide([FromBody] TravelPlan data)
        {
            bool hasLicense = employeeRepository.HasDriverLicense(data.ListOfPassengersIds);

            if (!hasLicense)
            {
                data.Error = "None of the empoloyees has a license";
                return Json(data);
            }

            bool canFitIntoACar = carRepository.CanFitIntoACar(data.SelectedCarPlates, data.ListOfPassengersIds);

            if (!canFitIntoACar)
            {
                data.Error = "Too many employees in a car!";
                return Json(data);
            }

            // if travel plan already exists
            if (travelPlanRepository.TravelPlans.Where(tp => tp.Id == data.Id).FirstOrDefault() != null)
            {
                data.SelectedEmployees = employeeRepository.GetEmployeesByIds(data.ListOfPassengersIds);
                data.SelectedCar = carRepository.GetCar(data.SelectedCarPlates);

                travelPlanRepository.SaveTravelPlan(data);

                return Json(data);
            }

            return Json(data);
        }

        public IActionResult CarpoolStatistics()
        {
            TravelPlanStatisticsViewModel travelPlanStatisticsViewModel = new TravelPlanStatisticsViewModel();

            return View(travelPlanStatisticsViewModel);
        }

        [HttpPost]
        public IActionResult CarpoolStatistics([FromBody] TravelPlanStatisticsViewModel data)
        {
            //travelPlanStatisticsViewModel.TravelPlans = travelPlanRepository.TravelPlans;
            data.TravelPlans = travelPlanRepository.GetTravelPlansForMonth(data.MonthId);

            return Json(data);
        }

    }
}
