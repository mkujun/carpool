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
        private ICarSharingRepository carSharingRepository;

        public HomeController(IEmployeeRepository employeeRepo, ICarRepository carRepo, ICarSharingRepository carSharingRepo)
        {
            employeeRepository = employeeRepo;
            carRepository = carRepo;
            carSharingRepository = carSharingRepo;
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

        public IActionResult CarSharing()
        {
            return View();
        }

        [HttpGet]
        public IActionResult CreateCarRide()
        {
            CreateCarRideViewModel createCarRideViewModel = new CreateCarRideViewModel();

            createCarRideViewModel.ListOfCars = new List<Car>();
            createCarRideViewModel.ListOfCars = carRepository.Cars.ToList();

            return View(createCarRideViewModel);
        }

        [HttpPost]
        public IActionResult CreateCarRide(CreateCarRideViewModel createCarRideViewModel)
        {
            // todo : validate createCarRideViewModel
            PickPassengersViewModel pickPassengersViewModel = new PickPassengersViewModel(
                createCarRideViewModel.StartLocation,
                createCarRideViewModel.EndLocation,
                createCarRideViewModel.StartDate,
                createCarRideViewModel.EndDate,
                createCarRideViewModel.SelectedCarPlates
                );

            return RedirectToAction("PickPassengers", pickPassengersViewModel);
        }

        [HttpGet]
        public IActionResult PickPassengers(PickPassengersViewModel pickPassengersViewModel)
        {
            pickPassengersViewModel.ListOfEmployees = employeeRepository.Employees.ToList();

            return View(pickPassengersViewModel);
        }

        [HttpPost]
        public IActionResult SaveRide([FromBody] CarSharing data)
        {
            // todo : make validation
            if (carSharingRepository.CarSharings == null)
            {
                carSharingRepository.CarSharings = new List<CarSharing>();
                carSharingRepository.SaveCarSharing(data);
            }
            else
            {
                carSharingRepository.SaveCarSharing(data);
            }

            return Json(data);
        }

    }
}
