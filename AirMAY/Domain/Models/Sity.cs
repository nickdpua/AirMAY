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
        public List<Ticket> Tickets { get; set; }
        public Sity()
        {
            Hotels = new List<Hotel>();
            Tickets = new List<Ticket>();
        }
    }
}