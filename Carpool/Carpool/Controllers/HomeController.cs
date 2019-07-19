﻿using System;
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
            travelPlan.ListOfCars = new List<Car>();
            travelPlan.ListOfCars = carRepository.Cars.ToList();

            return View(travelPlan);
        }

        [HttpPost]
        public IActionResult CreateTravelPlan(TravelPlan travelPlan)
        {
            if (ModelState.IsValid)
            {
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
            travelPlan.SelectedEmployees = travelPlan.SelectedEmployees;

            return View(travelPlan);
        }

        [HttpPost]
        public IActionResult EditTravelPlan(TravelPlan travelPlan)
        {
            if (ModelState.IsValid)
            {
                travelPlanRepository.EditTravelPlan(travelPlan);

                return RedirectToAction("Carpools");
            }

            else
            {
                travelPlan.ListOfCars = carRepository.Cars.ToList();

                return View(travelPlan);
            }
        }

        [HttpGet]
        public IActionResult PickPassengers(TravelPlan pickPassengersViewModel)
        {
            TravelPlan travelPlan = travelPlanRepository.GetTravelPlan(pickPassengersViewModel.Id);

            if (travelPlan != null)
            {
                pickPassengersViewModel.StartLocation = travelPlan.StartLocation;
                pickPassengersViewModel.EndLocation = travelPlan.EndLocation;
                pickPassengersViewModel.StartDate = travelPlan.StartDate;
                pickPassengersViewModel.EndDate = travelPlan.EndDate;
                pickPassengersViewModel.SelectedCarPlates = travelPlan.SelectedCarPlates;

                pickPassengersViewModel.ListOfEmployees = employeeRepository.Employees.ToList();
                pickPassengersViewModel.PassangersAddedOnRide = travelPlan.SelectedEmployees;
            }
            else
            {

                pickPassengersViewModel.ListOfEmployees = employeeRepository.Employees.ToList();
                //pickPassengersViewModel.PassangersAddedOnRide = travelPlan.SelectedEmployees;
            }

            return View(pickPassengersViewModel);
        }

        public IActionResult EditPassengers(int id)
        {
            TravelPlan travelPlan = travelPlanRepository.GetTravelPlan(id);

            return RedirectToAction("PickPassengers", travelPlan);
        }

        public RedirectToActionResult DeleteTravelPlan(int id)
        {
            travelPlanRepository.DeleteTravelPlan(id);

            return RedirectToAction("Carpools");
        }

        [HttpPost]
        public IActionResult SaveRide([FromBody] TravelPlan data)
        {
            // todo : if travel plan exists modify ti
            // this only adds new one 

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

            if (travelPlanRepository.TravelPlans.Where(tp => tp.Id == data.Id).FirstOrDefault() != null)
            {
                data.SelectedEmployees = employeeRepository.GetEmployeesByIds(data.ListOfPassengersIds);

                // todo : should not go into save!! should go into edit!!! refactor this!!!!

                travelPlanRepository.EditTravelPlan(data);

                //return RedirectToAction("Carpools");
                return Json(data);
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
