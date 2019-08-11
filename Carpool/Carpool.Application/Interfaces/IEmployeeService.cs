using Carpool.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Carpool.Application
{
    public interface IEmployeeService
    {
        IEnumerable<Employee> Employees { get; }
        bool HasDriverLicense(int[] employeeIds);
        List<Employee> GetEmployeesByIds(int[] employeeIds);
        int[] GetEmployeeIds(List<Employee> employees);
    }
}
