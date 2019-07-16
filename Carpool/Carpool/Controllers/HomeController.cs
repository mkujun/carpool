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
            return View();
        }

        [HttpGet]
        public IActionResult CreateTravelPlan()
        {
            CreateTravelPlanViewModel createCarRideViewModel = new CreateTravelPlanViewModel();

            createCarRideViewModel.ListOfCars = new List<Car>();
            createCarRideViewModel.ListOfCars = carRepository.Cars.ToList();

            return View(createCarRideViewModel);
        }

        [HttpPost]
        public IActionResult CreateTravelPlan(CreateTravelPlanViewModel createCarRideViewModel)
        {
            if (ModelState.IsValid)
            {
                PickPassengersViewModel pickPassengersViewModel = new PickPassengersViewModel(
                    createCarRideViewModel.StartLocation,
                    createCarRideViewModel.EndLocation,
                    createCarRideViewModel.StartDate,
                    createCarRideViewModel.EndDate,
                    createCarRideViewModel.SelectedCarPlates
                    );

                return RedirectToAction("PickPassengers", pickPassengersViewModel);
            }

            else
            {
                createCarRideViewModel.ListOfCars = carRepository.Cars.ToList();
                return View(createCarRideViewModel);
            }
        }

        [HttpGet]
        public IActionResult PickPassengers(PickPassengersViewModel pickPassengersViewModel)
        {
            pickPassengersViewModel.ListOfEmployees = employeeRepository.Employees.ToList();

            return View(pickPassengersViewModel);
        }

        [HttpPost]
        public IActionResult SaveRide([FromBody] TravelPlan data)
        {
            // todo : make validation
            // validate number of employees in a car

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
            }

            if (travelPlanRepository.TravelPlans == null)
            {
                travelPlanRepository.TravelPlans = new List<TravelPlan>();
                travelPlanRepository.SaveTravelPlan(data);
            }

            else
            {
                travelPlanRepository.SaveTravelPlan(data);
            }

            return Json(data);
        }

    }
}
