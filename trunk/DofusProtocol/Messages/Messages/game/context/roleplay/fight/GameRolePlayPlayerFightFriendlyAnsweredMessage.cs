// File generated by 'DofusProtocolBuilder.exe v1.0.0.0'
// From 'GameRolePlayPlayerFightFriendlyAnsweredMessage.xml' the '22/08/2011 17:22:55'
using System;
using Stump.Core.IO;

namespace Stump.DofusProtocol.Messages
{
	public class GameRolePlayPlayerFightFriendlyAnsweredMessage : Message
	{
		public const uint Id = 5733;
		public override uint MessageId
		{
			get
			{
				return 5733;
			}
		}
		
		public int fightId;
		public int sourceId;
		public int targetId;
		public bool accept;
		
		public GameRolePlayPlayerFightFriendlyAnsweredMessage()
		{
		}
		
		public GameRolePlayPlayerFightFriendlyAnsweredMessage(int fightId, int sourceId, int targetId, bool accept)
		{
			this.fightId = fightId;
			this.sourceId = sourceId;
			this.targetId = targetId;
			this.accept = accept;
		}
		
		public override void Serialize(IDataWriter writer)
		{
			writer.WriteInt(fightId);
			writer.WriteInt(sourceId);
			writer.WriteInt(targetId);
			writer.WriteBoolean(accept);
		}
		
		public override void Deserialize(IDataReader reader)
		{
			fightId = reader.ReadInt();
			sourceId = reader.ReadInt();
			if ( sourceId < 0 )
			{
				throw new Exception("Forbidden value on sourceId = " + sourceId + ", it doesn't respect the following condition : sourceId < 0");
			}
			targetId = reader.ReadInt();
			if ( targetId < 0 )
			{
				throw new Exception("Forbidden value on targetId = " + targetId + ", it doesn't respect the following condition : targetId < 0");
			}
			accept = reader.ReadBoolean();
		}
	}
}
