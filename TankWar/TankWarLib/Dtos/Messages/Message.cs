using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace TankWarLib.Dtos.Messages
{
    public class Message
    {
        public readonly string Content;
        public readonly MessageId Id;

        public Message(MessageId id, string content = null)
        {
            Id = id;
            Content = content;
        }

        public string Serialize() => JsonConvert.SerializeObject(this);

        public static Message Deserialize(string serialized) => JsonConvert.DeserializeObject<Message>(serialized);
    }
}
