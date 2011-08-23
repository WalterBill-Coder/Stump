// File generated by 'DofusProtocolBuilder.exe v1.0.0.0'
// From 'NotificationByServerMessage.xml' the '22/08/2011 17:22:55'
using System;
using Stump.Core.IO;
using System.Collections.Generic;
using System.Linq;

namespace Stump.DofusProtocol.Messages
{
	public class NotificationByServerMessage : Message
	{
		public const uint Id = 6103;
		public override uint MessageId
		{
			get
			{
				return 6103;
			}
		}
		
		public ushort id;
		public IEnumerable<string> parameters;
		public bool forceOpen;
		
		public NotificationByServerMessage()
		{
		}
		
		public NotificationByServerMessage(ushort id, IEnumerable<string> parameters, bool forceOpen)
		{
			this.id = id;
			this.parameters = parameters;
			this.forceOpen = forceOpen;
		}
		
		public override void Serialize(IDataWriter writer)
		{
			writer.WriteUShort(id);
			writer.WriteUShort((ushort)parameters.Count());
			foreach (var entry in parameters)
			{
				writer.WriteUTF(entry);
			}
			writer.WriteBoolean(forceOpen);
		}
		
		public override void Deserialize(IDataReader reader)
		{
			id = reader.ReadUShort();
			if ( id < 0 || id > 65535 )
			{
				throw new Exception("Forbidden value on id = " + id + ", it doesn't respect the following condition : id < 0 || id > 65535");
			}
			int limit = reader.ReadUShort();
			parameters = new string[limit];
			for (int i = 0; i < limit; i++)
			{
				(parameters as string[])[i] = reader.ReadUTF();
			}
			forceOpen = reader.ReadBoolean();
		}
	}
}
