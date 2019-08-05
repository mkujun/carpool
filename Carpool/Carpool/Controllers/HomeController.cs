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
            TravelPlanDTO travelPlanDTO = new TravelPlanDTO();
            travelPlanDTO.ListOfCars = carRepository.Cars.ToList();
            
            return View(travelPlanDTO);
        }

        [HttpPost]
        public IActionResult CreateTravelPlan(TravelPlanDTO travelPlanDTO)
        {
            bool isCarOnRide = travelPlanRepository.IsCarAlreadyOnTheRide(travelPlanDTO.SelectedCarPlates, travelPlanDTO.StartDate, travelPlanDTO.EndDate);

            if(isCarOnRide)
            {
                ModelState.AddModelError("CarOnTheRide", "");
            }

            if (ModelState.IsValid)
            {
                TravelPlan travelPlan = travelPlanRepository.MapDTOToTravelPlan(travelPlanDTO, carRepository); 
                travelPlanRepository.TravelPlans.Add(travelPlan);

                travelPlanDTO.Id = travelPlan.Id;

                return RedirectToAction("PickPassengers", travelPlanDTO);
            }

            else
            {
                travelPlanDTO.ListOfCars = carRepository.Cars.ToList();

                return View(travelPlanDTO);
            }
        }

        [HttpGet]
        public IActionResult EditTravelPlan(int id)
        {
            TravelPlan travelPlan = travelPlanRepository.GetTravelPlan(id);

            TravelPlanDTO travelPlanDTO = travelPlanRepository.MapTravelPlanToDTO(travelPlan, carRepository);

            return View(travelPlanDTO);
        }

        [HttpPost]
        public IActionResult EditTravelPlan(TravelPlanDTO travelPlan)
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
                travelPlan.ListOfCars = carRepository.Cars.ToList();
                travelPlan.SelectedCar = carRepository.GetCar(travelPlan.SelectedCarPlates);

                return View(travelPlan);
            }
        }

        [HttpGet]
        public IActionResult PickPassengers(int id)
        {
            TravelPlan travel = travelPlanRepository.GetTravelPlan(id);

            TravelPlanDTO travelPlanDTO = travelPlanRepository.MapTravelPlanToDTO(travel, carRepository);

            if (travelPlanDTO != null)
            {
                travelPlanDTO.ListOfEmployees = employeeRepository.Employees.ToList();
                travelPlanDTO.SelectedEmployees = travelPlanRepository.GetSelectedEmployees(travel.Id);
                travelPlanDTO.SelectedCarPlates = travel.SelectedCar.Plates;
            }

            return View(travelPlanDTO);
        }

        public RedirectToActionResult DeleteTravelPlan(int id)
        {
            travelPlanRepository.DeleteTravelPlan(id);

            return RedirectToAction("Carpools");
        }

        [HttpPost]
        public IActionResult SaveRide([FromBody] TravelPlanDTO data)
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
            travelPlanStatisticsViewModel.Cars = carRepository.Cars.ToList();

            return View(travelPlanStatisticsViewModel);
        }

        [HttpPost]
        public IActionResult CarpoolStatistics(TravelPlanStatisticsViewModel data)
        {
            data.Cars = carRepository.Cars.ToList();
            data.TravelPlans = travelPlanRepository.GetTravelPlansForMonth(data.MonthId, data.SelectedCarPlates);

            return View(data);
        }

    }
}
