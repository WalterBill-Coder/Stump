// File generated by 'DofusProtocolBuilder.exe v1.0.0.0'
// From 'FriendWarnOnLevelGainStateMessage.xml' the '22/08/2011 17:22:58'
using System;
using Stump.Core.IO;

namespace Stump.DofusProtocol.Messages
{
	public class FriendWarnOnLevelGainStateMessage : Message
	{
		public const uint Id = 6078;
		public override uint MessageId
		{
			get
			{
				return 6078;
			}
		}
		
		public bool enable;
		
		public FriendWarnOnLevelGainStateMessage()
		{
		}
		
		public FriendWarnOnLevelGainStateMessage(bool enable)
		{
			this.enable = enable;
		}
		
		public override void Serialize(IDataWriter writer)
		{
			writer.WriteBoolean(enable);
		}
		
		public override void Deserialize(IDataReader reader)
		{
			enable = reader.ReadBoolean();
		}
	}
}
