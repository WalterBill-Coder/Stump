// File generated by 'DofusProtocolBuilder.exe v1.0.0.0'
// From 'SequenceEndMessage.xml' the '22/08/2011 17:22:51'
using System;
using Stump.Core.IO;

namespace Stump.DofusProtocol.Messages
{
	public class SequenceEndMessage : Message
	{
		public const uint Id = 956;
		public override uint MessageId
		{
			get
			{
				return 956;
			}
		}
		
		public short actionId;
		public int authorId;
		public sbyte sequenceType;
		
		public SequenceEndMessage()
		{
		}
		
		public SequenceEndMessage(short actionId, int authorId, sbyte sequenceType)
		{
			this.actionId = actionId;
			this.authorId = authorId;
			this.sequenceType = sequenceType;
		}
		
		public override void Serialize(IDataWriter writer)
		{
			writer.WriteShort(actionId);
			writer.WriteInt(authorId);
			writer.WriteSByte(sequenceType);
		}
		
		public override void Deserialize(IDataReader reader)
		{
			actionId = reader.ReadShort();
			if ( actionId < 0 )
			{
				throw new Exception("Forbidden value on actionId = " + actionId + ", it doesn't respect the following condition : actionId < 0");
			}
			authorId = reader.ReadInt();
			sequenceType = reader.ReadSByte();
		}
	}
}
