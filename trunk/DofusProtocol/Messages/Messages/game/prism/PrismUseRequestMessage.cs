// File generated by 'DofusProtocolBuilder.exe v1.0.0.0'
// From 'PrismUseRequestMessage.xml' the '22/08/2011 17:23:03'
using System;
using Stump.Core.IO;

namespace Stump.DofusProtocol.Messages
{
	public class PrismUseRequestMessage : Message
	{
		public const uint Id = 6041;
		public override uint MessageId
		{
			get
			{
				return 6041;
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
