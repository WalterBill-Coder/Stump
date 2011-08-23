// File generated by 'DofusProtocolBuilder.exe v1.0.0.0'
// From 'ExchangeLeaveMessage.xml' the '22/08/2011 17:23:01'
using System;
using Stump.Core.IO;

namespace Stump.DofusProtocol.Messages
{
	public class ExchangeLeaveMessage : LeaveDialogMessage
	{
		public const uint Id = 5628;
		public override uint MessageId
		{
			get
			{
				return 5628;
			}
		}
		
		public bool success;
		
		public ExchangeLeaveMessage()
		{
		}
		
		public ExchangeLeaveMessage(bool success)
		{
			this.success = success;
		}
		
		public override void Serialize(IDataWriter writer)
		{
			base.Serialize(writer);
			writer.WriteBoolean(success);
		}
		
		public override void Deserialize(IDataReader reader)
		{
			base.Deserialize(reader);
			success = reader.ReadBoolean();
		}
	}
}
