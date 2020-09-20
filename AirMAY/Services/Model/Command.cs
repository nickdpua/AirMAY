using System;
using System.Collections.Generic;
using System.Text;

namespace AirMAY.Services.Model
{
    public class Command
    {
        public string Message { get; set; }
        public string Nickname { get; set; }
        public string UserStatus { get; set; }
        public int ChatId { get; set; }
    }
}
