// File generated by 'DofusProtocolBuilder.exe v1.0.0.0'
// From 'AcquaintanceSearchErrorMessage.xml' the '22/08/2011 17:22:50'
using System;
using Stump.Core.IO;

namespace Stump.DofusProtocol.Messages
{
	public class AcquaintanceSearchErrorMessage : Message
	{
		public const uint Id = 6143;
		public override uint MessageId
		{
			get
			{
				return 6143;
			}
		}
		
		public sbyte reason;
		
		public AcquaintanceSearchErrorMessage()
		{
		}
		
		public AcquaintanceSearchErrorMessage(sbyte reason)
		{
			this.reason = reason;
		}
		
		public override void Serialize(IDataWriter writer)
		{
			writer.WriteSByte(reason);
		}
		
		public override void Deserialize(IDataReader reader)
		{
			reason = reader.ReadSByte();
			if ( reason < 0 )
			{
				throw new Exception("Forbidden value on reason = " + reason + ", it doesn't respect the following condition : reason < 0");
			}
		}
	}
}
