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
            new Employee { Id = 1, Name = "John", IsDriver = true },
            new Employee { Id = 2, Name = "Paul", IsDriver = true },
            new Employee { Id = 3, Name = "George", IsDriver = true },
            new Employee { Id = 7, Name = "Ringo", IsDriver = true }
        };
    }
}
