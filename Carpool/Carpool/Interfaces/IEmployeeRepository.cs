using Carpool.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Carpool.Interfaces
{
    public interface IEmployeeRepository
    {
        IEnumerable<Employee> Employees { get; }
        bool HasDriverLicense(int[] employeeIds);
        List<Employee> GetEmployeesByIds(int[] employeeIds);
    }
}
