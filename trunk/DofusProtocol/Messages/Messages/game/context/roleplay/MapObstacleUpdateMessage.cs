// File generated by 'DofusProtocolBuilder.exe v1.0.0.0'
// From 'MapObstacleUpdateMessage.xml' the '22/08/2011 17:22:55'
using System;
using Stump.Core.IO;
using System.Collections.Generic;
using System.Linq;

namespace Stump.DofusProtocol.Messages
{
	public class MapObstacleUpdateMessage : Message
	{
		public const uint Id = 6051;
		public override uint MessageId
		{
			get
			{
				return 6051;
			}
		}
		
		public IEnumerable<Types.MapObstacle> obstacles;
		
		public MapObstacleUpdateMessage()
		{
		}
		
		public MapObstacleUpdateMessage(IEnumerable<Types.MapObstacle> obstacles)
		{
			this.obstacles = obstacles;
		}
		
		public override void Serialize(IDataWriter writer)
		{
			writer.WriteUShort((ushort)obstacles.Count());
			foreach (var entry in obstacles)
			{
				entry.Serialize(writer);
			}
		}
		
		public override void Deserialize(IDataReader reader)
		{
			int limit = reader.ReadUShort();
			obstacles = new Types.MapObstacle[limit];
			for (int i = 0; i < limit; i++)
			{
				(obstacles as Types.MapObstacle[])[i] = new Types.MapObstacle();
				(obstacles as Types.MapObstacle[])[i].Deserialize(reader);
			}
		}
	}
}
