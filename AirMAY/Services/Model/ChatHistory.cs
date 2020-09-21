using System;
using System.Collections.Generic;
using System.Text;

namespace AirMAY.Services.Model
{
    public class ChatHistory
    {
        public string ChatId { get; set; }
        public List<string> Messages { get; set; } = new List<string>();
    }
}
