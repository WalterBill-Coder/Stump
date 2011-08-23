// File generated by 'DofusProtocolBuilder.exe v1.0.0.0'
// From 'OrientedObjectItemWithLookInRolePlay.xml' the '22/08/2011 17:23:06'
using System;
using Stump.Core.IO;

namespace Stump.DofusProtocol.Types
{
	public class OrientedObjectItemWithLookInRolePlay : ObjectItemWithLookInRolePlay
	{
		public const uint Id = 199;
		public override short TypeId
		{
			get
			{
				return 199;
			}
		}
		
		public sbyte direction;
		
		public OrientedObjectItemWithLookInRolePlay()
		{
		}
		
		public OrientedObjectItemWithLookInRolePlay(short cellId, short objectGID, Types.EntityLook entityLook, sbyte direction)
			 : base(cellId, objectGID, entityLook)
		{
			this.direction = direction;
		}
		
		public override void Serialize(IDataWriter writer)
		{
			base.Serialize(writer);
			writer.WriteSByte(direction);
		}
		
		public override void Deserialize(IDataReader reader)
		{
			base.Deserialize(reader);
			direction = reader.ReadSByte();
			if ( direction < 0 )
			{
				throw new Exception("Forbidden value on direction = " + direction + ", it doesn't respect the following condition : direction < 0");
			}
		}
	}
}
