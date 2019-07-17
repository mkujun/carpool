﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Carpool.Models
{
    public class CreateTravelPlanViewModel : IValidatableObject
    {
        public CreateTravelPlanViewModel()
        {

        }

        public CreateTravelPlanViewModel(string startLocation, string endLocation, DateTime startDate, DateTime endDate, string selectedCarPlates)
        {
            StartLocation = startLocation;
            EndLocation = endLocation;
            StartDate = startDate;
            EndDate = endDate;
            SelectedCarPlates = selectedCarPlates;
        }

        [Required(ErrorMessage = "Start location is required.", AllowEmptyStrings = false)]
        public string StartLocation { get; set; }

        [Required(ErrorMessage = "End location is required.", AllowEmptyStrings = false)]
        public string EndLocation { get; set; }

        [Required(ErrorMessage = "The start date is required.")]
        public DateTime? StartDate { get; set; }

        [Required(ErrorMessage = "The end date is required.")]
        public DateTime? EndDate { get; set; }

        public List<Car> ListOfCars { get; set; }

        public string SelectedCarPlates { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (EndDate < StartDate)
            {
                yield return new ValidationResult("End date must be greater than starting date.", new List<string> { "EndDate" });
            }
            else
            {

            }
        }
    }
}
