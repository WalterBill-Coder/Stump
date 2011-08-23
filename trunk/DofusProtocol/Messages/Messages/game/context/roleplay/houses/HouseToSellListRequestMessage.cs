// File generated by 'DofusProtocolBuilder.exe v1.0.0.0'
// From 'HouseToSellListRequestMessage.xml' the '22/08/2011 17:22:55'
using System;
using Stump.Core.IO;

namespace Stump.DofusProtocol.Messages
{
	public class HouseToSellListRequestMessage : Message
	{
		public const uint Id = 6139;
		public override uint MessageId
		{
			get
			{
				return 6139;
			}
		}
		
		public short pageIndex;
		
		public HouseToSellListRequestMessage()
		{
		}
		
		public HouseToSellListRequestMessage(short pageIndex)
		{
			this.pageIndex = pageIndex;
		}
		
		public override void Serialize(IDataWriter writer)
		{
			writer.WriteShort(pageIndex);
		}
		
		public override void Deserialize(IDataReader reader)
		{
			pageIndex = reader.ReadShort();
			if ( pageIndex < 0 )
			{
				throw new Exception("Forbidden value on pageIndex = " + pageIndex + ", it doesn't respect the following condition : pageIndex < 0");
			}
		}
	}
}
