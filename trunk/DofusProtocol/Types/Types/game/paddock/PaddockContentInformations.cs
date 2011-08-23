// File generated by 'DofusProtocolBuilder.exe v1.0.0.0'
// From 'PaddockContentInformations.xml' the '22/08/2011 17:23:07'
using System;
using Stump.Core.IO;
using System.Collections.Generic;
using System.Linq;

namespace Stump.DofusProtocol.Types
{
	public class PaddockContentInformations : PaddockInformations
	{
		public const uint Id = 183;
		public override short TypeId
		{
			get
			{
				return 183;
			}
		}
		
		public int paddockId;
		public short worldX;
		public short worldY;
		public int mapId;
		public IEnumerable<Types.MountInformationsForPaddock> mountsInformations;
		
		public PaddockContentInformations()
		{
		}
		
		public PaddockContentInformations(short maxOutdoorMount, short maxItems, int paddockId, short worldX, short worldY, int mapId, IEnumerable<Types.MountInformationsForPaddock> mountsInformations)
			 : base(maxOutdoorMount, maxItems)
		{
			this.paddockId = paddockId;
			this.worldX = worldX;
			this.worldY = worldY;
			this.mapId = mapId;
			this.mountsInformations = mountsInformations;
		}
		
		public override void Serialize(IDataWriter writer)
		{
			base.Serialize(writer);
			writer.WriteInt(paddockId);
			writer.WriteShort(worldX);
			writer.WriteShort(worldY);
			writer.WriteInt(mapId);
			writer.WriteUShort((ushort)mountsInformations.Count());
			foreach (var entry in mountsInformations)
			{
				entry.Serialize(writer);
			}
		}
		
		public override void Deserialize(IDataReader reader)
		{
			base.Deserialize(reader);
			paddockId = reader.ReadInt();
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
			int limit = reader.ReadUShort();
			mountsInformations = new Types.MountInformationsForPaddock[limit];
			for (int i = 0; i < limit; i++)
			{
				(mountsInformations as MountInformationsForPaddock[])[i] = new Types.MountInformationsForPaddock();
				(mountsInformations as Types.MountInformationsForPaddock[])[i].Deserialize(reader);
			}
		}
	}
}
