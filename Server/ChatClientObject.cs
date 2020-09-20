using System;
using System.Collections.Generic;
using System.Text;

namespace Server
{
    public class ChatClientObject
    {
        public string ChatId { get; set; }
        public ClientObject AdminClientObject { get; set; }
        public ClientObject ClientObject { get; set; }
        public void BroadcastMessage(Command message, string id)
        {
            if (ClientObject.Id != id) ClientObject.SendCommand(message);
            else AdminClientObject.SendCommand(message);
        }
    }
}
