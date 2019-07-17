using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Carpool.Models
{
    public class EditTravelPlanViewModel : IValidatableObject
    {
        public EditTravelPlanViewModel()
        {

        }

        public EditTravelPlanViewModel(int id, string startLocation, string endLocation, DateTime startDate, DateTime endDate, string selectedCarPlates)
        {
            Id = id;
            StartLocation = startLocation;
            EndLocation = endLocation;
            StartDate = startDate;
            EndDate = endDate;
            SelectedCarPlates = selectedCarPlates;
        }

        public int Id { get; set; }

        [Required(ErrorMessage = "Start location is required.", AllowEmptyStrings = false)]
        public string StartLocation { get; set; }

        [Required(ErrorMessage = "End location is required.", AllowEmptyStrings = false)]
        public string EndLocation { get; set; }

        [Required(ErrorMessage = "The start date is required.")]
        public DateTime StartDate { get; set; }

        [Required(ErrorMessage = "The end date is required.")]
        public DateTime EndDate { get; set; }

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
