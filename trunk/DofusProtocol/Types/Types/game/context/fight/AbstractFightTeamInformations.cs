// File generated by 'DofusProtocolBuilder.exe v1.0.0.0'
// From 'AbstractFightTeamInformations.xml' the '22/08/2011 17:23:05'
using System;
using Stump.Core.IO;

namespace Stump.DofusProtocol.Types
{
	public class AbstractFightTeamInformations
	{
		public const uint Id = 116;
		public virtual short TypeId
		{
			get
			{
				return 116;
			}
		}
		
		public sbyte teamId;
		public int leaderId;
		public sbyte teamSide;
		public sbyte teamTypeId;
		
		public AbstractFightTeamInformations()
		{
		}
		
		public AbstractFightTeamInformations(sbyte teamId, int leaderId, sbyte teamSide, sbyte teamTypeId)
		{
			this.teamId = teamId;
			this.leaderId = leaderId;
			this.teamSide = teamSide;
			this.teamTypeId = teamTypeId;
		}
		
		public virtual void Serialize(IDataWriter writer)
		{
			writer.WriteSByte(teamId);
			writer.WriteInt(leaderId);
			writer.WriteSByte(teamSide);
			writer.WriteSByte(teamTypeId);
		}
		
		public virtual void Deserialize(IDataReader reader)
		{
			teamId = reader.ReadSByte();
			if ( teamId < 0 )
			{
				throw new Exception("Forbidden value on teamId = " + teamId + ", it doesn't respect the following condition : teamId < 0");
			}
			leaderId = reader.ReadInt();
			teamSide = reader.ReadSByte();
			teamTypeId = reader.ReadSByte();
			if ( teamTypeId < 0 )
			{
				throw new Exception("Forbidden value on teamTypeId = " + teamTypeId + ", it doesn't respect the following condition : teamTypeId < 0");
			}
		}
	}
}