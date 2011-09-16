// File generated by 'DofusProtocolBuilder.exe v1.0.0.0'
// From 'FightTemporaryBoostWeaponDamagesEffect.xml' the '22/08/2011 17:23:04'
using System;
using Stump.Core.IO;

namespace Stump.DofusProtocol.Types
{
	public class FightTemporaryBoostWeaponDamagesEffect : FightTemporaryBoostEffect
	{
		public const uint Id = 211;
		public override short TypeId
		{
			get
			{
				return 211;
			}
		}
		
		public short weaponTypeId;
		
		public FightTemporaryBoostWeaponDamagesEffect()
		{
		}
		
		public FightTemporaryBoostWeaponDamagesEffect(int uid, int targetId, short turnDuration, sbyte dispelable, short spellId, short delta, short weaponTypeId)
			 : base(uid, targetId, turnDuration, dispelable, spellId, delta)
		{
			this.weaponTypeId = weaponTypeId;
		}
		
		public override void Serialize(IDataWriter writer)
		{
			base.Serialize(writer);
			writer.WriteShort(weaponTypeId);
		}
		
		public override void Deserialize(IDataReader reader)
		{
			base.Deserialize(reader);
			weaponTypeId = reader.ReadShort();
		}
	}
}