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
            CreateTravelPlanViewModel createTravelPlanViewModel = new CreateTravelPlanViewModel();
            createTravelPlanViewModel.ListOfCars = new List<Car>();
            createTravelPlanViewModel.ListOfCars = carRepository.Cars.ToList();

            return View(createTravelPlanViewModel);
        }

        [HttpPost]
        public IActionResult CreateTravelPlan(CreateTravelPlanViewModel createTravelPlanViewModel)
        {
            if (ModelState.IsValid)
            {
                PickPassengersViewModel pickPassengersViewModel = new PickPassengersViewModel(
                    createTravelPlanViewModel.StartLocation,
                    createTravelPlanViewModel.EndLocation,
                    createTravelPlanViewModel.StartDate,
                    createTravelPlanViewModel.EndDate,
                    createTravelPlanViewModel.SelectedCarPlates
                    );

                return RedirectToAction("PickPassengers", pickPassengersViewModel);
            }

            else
            {
                createTravelPlanViewModel.ListOfCars = carRepository.Cars.ToList();

                return View(createTravelPlanViewModel);
            }
        }

        [HttpGet]
        public IActionResult EditTravelPlan(int id)
        {
            TravelPlan travelPlan = travelPlanRepository.GetTravelPlan(id);

            // todo : rename this to "TravelPlanViewModel"...
            EditTravelPlanViewModel editTravelPlanViewModel = new EditTravelPlanViewModel(
                travelPlan.Id,
                travelPlan.StartLocation,
                travelPlan.EndLocation,
                travelPlan.StartDate,
                travelPlan.EndDate,
                travelPlan.SelectedCarPlates
                );

            editTravelPlanViewModel.ListOfCars = carRepository.Cars.ToList();

            return View(editTravelPlanViewModel);
        }

        [HttpPost]
        public IActionResult EditTravelPlan(EditTravelPlanViewModel editTravelPlanViewModel)
        {
            if (ModelState.IsValid)
            {
                TravelPlan travelPlan = travelPlanRepository.GetTravelPlan(editTravelPlanViewModel.Id);

                travelPlan.StartLocation = editTravelPlanViewModel.StartLocation;
                travelPlan.EndLocation = editTravelPlanViewModel.EndLocation;
                travelPlan.StartDate = editTravelPlanViewModel.StartDate;
                travelPlan.EndDate = editTravelPlanViewModel.EndDate;

                travelPlanRepository.EditTravelPlan(travelPlan);

                return RedirectToAction("Carpools");
            }

            else
            {
                editTravelPlanViewModel.ListOfCars = carRepository.Cars.ToList();

                return View(editTravelPlanViewModel);
            }
        }

        [HttpGet]
        public IActionResult PickPassengers(PickPassengersViewModel pickPassengersViewModel)
        {
            pickPassengersViewModel.ListOfEmployees = employeeRepository.Employees.ToList();

            return View(pickPassengersViewModel);
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

            // if travel plans are empty(null) 
            if (travelPlanRepository.TravelPlans == null)
            {
                travelPlanRepository.TravelPlans = new List<TravelPlan>();

                data.SelectedEmployees = employeeRepository.GetEmployeesByIds(data.ListOfPassengersIds);

                travelPlanRepository.SaveTravelPlan(data);
            }

            else
            {
                data.SelectedEmployees = employeeRepository.GetEmployeesByIds(data.ListOfPassengersIds);

                travelPlanRepository.SaveTravelPlan(data);

                //return RedirectToAction("Carpools");
                return Json(data);
            }

            return Json(data);
        }

    }
}
