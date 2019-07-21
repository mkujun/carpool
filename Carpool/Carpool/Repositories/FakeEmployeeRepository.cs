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
            new Employee { Id = 4, Name = "Ringo Starr", IsDriver = false },
            new Employee { Id = 5, Name = "Johnny Cash", IsDriver = true },
            new Employee { Id = 6, Name = "Jimi Hendrix", IsDriver = false },
            new Employee { Id = 7, Name = "Eric Clapton", IsDriver = true },
            new Employee { Id = 8, Name = "Carlos Santana", IsDriver = false },
            new Employee { Id = 9, Name = "Freddie Mercury", IsDriver = false },
            new Employee { Id = 10, Name = "David Bowie", IsDriver = false },
            new Employee { Id = 11, Name = "Mark Knopfler", IsDriver = true },
            new Employee { Id = 12, Name = "John Frusciante", IsDriver = false },
            new Employee { Id = 13, Name = "Bono Vox", IsDriver = true },
            new Employee { Id = 14, Name = "Jimmy Page", IsDriver = true },
            new Employee { Id = 15, Name = "John Coltrane", IsDriver = true },
            new Employee { Id = 16, Name = "Al Di Meola", IsDriver = true },
            new Employee { Id = 17, Name = "Brandon Flowers", IsDriver = false },
            new Employee { Id = 18, Name = "Alex Turner", IsDriver = false },
            new Employee { Id = 19, Name = "Arsen Dedic", IsDriver = true },
            new Employee { Id = 20, Name = "Milan Mladenovic", IsDriver = true },
            new Employee { Id = 21, Name = "Jim Morrison", IsDriver = true },
            new Employee { Id = 22, Name = "Jack White", IsDriver = false },
            new Employee { Id = 23, Name = "Goran Bare", IsDriver = false },
            new Employee { Id = 24, Name = "Norah Jones", IsDriver = true },
            new Employee { Id = 25, Name = "Boris Stok", IsDriver = true } // easter egg for Milivoj M.
        };

        public int[] GetEmployeeIds(List<Employee> employees)
        {
            Employee[] employeeArray = employees.ToArray();

            int[] employeeIds = new int[employeeArray.Length];

            for (int i = 0; i < employeeArray.Length; i++)
            {
                employeeIds[i] = employeeArray[i].Id; 
            }

            return employeeIds;
        }

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
