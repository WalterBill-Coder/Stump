// File generated by 'DofusProtocolBuilder.exe v1.0.0.0'
// From 'ExchangeBuyOkMessage.xml' the '22/08/2011 17:23:00'
using System;
using Stump.Core.IO;

namespace Stump.DofusProtocol.Messages
{
	public class ExchangeBuyOkMessage : Message
	{
		public const uint Id = 5759;
		public override uint MessageId
		{
			get
			{
				return 5759;
			}
		}
		
		
		public override void Serialize(IDataWriter writer)
		{
		}
		
		public override void Deserialize(IDataReader reader)
		{
		}
	}
}
