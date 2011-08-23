// File generated by 'DofusProtocolBuilder.exe v1.0.0.0'
// From 'ExchangeBidHouseSearchMessage.xml' the '22/08/2011 17:23:00'
using System;
using Stump.Core.IO;

namespace Stump.DofusProtocol.Messages
{
	public class ExchangeBidHouseSearchMessage : Message
	{
		public const uint Id = 5806;
		public override uint MessageId
		{
			get
			{
				return 5806;
			}
		}
		
		public int type;
		public int genId;
		
		public ExchangeBidHouseSearchMessage()
		{
		}
		
		public ExchangeBidHouseSearchMessage(int type, int genId)
		{
			this.type = type;
			this.genId = genId;
		}
		
		public override void Serialize(IDataWriter writer)
		{
			writer.WriteInt(type);
			writer.WriteInt(genId);
		}
		
		public override void Deserialize(IDataReader reader)
		{
			type = reader.ReadInt();
			if ( type < 0 )
			{
				throw new Exception("Forbidden value on type = " + type + ", it doesn't respect the following condition : type < 0");
			}
			genId = reader.ReadInt();
			if ( genId < 0 )
			{
				throw new Exception("Forbidden value on genId = " + genId + ", it doesn't respect the following condition : genId < 0");
			}
		}
	}
}
