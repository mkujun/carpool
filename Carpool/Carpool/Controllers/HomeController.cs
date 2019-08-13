using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Carpool.Models;
using Carpool.Application;
using Carpool.Domain.Models;

namespace Carpool.Controllers
{
    public class HomeController : Controller
    {
        private IEmployeeService _employeeService;
        private ICarService _carService;
        private ITravelPlanService _travelPlanService;

        public HomeController(IEmployeeService employeeService, ICarService carService, ITravelPlanService travelPlanService)
        {
            _employeeService = employeeService;
            _carService = carService;
            _travelPlanService = travelPlanService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Employees()
        {
            IEnumerable<Employee> employees = _employeeService.Employees;

            return View(employees);
        }

        public IActionResult Cars()
        {
            IEnumerable<Car> cars = _carService.Cars;

            return View(cars);
        }

        public IActionResult Carpools()
        {
            List<TravelPlan> travelPlans = _travelPlanService.GetTravelPlans();

            return View(travelPlans);
        }

        [HttpGet]
        public IActionResult CreateTravelPlan()
        {
            TravelPlanDTO travelPlanDTO = new TravelPlanDTO();
            travelPlanDTO.ListOfCars = _carService.Cars.ToList();
            
            return View(travelPlanDTO);
        }

        [HttpPost]
        public IActionResult CreateTravelPlan(TravelPlanDTO travelPlanDTO)
        {
            bool isCarOnRide = _travelPlanService.IsCarAlreadyOnTheRide(travelPlanDTO.SelectedCarPlates, travelPlanDTO.StartDate, travelPlanDTO.EndDate);

            if(isCarOnRide)
            {
                ModelState.AddModelError("CarOnTheRide", "");
            }

            if (ModelState.IsValid)
            {
                TravelPlan travelPlan = _travelPlanService.MapDTOToTravelPlan(travelPlanDTO, _carService);
                
                List<TravelPlan> travelPlans = _travelPlanService.GetTravelPlans();
                travelPlans.Add(travelPlan);

                travelPlanDTO.Id = travelPlan.Id;

                return RedirectToAction("PickPassengers", travelPlanDTO);
            }

            else
            {
                travelPlanDTO.ListOfCars = _carService.Cars.ToList();

                return View(travelPlanDTO);
            }
        }

        [HttpGet]
        public IActionResult EditTravelPlan(int id)
        {
            TravelPlan travelPlan = _travelPlanService.GetTravelPlan(id);

            TravelPlanDTO travelPlanDTO = _travelPlanService.MapTravelPlanToDTO(travelPlan, _carService);

            return View(travelPlanDTO);
        }

        [HttpPost]
        public IActionResult EditTravelPlan(TravelPlanDTO travelPlan)
        {
            TravelPlan travelPlanForEdit = _travelPlanService.GetTravelPlans().Where(p => p.Id == travelPlan.Id).FirstOrDefault();

            bool canFitIntoACar = _carService.CanFitIntoACar(travelPlan.SelectedCarPlates, travelPlanForEdit.SelectedEmployees);

            if(!canFitIntoACar)
            {
                ModelState.AddModelError("CarIsFull", "");
            }

            if (ModelState.IsValid)
            {
                travelPlan.SelectedEmployees = _travelPlanService.GetSelectedEmployees(travelPlan.Id);
                travelPlan.SelectedCar = _carService.GetCar(travelPlan.SelectedCarPlates);
                
                _travelPlanService.SaveTravelPlan(travelPlan);

                return RedirectToAction("Carpools");
            }

            else
            {
                travelPlan.ListOfCars = _carService.Cars.ToList();
                travelPlan.SelectedCar = _carService.GetCar(travelPlan.SelectedCarPlates);

                return View(travelPlan);
            }
        }

        [HttpGet]
        public IActionResult PickPassengers(int id)
        {
            TravelPlan travel = _travelPlanService.GetTravelPlan(id);

            TravelPlanDTO travelPlanDTO = _travelPlanService.MapTravelPlanToDTO(travel, _carService);

            if (travelPlanDTO != null)
            {
                travelPlanDTO.ListOfEmployees = _employeeService.Employees.ToList();
                travelPlanDTO.SelectedEmployees = _travelPlanService.GetSelectedEmployees(travel.Id);
                travelPlanDTO.SelectedCarPlates = travel.SelectedCar.Plates;
            }

            return View(travelPlanDTO);
        }

        public RedirectToActionResult DeleteTravelPlan(int id)
        {
            _travelPlanService.DeleteTravelPlan(id);

            return RedirectToAction("Carpools");
        }

        [HttpPost]
        public IActionResult SaveRide([FromBody] TravelPlanDTO data)
        {
            bool employeeAlreadyOnTheRoad = false;

            foreach (var employee in data.ListOfPassengersIds)
            {
                employeeAlreadyOnTheRoad = _travelPlanService.IsEmployeeOnTheRide(data.StartDate, data.EndDate, employee);
            }

            if (employeeAlreadyOnTheRoad)
            {
                data.Error = "One or more selected employees are already on the ride";
                return Json(data);
            }

            bool hasLicense = _employeeService.HasDriverLicense(data.ListOfPassengersIds);

            if (!hasLicense)
            {
                data.Error = "None of the empoloyees has a license";
                return Json(data);
            }

            bool canFitIntoACar = _carService.CanFitIntoACar(data.SelectedCarPlates, data.ListOfPassengersIds);

            if (!canFitIntoACar)
            {
                data.Error = "Too many employees in a car!";
                return Json(data);
            }

            if (_travelPlanService.GetTravelPlans().Where(tp => tp.Id == data.Id).FirstOrDefault() != null)
            {
                data.SelectedEmployees = _employeeService.GetEmployeesByIds(data.ListOfPassengersIds);
                data.SelectedCar = _carService.GetCar(data.SelectedCarPlates);

                _travelPlanService.SaveTravelPlan(data);

                return Json(data);
            }

            return Json(data);
        }

        public IActionResult CarpoolStatistics()
        {
            TravelPlanStatisticsViewModel travelPlanStatisticsViewModel = new TravelPlanStatisticsViewModel();
            travelPlanStatisticsViewModel.Cars = _carService.Cars.ToList();

            return View(travelPlanStatisticsViewModel);
        }

        [HttpPost]
        public IActionResult CarpoolStatistics(TravelPlanStatisticsViewModel data)
        {
            data.Cars = _carService.Cars.ToList();
            data.TravelPlans = _travelPlanService.GetTravelPlansForMonth(data.MonthId, data.SelectedCarPlates);

            return View(data);
        }

    }
}
