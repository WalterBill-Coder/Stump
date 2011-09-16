// File generated by 'DofusProtocolBuilder.exe v1.0.0.0'
// From 'GameRolePlayPVPArenaFightRequestedMessage.xml' the '22/08/2011 17:22:55'
using System;
using Stump.Core.IO;
using System.Collections.Generic;
using System.Linq;

namespace Stump.DofusProtocol.Messages
{
	public class GameRolePlayPVPArenaFightRequestedMessage : Message
	{
		public const uint Id = 6258;
		public override uint MessageId
		{
			get
			{
				return 6258;
			}
		}
		
		public int fightId;
		public IEnumerable<int> fightersIDs;
		
		public GameRolePlayPVPArenaFightRequestedMessage()
		{
		}
		
		public GameRolePlayPVPArenaFightRequestedMessage(int fightId, IEnumerable<int> fightersIDs)
		{
			this.fightId = fightId;
			this.fightersIDs = fightersIDs;
		}
		
		public override void Serialize(IDataWriter writer)
		{
			writer.WriteInt(fightId);
			writer.WriteUShort((ushort)fightersIDs.Count());
			foreach (var entry in fightersIDs)
			{
				writer.WriteInt(entry);
			}
		}
		
		public override void Deserialize(IDataReader reader)
		{
			fightId = reader.ReadInt();
			if ( fightId < 0 )
			{
				throw new Exception("Forbidden value on fightId = " + fightId + ", it doesn't respect the following condition : fightId < 0");
			}
			int limit = reader.ReadUShort();
			fightersIDs = new int[limit];
			for (int i = 0; i < limit; i++)
			{
				(fightersIDs as int[])[i] = reader.ReadInt();
			}
		}
	}
}