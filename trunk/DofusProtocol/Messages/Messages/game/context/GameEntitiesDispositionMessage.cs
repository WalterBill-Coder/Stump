// File generated by 'DofusProtocolBuilder.exe v1.0.0.0'
// From 'GameEntitiesDispositionMessage.xml' the '22/08/2011 17:22:53'
using System;
using Stump.Core.IO;
using System.Collections.Generic;
using System.Linq;

namespace Stump.DofusProtocol.Messages
{
	public class GameEntitiesDispositionMessage : Message
	{
		public const uint Id = 5696;
		public override uint MessageId
		{
			get
			{
				return 5696;
			}
		}
		
		public IEnumerable<Types.IdentifiedEntityDispositionInformations> dispositions;
		
		public GameEntitiesDispositionMessage()
		{
		}
		
		public GameEntitiesDispositionMessage(IEnumerable<Types.IdentifiedEntityDispositionInformations> dispositions)
		{
			this.dispositions = dispositions;
		}
		
		public override void Serialize(IDataWriter writer)
		{
			writer.WriteUShort((ushort)dispositions.Count());
			foreach (var entry in dispositions)
			{
				writer.WriteShort(entry.TypeId);
				entry.Serialize(writer);
			}
		}
		
		public override void Deserialize(IDataReader reader)
		{
			int limit = reader.ReadUShort();
			dispositions = new Types.IdentifiedEntityDispositionInformations[limit];
			for (int i = 0; i < limit; i++)
			{
				(dispositions as Types.IdentifiedEntityDispositionInformations[])[i] = Types.ProtocolTypeManager.GetInstance<Types.IdentifiedEntityDispositionInformations>(reader.ReadShort());
				(dispositions as Types.IdentifiedEntityDispositionInformations[])[i].Deserialize(reader);
			}
		}
	}
}
