using System;
using System.Collections.Generic;
using System.Text;

namespace AirMAY.Domain.Models
{
    public class FlightUser
    {
        public int FlightId { get; set; }
        public Flight Flight { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }
    }
}