using Carpool.Domain.Models;
using System.Collections.Generic;

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
