using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Carpool.Models
{
    public class TravelPlanStatisticsViewModel
    {
        public TravelPlanStatisticsViewModel()
        {
            Months.Add(new SelectListItem("January", "1"));
            Months.Add(new SelectListItem("February ", "2"));
            Months.Add(new SelectListItem("March ", "3"));
            Months.Add(new SelectListItem("April ", "4"));
            Months.Add(new SelectListItem("May", "5"));
            Months.Add(new SelectListItem("June", "6"));
            Months.Add(new SelectListItem("July", "7"));
            Months.Add(new SelectListItem("August", "8"));
            Months.Add(new SelectListItem("September", "9"));
            Months.Add(new SelectListItem("October", "10"));
            Months.Add(new SelectListItem("November", "11"));
            Months.Add(new SelectListItem("December", "12"));
        }

        public List<TravelPlan> TravelPlans { get; set; }

        public List<SelectListItem> Months = new List<SelectListItem>();

        public int MonthId { get; set; }
    }
}
