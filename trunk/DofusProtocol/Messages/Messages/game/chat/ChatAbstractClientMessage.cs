// File generated by 'DofusProtocolBuilder.exe v1.0.0.0'
// From 'ChatAbstractClientMessage.xml' the '22/08/2011 17:22:53'
using System;
using Stump.Core.IO;

namespace Stump.DofusProtocol.Messages
{
	public class ChatAbstractClientMessage : Message
	{
		public const uint Id = 850;
		public override uint MessageId
		{
			get
			{
				return 850;
			}
		}
		
		public string content;
		
		public ChatAbstractClientMessage()
		{
		}
		
		public ChatAbstractClientMessage(string content)
		{
			this.content = content;
		}
		
		public override void Serialize(IDataWriter writer)
		{
			writer.WriteUTF(content);
		}
		
		public override void Deserialize(IDataReader reader)
		{
			content = reader.ReadUTF();
		}
	}
}
