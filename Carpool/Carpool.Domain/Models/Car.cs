using System;
using System.Collections.Generic;
using System.Text;

namespace Carpool.Domain.Models
{
    public class Car
    {
        public Car()
        {

        }

        public Car(string name, string carType, string color, string plates, int numberOfSeats)
        {
            Name = name;
            CarType = carType;
            Color = color;
            Plates = plates;
            NumberOfSeats = numberOfSeats;
        }

        public string Name { get; set; }
        public string CarType { get; set; }
        public string Color { get; set; }
        public string Plates { get; set; }
        public int NumberOfSeats { get; set; }
    }
}
