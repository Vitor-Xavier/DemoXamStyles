using System;
using System.Collections.Generic;
using System.Text;

namespace DemoXamStyles.Models
{
    public class Vehicle
    {
        public int VehicleId { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }
        public string Color { get; set; }
        public string ImageSource { get; set; }

        public override string ToString()
        {
            return $"{Model} {Color}";
        }
    }
}
