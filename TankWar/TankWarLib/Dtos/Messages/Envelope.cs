using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TankWarLib.Dtos.Messages
{
    public class Envelope
    {
        public string Id;
        public Message Message;

        public Envelope(string id, Message message)
        {
            Id = id;
            Message = message;
        }
    }
}
