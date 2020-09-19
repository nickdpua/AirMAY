using System;
using System.Collections.Generic;
using System.Text;

namespace AirMAY.Domain.Models
{
    public class Hotel
    {
        public int Id { get; set; }
        public string NameHotel { get; set; }
        public decimal Price { get; set; }
        public string Locate { get; set; }

        public int SityId { get; set; }
        public Sity Sity { get; set; }
    }
}