// File generated by 'DofusProtocolBuilder.exe v1.0.0.0'
// From 'SlaveSwitchContextMessage.xml' the '22/08/2011 17:23:03'
using System;
using Stump.Core.IO;
using System.Collections.Generic;
using System.Linq;

namespace Stump.DofusProtocol.Messages
{
	public class SlaveSwitchContextMessage : Message
	{
		public const uint Id = 6214;
		public override uint MessageId
		{
			get
			{
				return 6214;
			}
		}
		
		public int summonerId;
		public int slaveId;
		public IEnumerable<Types.SpellItem> slaveSpells;
		public Types.CharacterCharacteristicsInformations slaveStats;
		
		public SlaveSwitchContextMessage()
		{
		}
		
		public SlaveSwitchContextMessage(int summonerId, int slaveId, IEnumerable<Types.SpellItem> slaveSpells, Types.CharacterCharacteristicsInformations slaveStats)
		{
			this.summonerId = summonerId;
			this.slaveId = slaveId;
			this.slaveSpells = slaveSpells;
			this.slaveStats = slaveStats;
		}
		
		public override void Serialize(IDataWriter writer)
		{
			writer.WriteInt(summonerId);
			writer.WriteInt(slaveId);
			writer.WriteUShort((ushort)slaveSpells.Count());
			foreach (var entry in slaveSpells)
			{
				entry.Serialize(writer);
			}
			slaveStats.Serialize(writer);
		}
		
		public override void Deserialize(IDataReader reader)
		{
			summonerId = reader.ReadInt();
			slaveId = reader.ReadInt();
			int limit = reader.ReadUShort();
			slaveSpells = new Types.SpellItem[limit];
			for (int i = 0; i < limit; i++)
			{
				(slaveSpells as Types.SpellItem[])[i] = new Types.SpellItem();
				(slaveSpells as Types.SpellItem[])[i].Deserialize(reader);
			}
			slaveStats = new Types.CharacterCharacteristicsInformations();
			slaveStats.Deserialize(reader);
		}
	}
}
