using System;
using System.Collections.Generic;
using System.Text;

namespace AirMAY.Domain.Models
{
    public class Ticket
    {
        public int Id { get; set; }
        public decimal Price { get; set; }

        public int FirstSityId { get; set; }
        public Sity FirstSity { get; set; }

        public int SecondSityId { get; set; }
        public Sity SecondSity { get; set; }

        public List<TicketUser> TicketUser { get; set; }
        public Ticket()
        {
            TicketUser = new List<TicketUser>();
        }
    }
}