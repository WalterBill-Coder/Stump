// File generated by 'DofusProtocolBuilder.exe v1.0.0.0'
// From 'SkillActionDescriptionCraft.xml' the '22/08/2011 17:23:07'
using System;
using Stump.Core.IO;

namespace Stump.DofusProtocol.Types
{
	public class SkillActionDescriptionCraft : SkillActionDescription
	{
		public const uint Id = 100;
		public override short TypeId
		{
			get
			{
				return 100;
			}
		}
		
		public sbyte maxSlots;
		public sbyte probability;
		
		public SkillActionDescriptionCraft()
		{
		}
		
		public SkillActionDescriptionCraft(short skillId, sbyte maxSlots, sbyte probability)
			 : base(skillId)
		{
			this.maxSlots = maxSlots;
			this.probability = probability;
		}
		
		public override void Serialize(IDataWriter writer)
		{
			base.Serialize(writer);
			writer.WriteSByte(maxSlots);
			writer.WriteSByte(probability);
		}
		
		public override void Deserialize(IDataReader reader)
		{
			base.Deserialize(reader);
			maxSlots = reader.ReadSByte();
			if ( maxSlots < 0 )
			{
				throw new Exception("Forbidden value on maxSlots = " + maxSlots + ", it doesn't respect the following condition : maxSlots < 0");
			}
			probability = reader.ReadSByte();
			if ( probability < 0 )
			{
				throw new Exception("Forbidden value on probability = " + probability + ", it doesn't respect the following condition : probability < 0");
			}
		}
	}
}
