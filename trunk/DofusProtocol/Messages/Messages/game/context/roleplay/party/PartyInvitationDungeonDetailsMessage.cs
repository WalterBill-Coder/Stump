// File generated by 'DofusProtocolBuilder.exe v1.0.0.0'
// From 'PartyInvitationDungeonDetailsMessage.xml' the '22/08/2011 17:22:57'
using System;
using Stump.Core.IO;
using System.Collections.Generic;
using System.Linq;

namespace Stump.DofusProtocol.Messages
{
	public class PartyInvitationDungeonDetailsMessage : PartyInvitationDetailsMessage
	{
		public const uint Id = 6262;
		public override uint MessageId
		{
			get
			{
				return 6262;
			}
		}
		
		public short dungeonId;
		public IEnumerable<bool> playersDungeonReady;
		
		public PartyInvitationDungeonDetailsMessage()
		{
		}
		
		public PartyInvitationDungeonDetailsMessage(int partyId, int fromId, string fromName, int leaderId, IEnumerable<Types.PartyInvitationMemberInformations> members, short dungeonId, IEnumerable<bool> playersDungeonReady)
			 : base(partyId, fromId, fromName, leaderId, members)
		{
			this.dungeonId = dungeonId;
			this.playersDungeonReady = playersDungeonReady;
		}
		
		public override void Serialize(IDataWriter writer)
		{
			base.Serialize(writer);
			writer.WriteShort(dungeonId);
			writer.WriteUShort((ushort)playersDungeonReady.Count());
			foreach (var entry in playersDungeonReady)
			{
				writer.WriteBoolean(entry);
			}
		}
		
		public override void Deserialize(IDataReader reader)
		{
			base.Deserialize(reader);
			dungeonId = reader.ReadShort();
			if ( dungeonId < 0 )
			{
				throw new Exception("Forbidden value on dungeonId = " + dungeonId + ", it doesn't respect the following condition : dungeonId < 0");
			}
			int limit = reader.ReadUShort();
			playersDungeonReady = new bool[limit];
			for (int i = 0; i < limit; i++)
			{
				(playersDungeonReady as bool[])[i] = reader.ReadBoolean();
			}
		}
	}
}
