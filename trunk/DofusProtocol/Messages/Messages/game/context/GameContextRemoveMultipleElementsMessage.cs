// File generated by 'DofusProtocolBuilder.exe v1.0.0.0'
// From 'GameContextRemoveMultipleElementsMessage.xml' the '22/08/2011 17:22:53'
using System;
using Stump.Core.IO;
using System.Collections.Generic;
using System.Linq;

namespace Stump.DofusProtocol.Messages
{
	public class GameContextRemoveMultipleElementsMessage : Message
	{
		public const uint Id = 252;
		public override uint MessageId
		{
			get
			{
				return 252;
			}
		}
		
		public IEnumerable<int> id;
		
		public GameContextRemoveMultipleElementsMessage()
		{
		}
		
		public GameContextRemoveMultipleElementsMessage(IEnumerable<int> id)
		{
			this.id = id;
		}
		
		public override void Serialize(IDataWriter writer)
		{
			writer.WriteUShort((ushort)id.Count());
			foreach (var entry in id)
			{
				writer.WriteInt(entry);
			}
		}
		
		public override void Deserialize(IDataReader reader)
		{
			int limit = reader.ReadUShort();
			id = new int[limit];
			for (int i = 0; i < limit; i++)
			{
				(id as int[])[i] = reader.ReadInt();
			}
		}
	}
}
