// File generated by 'DofusProtocolBuilder.exe v1.0.0.0'
// From 'MapCoordinatesExtended.xml' the '22/08/2011 17:23:05'
using System;
using Stump.Core.IO;

namespace Stump.DofusProtocol.Types
{
	public class MapCoordinatesExtended : MapCoordinates
	{
		public const uint Id = 176;
		public override short TypeId
		{
			get
			{
				return 176;
			}
		}
		
		public int mapId;
		
		public MapCoordinatesExtended()
		{
		}
		
		public MapCoordinatesExtended(short worldX, short worldY, int mapId)
			 : base(worldX, worldY)
		{
			this.mapId = mapId;
		}
		
		public override void Serialize(IDataWriter writer)
		{
			base.Serialize(writer);
			writer.WriteInt(mapId);
		}
		
		public override void Deserialize(IDataReader reader)
		{
			base.Deserialize(reader);
			mapId = reader.ReadInt();
		}
	}
}
