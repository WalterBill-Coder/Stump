// File generated by 'DofusProtocolBuilder.exe v1.0.0.0'
// From 'EmotePlayAbstractMessage.xml' the '22/08/2011 17:22:55'
using System;
using Stump.Core.IO;

namespace Stump.DofusProtocol.Messages
{
	public class EmotePlayAbstractMessage : Message
	{
		public const uint Id = 5690;
		public override uint MessageId
		{
			get
			{
				return 5690;
			}
		}
		
		public sbyte emoteId;
		public byte duration;
		
		public EmotePlayAbstractMessage()
		{
		}
		
		public EmotePlayAbstractMessage(sbyte emoteId, byte duration)
		{
			this.emoteId = emoteId;
			this.duration = duration;
		}
		
		public override void Serialize(IDataWriter writer)
		{
			writer.WriteSByte(emoteId);
			writer.WriteByte(duration);
		}
		
		public override void Deserialize(IDataReader reader)
		{
			emoteId = reader.ReadSByte();
			if ( emoteId < 0 )
			{
				throw new Exception("Forbidden value on emoteId = " + emoteId + ", it doesn't respect the following condition : emoteId < 0");
			}
			duration = reader.ReadByte();
			if ( duration < 0 || duration > 255 )
			{
				throw new Exception("Forbidden value on duration = " + duration + ", it doesn't respect the following condition : duration < 0 || duration > 255");
			}
		}
	}
}
