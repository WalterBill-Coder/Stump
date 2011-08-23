// File generated by 'DofusProtocolBuilder.exe v1.0.0.0'
// From 'PrismAlignmentBonusResultMessage.xml' the '22/08/2011 17:23:03'
using System;
using Stump.Core.IO;

namespace Stump.DofusProtocol.Messages
{
	public class PrismAlignmentBonusResultMessage : Message
	{
		public const uint Id = 5842;
		public override uint MessageId
		{
			get
			{
				return 5842;
			}
		}
		
		public Types.AlignmentBonusInformations alignmentBonus;
		
		public PrismAlignmentBonusResultMessage()
		{
		}
		
		public PrismAlignmentBonusResultMessage(Types.AlignmentBonusInformations alignmentBonus)
		{
			this.alignmentBonus = alignmentBonus;
		}
		
		public override void Serialize(IDataWriter writer)
		{
			alignmentBonus.Serialize(writer);
		}
		
		public override void Deserialize(IDataReader reader)
		{
			alignmentBonus = new Types.AlignmentBonusInformations();
			alignmentBonus.Deserialize(reader);
		}
	}
}
