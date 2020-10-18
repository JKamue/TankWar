using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TankWarLib.Dtos.Messages
{
    public class MessageId: IEquatable<MessageId>
    {
        // Movement Actions
        public static readonly MessageId UpStart = new MessageId(0);
        public static readonly MessageId UpStop = new MessageId(1);
        public static readonly MessageId DownStart = new MessageId(2);
        public static readonly MessageId DownStop = new MessageId(3);
        public static readonly MessageId TurnRightStart = new MessageId(4);
        public static readonly MessageId TurnRightStop = new MessageId(5);
        public static readonly MessageId TurnLeftStart = new MessageId(6);
        public static readonly MessageId TurnLeftStop = new MessageId(7);
        public static readonly MessageId TurnTurretRightStart = new MessageId(8);
        public static readonly MessageId TurnTurretRightStop = new MessageId(9);
        public static readonly MessageId TurnTurretLeftStart = new MessageId(10);
        public static readonly MessageId TurnTurretLeftStop = new MessageId(11);

        // Ingame Actions
        public static readonly MessageId Shoot = new MessageId(12);

        // Connection Actions
        public static readonly MessageId GameJoined = new MessageId(13);
        public static readonly MessageId KeepAlive = new MessageId(14);
        public static readonly MessageId GameQuit = new MessageId(15);

        // Server Messages
        public static readonly MessageId MapData = new MessageId(16);
        public static readonly MessageId Positions = new MessageId(17);

        // Other
        public static readonly MessageId Nothing = new MessageId(18);

        public readonly int Content;

        public MessageId(int number)
        {
            Content = (number > 17 || number < 0) ? 18 : number;
        }

        public bool Equals(MessageId other) => Content == other.Content;

        public override int GetHashCode()
        {
            return Content;
        }
    }
}
