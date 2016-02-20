﻿using Stump.DofusProtocol.Enums;
using Stump.Server.WorldServer.Database.World;
using Stump.Server.WorldServer.Game.Actors.Fight;
using Stump.Server.WorldServer.Game.Fights.Buffs;

namespace Stump.Server.WorldServer.Game.Spells.Casts.Monsters
{
    [SpellCastHandler(SpellIdEnum.VILAIN_GARNEMENT)]
    public class NaughtyChildCastHandler : DefaultSpellCastHandler
    {
        public NaughtyChildCastHandler(FightActor caster, Spell spell, Cell targetedCell, bool critical)
            : base(caster, spell, targetedCell, critical)
        {
        }

        public override void Execute()
        {
            if (!m_initialized)
                Initialize();

            var buffId = Caster.PopNextBuffId();
            var effect = Spell.CurrentSpellLevel.Effects[0];

            var buff = new TriggerBuff(buffId, Caster, Caster, effect, Spell, Spell, false, FightDispellableEnum.DISPELLABLE_BY_DEATH, 0, SpellBuffTrigger)
            {
                Duration = (short)effect.Duration
            };
            buff.SetTrigger(BuffTriggerType.OnMPLost);

            Caster.AddBuff(buff);
        }

        private void SpellBuffTrigger(TriggerBuff buff, FightActor triggerrer, BuffTriggerType trigger, object token)
        {
            Handlers[0].Apply();
        }
    }
}
