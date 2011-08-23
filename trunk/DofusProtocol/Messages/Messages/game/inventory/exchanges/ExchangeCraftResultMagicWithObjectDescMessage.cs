// File generated by 'DofusProtocolBuilder.exe v1.0.0.0'
// From 'ExchangeCraftResultMagicWithObjectDescMessage.xml' the '22/08/2011 17:23:00'
using System;
using Stump.Core.IO;

namespace Stump.DofusProtocol.Messages
{
	public class ExchangeCraftResultMagicWithObjectDescMessage : ExchangeCraftResultWithObjectDescMessage
	{
		public const uint Id = 6188;
		public override uint MessageId
		{
			get
			{
				return 6188;
			}
		}
		
		public sbyte magicPoolStatus;
		
		public ExchangeCraftResultMagicWithObjectDescMessage()
		{
		}
		
		public ExchangeCraftResultMagicWithObjectDescMessage(sbyte craftResult, Types.ObjectItemNotInContainer objectInfo, sbyte magicPoolStatus)
			 : base(craftResult, objectInfo)
		{
			this.magicPoolStatus = magicPoolStatus;
		}
		
		public override void Serialize(IDataWriter writer)
		{
			base.Serialize(writer);
			writer.WriteSByte(magicPoolStatus);
		}
		
		public override void Deserialize(IDataReader reader)
		{
			base.Deserialize(reader);
			magicPoolStatus = reader.ReadSByte();
		}
	}
}
