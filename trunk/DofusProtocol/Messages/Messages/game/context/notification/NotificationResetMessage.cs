// File generated by 'DofusProtocolBuilder.exe v1.0.0.0'
// From 'NotificationResetMessage.xml' the '22/08/2011 17:22:55'
using System;
using Stump.Core.IO;

namespace Stump.DofusProtocol.Messages
{
	public class NotificationResetMessage : Message
	{
		public const uint Id = 6089;
		public override uint MessageId
		{
			get
			{
				return 6089;
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
