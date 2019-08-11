using Carpool.Domain;
using Carpool.Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Carpool.Application
{
    public class TravelPlanDTO : IValidatableObject
    {
        public int Id { get; set; } 

        [Required(ErrorMessage = "Start location is required.", AllowEmptyStrings = false)]
        public string StartLocation { get; set; }

        [Required(ErrorMessage = "End location is required.", AllowEmptyStrings = false)]
        public string EndLocation { get; set; }

        [Required(ErrorMessage = "The start date is required.")]
        public DateTime StartDate { get; set; }

        [Required(ErrorMessage = "The end date is required.")]
        public DateTime EndDate { get; set; }

        public int[] ListOfPassengersIds { get; set; }
        public int SelectedEmployeesId { get; set; }
        public string Error { get; set; }

        public List<Employee> ListOfEmployees { get; set; }
        public List<Employee> SelectedEmployees { get; set; }
        public Car SelectedCar { get; set; }
        public string SelectedCarPlates { get; set; }
        public List<Car> ListOfCars { get; set; }
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
