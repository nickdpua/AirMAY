using System;
using System.Collections.Generic;
using System.Text;

namespace AirMAY.Domain.Models
{
    public class Sity
    {
        public int Id { get; set; }
        public string SityName { get; set; }
        public string ZipCode { get; set; }

        public List<Hotel> Hotels { get; set; }
        public List<Flight> FirstSity { get; set; }
        public List<Flight> SecondSity { get; set; }
        public Sity()
        {
            Hotels = new List<Hotel>();
            FirstSity = new List<Flight>();
            SecondSity = new List<Flight>();
        }
    }
}