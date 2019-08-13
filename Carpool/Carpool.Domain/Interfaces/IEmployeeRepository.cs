using Carpool.Domain.Models;
using System.Collections.Generic;

namespace Carpool.Domain.Interfaces
{
    public interface IEmployeeRepository
    {
        IEnumerable<Employee> GetEmployees();
    }
}
