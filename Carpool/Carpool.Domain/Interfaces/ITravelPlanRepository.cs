﻿using Carpool.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Carpool.Domain.Interfaces
{
    public interface ITravelPlanRepository
    {
        List<TravelPlan> GetTravelPlans();
    }
}