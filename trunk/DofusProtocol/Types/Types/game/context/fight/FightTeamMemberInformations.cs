// File generated by 'DofusProtocolBuilder.exe v1.0.0.0'
// From 'FightTeamMemberInformations.xml' the '22/08/2011 17:23:05'
using System;
using Stump.Core.IO;

namespace Stump.DofusProtocol.Types
{
	public class FightTeamMemberInformations
	{
		public const uint Id = 44;
		public virtual short TypeId
		{
			get
			{
				return 44;
			}
		}
		
		public int id;
		
		public FightTeamMemberInformations()
		{
		}
		
		public FightTeamMemberInformations(int id)
		{
			this.id = id;
		}
		
		public virtual void Serialize(IDataWriter writer)
		{
			writer.WriteInt(id);
		}
		
		public virtual void Deserialize(IDataReader reader)
		{
			id = reader.ReadInt();
		}
	}
}
