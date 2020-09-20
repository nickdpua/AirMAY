using System;
using System.Collections.Generic;
using System.Text;

namespace AirMAY.Domain.Models
{
    public class Flight
    {
        public int Id { get; set; }
        public decimal Price { get; set; }

        public int FirstSityId { get; set; }
        public City FirstSity { get; set; }

        public int SecondSityId { get; set; }
        public City SecondSity { get; set; }

        public List<FlightTime> FlightTimes { get; set; }
        public List<FlightUser> FlightUser { get; set; }
        public Flight()
        {
            FlightUser = new List<FlightUser>();
        }
    }
}