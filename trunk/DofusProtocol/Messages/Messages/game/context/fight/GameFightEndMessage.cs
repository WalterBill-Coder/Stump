// File generated by 'DofusProtocolBuilder.exe v1.0.0.0'
// From 'GameFightEndMessage.xml' the '22/08/2011 17:22:54'
using System;
using Stump.Core.IO;
using System.Collections.Generic;
using System.Linq;

namespace Stump.DofusProtocol.Messages
{
	public class GameFightEndMessage : Message
	{
		public const uint Id = 720;
		public override uint MessageId
		{
			get
			{
				return 720;
			}
		}
		
		public int duration;
		public short ageBonus;
		public IEnumerable<Types.FightResultListEntry> results;
		
		public GameFightEndMessage()
		{
		}
		
		public GameFightEndMessage(int duration, short ageBonus, IEnumerable<Types.FightResultListEntry> results)
		{
			this.duration = duration;
			this.ageBonus = ageBonus;
			this.results = results;
		}
		
		public override void Serialize(IDataWriter writer)
		{
			writer.WriteInt(duration);
			writer.WriteShort(ageBonus);
			writer.WriteUShort((ushort)results.Count());
			foreach (var entry in results)
			{
				writer.WriteShort(entry.TypeId);
				entry.Serialize(writer);
			}
		}
		
		public override void Deserialize(IDataReader reader)
		{
			duration = reader.ReadInt();
			if ( duration < 0 )
			{
				throw new Exception("Forbidden value on duration = " + duration + ", it doesn't respect the following condition : duration < 0");
			}
			ageBonus = reader.ReadShort();
			int limit = reader.ReadUShort();
			results = new Types.FightResultListEntry[limit];
			for (int i = 0; i < limit; i++)
			{
				(results as Types.FightResultListEntry[])[i] = Types.ProtocolTypeManager.GetInstance<Types.FightResultListEntry>(reader.ReadShort());
				(results as Types.FightResultListEntry[])[i].Deserialize(reader);
			}
		}
	}
}
