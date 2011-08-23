// File generated by 'DofusProtocolBuilder.exe v1.0.0.0'
// From 'NotificationListMessage.xml' the '22/08/2011 17:22:55'
using System;
using Stump.Core.IO;
using System.Collections.Generic;
using System.Linq;

namespace Stump.DofusProtocol.Messages
{
	public class NotificationListMessage : Message
	{
		public const uint Id = 6087;
		public override uint MessageId
		{
			get
			{
				return 6087;
			}
		}
		
		public IEnumerable<int> flags;
		
		public NotificationListMessage()
		{
		}
		
		public NotificationListMessage(IEnumerable<int> flags)
		{
			this.flags = flags;
		}
		
		public override void Serialize(IDataWriter writer)
		{
			writer.WriteUShort((ushort)flags.Count());
			foreach (var entry in flags)
			{
				writer.WriteInt(entry);
			}
		}
		
		public override void Deserialize(IDataReader reader)
		{
			int limit = reader.ReadUShort();
			flags = new int[limit];
			for (int i = 0; i < limit; i++)
			{
				(flags as int[])[i] = reader.ReadInt();
			}
		}
	}
}
