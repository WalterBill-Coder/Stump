// File generated by 'DofusProtocolBuilder.exe v1.0.0.0'
// From 'ExchangeObjectTransfertExistingToInvMessage.xml' the '26/06/2012 18:47:57'
using System;
using Stump.Core.IO;

namespace Stump.DofusProtocol.Messages
{
	public class ExchangeObjectTransfertExistingToInvMessage : Message
	{
		public const uint Id = 6326;
		public override uint MessageId
		{
			get
			{
				return 6326;
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