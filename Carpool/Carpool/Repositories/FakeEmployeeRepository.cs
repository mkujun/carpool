using Carpool.Interfaces;
using Carpool.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Carpool.Repositories
{
    public class FakeEmployeeRepository : IEmployeeRepository
    {
        public IEnumerable<Employee> Employees => new List<Employee>
        {
            new Employee { Id = 1, Name = "Marko", IsDriver = true }
        };
    }
}
