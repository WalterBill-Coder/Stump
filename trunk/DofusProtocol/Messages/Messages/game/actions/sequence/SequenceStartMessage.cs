// File generated by 'DofusProtocolBuilder.exe v1.0.0.0'
// From 'SequenceStartMessage.xml' the '22/08/2011 17:22:51'
using System;
using Stump.Core.IO;

namespace Stump.DofusProtocol.Messages
{
	public class SequenceStartMessage : Message
	{
		public const uint Id = 955;
		public override uint MessageId
		{
			get
			{
				return 955;
			}
		}
		
		public sbyte sequenceType;
		public int authorId;
		
		public SequenceStartMessage()
		{
		}
		
		public SequenceStartMessage(sbyte sequenceType, int authorId)
		{
			this.sequenceType = sequenceType;
			this.authorId = authorId;
		}
		
		public override void Serialize(IDataWriter writer)
		{
			writer.WriteSByte(sequenceType);
			writer.WriteInt(authorId);
		}
		
		public override void Deserialize(IDataReader reader)
		{
			sequenceType = reader.ReadSByte();
			authorId = reader.ReadInt();
		}
	}
}
