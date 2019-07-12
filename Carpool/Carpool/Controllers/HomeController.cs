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

        public HomeController(IEmployeeRepository employeeRepo, ICarRepository carRepo)
        {
            employeeRepository = employeeRepo;
            carRepository = carRepo;
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
            // todo : preload cars and employees from repository
            CarSharing carSharing = new CarSharing(carRepository.Cars, employeeRepository.Employees);

            return View(carSharing);
        }

        [HttpPost]
        public IActionResult CreateCarRide(CarSharing carSharing)
        {
            // todo : return "redirect to action" which will be the list of scheduled rides
            return View();
        }

    }
}
