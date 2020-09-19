using System;
using System.Collections.Generic;
using System.Text;

namespace AirMAY.Domain.Models
{
    public class FlightTime
    {
        public int Id { get; set; }
        public DateTime TimeOfDispatch { get; set; }
        public DateTime EstimatedArrivalTime { get; set; }

        public int FlightId { get; set; }
        public Flight Flight { get; set; }
    }
}