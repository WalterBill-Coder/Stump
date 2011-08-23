// File generated by 'DofusProtocolBuilder.exe v1.0.0.0'
// From 'GameRolePlayPlayerFightFriendlyAnswerMessage.xml' the '22/08/2011 17:22:55'
using System;
using Stump.Core.IO;

namespace Stump.DofusProtocol.Messages
{
	public class GameRolePlayPlayerFightFriendlyAnswerMessage : Message
	{
		public const uint Id = 5732;
		public override uint MessageId
		{
			get
			{
				return 5732;
			}
		}
		
		public int fightId;
		public bool accept;
		
		public GameRolePlayPlayerFightFriendlyAnswerMessage()
		{
		}
		
		public GameRolePlayPlayerFightFriendlyAnswerMessage(int fightId, bool accept)
		{
			this.fightId = fightId;
			this.accept = accept;
		}
		
		public override void Serialize(IDataWriter writer)
		{
			writer.WriteInt(fightId);
			writer.WriteBoolean(accept);
		}
		
		public override void Deserialize(IDataReader reader)
		{
			fightId = reader.ReadInt();
			accept = reader.ReadBoolean();
		}
	}
}
