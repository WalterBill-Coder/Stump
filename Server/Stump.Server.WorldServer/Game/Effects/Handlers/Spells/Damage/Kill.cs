﻿using Stump.DofusProtocol.Enums;
using Stump.Server.WorldServer.Database.World;
using Stump.Server.WorldServer.Game.Actors.Fight;
using Stump.Server.WorldServer.Game.Effects.Instances;using Stump.Server.WorldServer.Game.Fights.Buffs;
using Stump.Server.WorldServer.Game.Spells.Casts;
using Stump.Server.WorldServer.Handlers.Context;

namespace Stump.Server.WorldServer.Game.Effects.Handlers.Spells.Damage
{
    [EffectHandler(EffectsEnum.Effect_Kill)]
    public class Kill : SpellEffectHandler
    {
        public Kill(EffectDice effect, FightActor caster, SpellCastHandler castHandler, Cell targetedCell, bool critical)
            : base(effect, caster, castHandler, targetedCell, critical)
        {
        }

        public override bool Apply()
        {
            foreach (var actor in GetAffectedActors())
            {
                if (Effect.Duration != 0 || Effect.Delay != 0)
                {
                    AddTriggerBuff(actor, true, KillTrigger);
                }
                else
                {
                    actor.Stats.Health.DamageTaken = int.MaxValue;
                    actor.CheckDead(Caster);

                    ContextHandler.SendGameActionFightKillMessage(Fight.Clients, Caster, actor);
                }
            }

            return true;
        }

        void KillTrigger(TriggerBuff buff, FightActor triggerrer, BuffTriggerType trigger, object token)
        {
            buff.Target.Stats.Health.DamageTaken = int.MaxValue;
            buff.Target.CheckDead(buff.Caster);

            ContextHandler.SendGameActionFightKillMessage(Fight.Clients, buff.Caster, buff.Target);
        }
    }
}