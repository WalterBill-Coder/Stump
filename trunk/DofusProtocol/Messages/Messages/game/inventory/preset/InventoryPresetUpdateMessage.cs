// File generated by 'DofusProtocolBuilder.exe v1.0.0.0'
// From 'InventoryPresetUpdateMessage.xml' the '22/08/2011 17:23:03'
using System;
using Stump.Core.IO;

namespace Stump.DofusProtocol.Messages
{
	public class InventoryPresetUpdateMessage : Message
	{
		public const uint Id = 6171;
		public override uint MessageId
		{
			get
			{
				return 6171;
			}
		}
		
		public Types.Preset preset;
		
		public InventoryPresetUpdateMessage()
		{
		}
		
		public InventoryPresetUpdateMessage(Types.Preset preset)
		{
			this.preset = preset;
		}
		
		public override void Serialize(IDataWriter writer)
		{
			preset.Serialize(writer);
		}
		
		public override void Deserialize(IDataReader reader)
		{
			preset = new Types.Preset();
			preset.Deserialize(reader);
		}
	}
}
