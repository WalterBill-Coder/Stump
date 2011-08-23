// File generated by 'DofusProtocolBuilder.exe v1.0.0.0'
// From 'SkillActionDescription.xml' the '22/08/2011 17:23:07'
using System;
using Stump.Core.IO;

namespace Stump.DofusProtocol.Types
{
	public class SkillActionDescription
	{
		public const uint Id = 102;
		public virtual short TypeId
		{
			get
			{
				return 102;
			}
		}
		
		public short skillId;
		
		public SkillActionDescription()
		{
		}
		
		public SkillActionDescription(short skillId)
		{
			this.skillId = skillId;
		}
		
		public virtual void Serialize(IDataWriter writer)
		{
			writer.WriteShort(skillId);
		}
		
		public virtual void Deserialize(IDataReader reader)
		{
			skillId = reader.ReadShort();
			if ( skillId < 0 )
			{
				throw new Exception("Forbidden value on skillId = " + skillId + ", it doesn't respect the following condition : skillId < 0");
			}
		}
	}
}
