using System;
using System.Collections.Generic;
using System.Text;

namespace Carpool.Domain.Models
{
    public class Employee
    {
        public Employee()
        {

        }

        public Employee(int id, string name, bool isDriver)
        {
            Id = id;
            Name = name;
            IsDriver = isDriver;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsDriver { get; set; }
    }
}
