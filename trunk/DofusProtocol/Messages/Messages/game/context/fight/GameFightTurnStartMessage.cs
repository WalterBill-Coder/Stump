// File generated by 'DofusProtocolBuilder.exe v1.0.0.0'
// From 'GameFightTurnStartMessage.xml' the '22/08/2011 17:22:54'
using System;
using Stump.Core.IO;

namespace Stump.DofusProtocol.Messages
{
	public class GameFightTurnStartMessage : Message
	{
		public const uint Id = 714;
		public override uint MessageId
		{
			get
			{
				return 714;
			}
		}
		
		public int id;
		public int waitTime;
		
		public GameFightTurnStartMessage()
		{
		}
		
		public GameFightTurnStartMessage(int id, int waitTime)
		{
			this.id = id;
			this.waitTime = waitTime;
		}
		
		public override void Serialize(IDataWriter writer)
		{
			writer.WriteInt(id);
			writer.WriteInt(waitTime);
		}
		
		public override void Deserialize(IDataReader reader)
		{
			id = reader.ReadInt();
			waitTime = reader.ReadInt();
			if ( waitTime < 0 )
			{
				throw new Exception("Forbidden value on waitTime = " + waitTime + ", it doesn't respect the following condition : waitTime < 0");
			}
		}
	}
}
