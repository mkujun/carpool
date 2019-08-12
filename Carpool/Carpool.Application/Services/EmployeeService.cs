using System.Collections.Generic;
using System.Linq;
using Carpool.Domain.Interfaces;
using Carpool.Domain.Models;

namespace Carpool.Application.Services
{
    public class EmployeeService : IEmployeeService
    {
        private IEmployeeRepository _employeeRepository;

        public EmployeeService(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public IEnumerable<Employee> Employees => _employeeRepository.GetEmployees();

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

                if (employee.IsDriver)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
