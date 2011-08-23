// File generated by 'DofusProtocolBuilder.exe v1.0.0.0'
// From 'PartyInvitationMemberInformations.xml' the '22/08/2011 17:23:06'
using System;
using Stump.Core.IO;

namespace Stump.DofusProtocol.Types
{
	public class PartyInvitationMemberInformations : CharacterBaseInformations
	{
		public const uint Id = 376;
		public override short TypeId
		{
			get
			{
				return 376;
			}
		}
		
		public short worldX;
		public short worldY;
		public int mapId;
		
		public PartyInvitationMemberInformations()
		{
		}
		
		public PartyInvitationMemberInformations(int id, byte level, string name, Types.EntityLook entityLook, sbyte breed, bool sex, short worldX, short worldY, int mapId)
			 : base(id, level, name, entityLook, breed, sex)
		{
			this.worldX = worldX;
			this.worldY = worldY;
			this.mapId = mapId;
		}
		
		public override void Serialize(IDataWriter writer)
		{
			base.Serialize(writer);
			writer.WriteShort(worldX);
			writer.WriteShort(worldY);
			writer.WriteInt(mapId);
		}
		
		public override void Deserialize(IDataReader reader)
		{
			base.Deserialize(reader);
			worldX = reader.ReadShort();
			if ( worldX < -255 || worldX > 255 )
			{
				throw new Exception("Forbidden value on worldX = " + worldX + ", it doesn't respect the following condition : worldX < -255 || worldX > 255");
			}
			worldY = reader.ReadShort();
			if ( worldY < -255 || worldY > 255 )
			{
				throw new Exception("Forbidden value on worldY = " + worldY + ", it doesn't respect the following condition : worldY < -255 || worldY > 255");
			}
			mapId = reader.ReadInt();
		}
	}
}
