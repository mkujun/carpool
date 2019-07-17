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
            new Employee { Id = 1, Name = "John Lennon", IsDriver = true },
            new Employee { Id = 2, Name = "Paul McCartney", IsDriver = true },
            new Employee { Id = 3, Name = "George Harrison", IsDriver = false },
            new Employee { Id = 7, Name = "Ringo Starr", IsDriver = false }
        };

        public List<Employee> GetEmployeesByIds(int[] employeeIds)
        {
            List<Employee> employees = new List<Employee>();

            foreach (var employeeId in employeeIds)
            {
                Employee employee = Employees.Where(e => e.Id == employeeId).FirstOrDefault();

                if (employee != null)
                {
                    employees.Add(employee);
                }
            }

            return employees;
        }

        public bool HasDriverLicense(int[] employeeIds)
        {
            foreach (int employeeId in employeeIds)
            {
                Employee employee = Employees.Where(e => e.Id == employeeId).FirstOrDefault();

                if(employee.IsDriver)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
