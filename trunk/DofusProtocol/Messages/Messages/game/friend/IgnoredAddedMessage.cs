// File generated by 'DofusProtocolBuilder.exe v1.0.0.0'
// From 'IgnoredAddedMessage.xml' the '22/08/2011 17:22:58'
using System;
using Stump.Core.IO;

namespace Stump.DofusProtocol.Messages
{
	public class IgnoredAddedMessage : Message
	{
		public const uint Id = 5678;
		public override uint MessageId
		{
			get
			{
				return 5678;
			}
		}
		
		public Types.IgnoredInformations ignoreAdded;
		public bool session;
		
		public IgnoredAddedMessage()
		{
		}
		
		public IgnoredAddedMessage(Types.IgnoredInformations ignoreAdded, bool session)
		{
			this.ignoreAdded = ignoreAdded;
			this.session = session;
		}
		
		public override void Serialize(IDataWriter writer)
		{
			writer.WriteShort(ignoreAdded.TypeId);
			ignoreAdded.Serialize(writer);
			writer.WriteBoolean(session);
		}
		
		public override void Deserialize(IDataReader reader)
		{
			ignoreAdded = Types.ProtocolTypeManager.GetInstance<Types.IgnoredInformations>(reader.ReadShort());
			ignoreAdded.Deserialize(reader);
			session = reader.ReadBoolean();
		}
	}
}
