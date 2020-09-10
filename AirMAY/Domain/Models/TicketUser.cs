using System;
using System.Collections.Generic;
using System.Text;

namespace AirMAY.Domain.Models
{
    public class TicketUser
    {
        public int TicketId { get; set; }
        public Ticket Ticket { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }
    }
}