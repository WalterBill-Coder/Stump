// File generated by 'DofusProtocolBuilder.exe v1.0.0.0'
// From 'ObjectSetPositionMessage.xml' the '22/08/2011 17:23:03'
using System;
using Stump.Core.IO;

namespace Stump.DofusProtocol.Messages
{
	public class ObjectSetPositionMessage : Message
	{
		public const uint Id = 3021;
		public override uint MessageId
		{
			get
			{
				return 3021;
			}
		}
		
		public int objectUID;
		public byte position;
		public int quantity;
		
		public ObjectSetPositionMessage()
		{
		}
		
		public ObjectSetPositionMessage(int objectUID, byte position, int quantity)
		{
			this.objectUID = objectUID;
			this.position = position;
			this.quantity = quantity;
		}
		
		public override void Serialize(IDataWriter writer)
		{
			writer.WriteInt(objectUID);
			writer.WriteByte(position);
			writer.WriteInt(quantity);
		}
		
		public override void Deserialize(IDataReader reader)
		{
			objectUID = reader.ReadInt();
			if ( objectUID < 0 )
			{
				throw new Exception("Forbidden value on objectUID = " + objectUID + ", it doesn't respect the following condition : objectUID < 0");
			}
			position = reader.ReadByte();
			if ( position < 0 || position > 255 )
			{
				throw new Exception("Forbidden value on position = " + position + ", it doesn't respect the following condition : position < 0 || position > 255");
			}
			quantity = reader.ReadInt();
			if ( quantity < 0 )
			{
				throw new Exception("Forbidden value on quantity = " + quantity + ", it doesn't respect the following condition : quantity < 0");
			}
		}
	}
}
