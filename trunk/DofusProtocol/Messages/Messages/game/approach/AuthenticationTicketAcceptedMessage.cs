// File generated by 'DofusProtocolBuilder.exe v1.0.0.0'
// From 'AuthenticationTicketAcceptedMessage.xml' the '22/08/2011 17:22:52'
using System;
using Stump.Core.IO;

namespace Stump.DofusProtocol.Messages
{
	public class AuthenticationTicketAcceptedMessage : Message
	{
		public const uint Id = 111;
		public override uint MessageId
		{
			get
			{
				return 111;
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
