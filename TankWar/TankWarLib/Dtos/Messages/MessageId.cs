using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TankWarLib.Dtos.Messages
{
    public class MessageId: IEquatable<MessageId>
    {
        // Connection Actions
        public static readonly MessageId GameJoined = new MessageId(0);
        public static readonly MessageId GameQuit = new MessageId(2);

        // Movement Actions
        public static readonly MessageId Movement = new MessageId(3);

        // Ingame Actions
        public static readonly MessageId Shoot = new MessageId(4);


        // Server Messages
        public static readonly MessageId MapData = new MessageId(5);
        public static readonly MessageId Positions = new MessageId(6);

        // Other
        public static readonly MessageId Nothing = new MessageId(7);

        public int Content;

        public MessageId(int number)
        {
            Content = (number > 7 || number < 0) ? 7 : number;
        }

        public bool Equals(MessageId other) => Content == other.Content;

        public override int GetHashCode()
        {
            return Content;
        }
    }
}
