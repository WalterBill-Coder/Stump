// File generated by 'DofusProtocolBuilder.exe v1.0.0.0'
// From 'PrismAttackRequestMessage.xml' the '22/08/2011 17:23:03'
using System;
using Stump.Core.IO;

namespace Stump.DofusProtocol.Messages
{
	public class PrismAttackRequestMessage : Message
	{
		public const uint Id = 6042;
		public override uint MessageId
		{
			get
			{
				return 6042;
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
