// File generated by 'DofusProtocolBuilder.exe v1.0.0.0'
// From 'InteractiveElementNamedSkill.xml' the '22/08/2011 17:23:07'
using System;
using Stump.Core.IO;

namespace Stump.DofusProtocol.Types
{
	public class InteractiveElementNamedSkill : InteractiveElementSkill
	{
		public const uint Id = 220;
		public override short TypeId
		{
			get
			{
				return 220;
			}
		}
		
		public int nameId;
		
		public InteractiveElementNamedSkill()
		{
		}
		
		public InteractiveElementNamedSkill(int skillId, int skillInstanceUid, int nameId)
			 : base(skillId, skillInstanceUid)
		{
			this.nameId = nameId;
		}
		
		public override void Serialize(IDataWriter writer)
		{
			base.Serialize(writer);
			writer.WriteInt(nameId);
		}
		
		public override void Deserialize(IDataReader reader)
		{
			base.Deserialize(reader);
			nameId = reader.ReadInt();
			if ( nameId < 0 )
			{
				throw new Exception("Forbidden value on nameId = " + nameId + ", it doesn't respect the following condition : nameId < 0");
			}
		}
	}
}
