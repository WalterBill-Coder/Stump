// File generated by 'DofusProtocolBuilder.exe v1.0.0.0'
// From 'ExchangeItemPaymentForCraftMessage.xml' the '22/08/2011 17:23:01'
using System;
using Stump.Core.IO;

namespace Stump.DofusProtocol.Messages
{
	public class ExchangeItemPaymentForCraftMessage : Message
	{
		public const uint Id = 5831;
		public override uint MessageId
		{
			get
			{
				return 5831;
			}
		}
		
		public bool onlySuccess;
		public Types.ObjectItemNotInContainer @object;
		
		public ExchangeItemPaymentForCraftMessage()
		{
		}
		
		public ExchangeItemPaymentForCraftMessage(bool onlySuccess, Types.ObjectItemNotInContainer @object)
		{
			this.onlySuccess = onlySuccess;
			this.@object = @object;
		}
		
		public override void Serialize(IDataWriter writer)
		{
			writer.WriteBoolean(onlySuccess);
			@object.Serialize(writer);
		}
		
		public override void Deserialize(IDataReader reader)
		{
			onlySuccess = reader.ReadBoolean();
			@object = new Types.ObjectItemNotInContainer();
			@object.Deserialize(reader);
		}
	}
}
