// File generated by 'DofusProtocolBuilder.exe v1.0.0.0'
// From 'GameActionFightDispellMessage.xml' the '22/08/2011 17:22:50'
using System;
using Stump.Core.IO;

namespace Stump.DofusProtocol.Messages
{
	public class GameActionFightDispellMessage : AbstractGameActionMessage
	{
		public const uint Id = 5533;
		public override uint MessageId
		{
			get
			{
				return 5533;
			}
		}
		
		public int targetId;
		
		public GameActionFightDispellMessage()
		{
		}
		
		public GameActionFightDispellMessage(short actionId, int sourceId, int targetId)
			 : base(actionId, sourceId)
		{
			this.targetId = targetId;
		}
		
		public override void Serialize(IDataWriter writer)
		{
			base.Serialize(writer);
			writer.WriteInt(targetId);
		}
		
		public override void Deserialize(IDataReader reader)
		{
			base.Deserialize(reader);
			targetId = reader.ReadInt();
		}
	}
}
