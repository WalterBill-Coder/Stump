// File generated by 'DofusProtocolBuilder.exe v1.0.0.0'
// From 'GuildSpellUpgradeRequestMessage.xml' the '22/08/2011 17:22:59'
using System;
using Stump.Core.IO;

namespace Stump.DofusProtocol.Messages
{
	public class GuildSpellUpgradeRequestMessage : Message
	{
		public const uint Id = 5699;
		public override uint MessageId
		{
			get
			{
				return 5699;
			}
		}
		
		public int spellId;
		
		public GuildSpellUpgradeRequestMessage()
		{
		}
		
		public GuildSpellUpgradeRequestMessage(int spellId)
		{
			this.spellId = spellId;
		}
		
		public override void Serialize(IDataWriter writer)
		{
			writer.WriteInt(spellId);
		}
		
		public override void Deserialize(IDataReader reader)
		{
			spellId = reader.ReadInt();
			if ( spellId < 0 )
			{
				throw new Exception("Forbidden value on spellId = " + spellId + ", it doesn't respect the following condition : spellId < 0");
			}
		}
	}
}
