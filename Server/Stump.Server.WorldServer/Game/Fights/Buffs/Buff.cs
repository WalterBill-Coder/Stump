using Stump.DofusProtocol.Types;
using Stump.Server.WorldServer.Game.Actors.Fight;
using Stump.Server.WorldServer.Game.Effects.Instances;
using Stump.Server.WorldServer.Game.Spells;

namespace Stump.Server.WorldServer.Game.Fights.Buffs
{
    public abstract class Buff
    {
       
        public const int CHARACTERISTIC_STATE = 71;

        protected Buff(int id, FightActor target, FightActor caster, EffectBase effect, Spell spell, bool critical, bool dispelable)
            : this(id, target,caster,effect,spell,critical, dispelable, null)
        {
        }

        protected Buff(int id, FightActor target, FightActor caster, EffectBase effect, Spell spell, bool critical, bool dispelable, short? customActionId)
        {
            Id = id;
            Target = target;
            Caster = caster;
            Effect = effect;
            Spell = spell;
            Critical = critical;
            Dispellable = dispelable;
            CustomActionId = customActionId;

            Duration = (short)Effect.Duration;
            Delay = (short)Effect.Delay;

            Efficiency = 1.0d;
        }

        public int Id
        {
            get;
            private set;
        }

        public FightActor Target
        {
            get;
            private set;
        }

        public FightActor Caster
        {
            get;
            private set;
        }

        public FightActor PlayerOnCreation
        {
            get;
            set;
        }

        public EffectBase Effect
        {
            get;
            private set;
        }

        public Spell Spell
        {
            get;
            private set;
        }

        public short Duration
        {
            get;
            set;
        }

        public short Delay
        {
            get;
            set;
        }

        public bool Critical
        {
            get;
            private set;
        }

        public bool Dispellable
        {
            get;
            private set;
        }

        public short? CustomActionId
        {
            get;
            private set;
        }

        /// <summary>
        /// Efficiency factor, increase or decrease effect's efficiency. Default is 1.0
        /// </summary>
        public double Efficiency
        {
            get;
            set;
        }

        public virtual BuffType Type
        {
            get
            {
                if (Effect.Template.Characteristic == CHARACTERISTIC_STATE)
                    return BuffType.CATEGORY_STATE;

                if (Effect.Template.Operator == "-")
                    return Effect.Template.Active ? BuffType.CATEGORY_ACTIVE_MALUS : BuffType.CATEGORY_PASSIVE_MALUS;

                if (Effect.Template.Operator == "+")
                    return  Effect.Template.Active ? BuffType.CATEGORY_ACTIVE_BONUS : BuffType.CATEGORY_PASSIVE_BONUS;

                return BuffType.CATEGORY_OTHER;
            }
        }

        /// <summary>
        /// Decrement Duration and return true whenever the buff is over
        /// </summary>
        /// <returns></returns>
        public bool DecrementDuration()
        {
            if (Duration == -1) // Duration = -1 => unlimited buff
                return false;

            if (Delay > 0)
            {
                if (--Delay == 0)
                    Apply();

                return false;
            }

            return --Duration == 0;
        }
        
        public abstract void Apply();
        public abstract void Dispell();

        public virtual short GetActionId()
        {
            if (CustomActionId.HasValue)
                return CustomActionId.Value;

            return (short) Effect.EffectId;
        }

        public EffectInteger GenerateEffect()
        {
            var effect = Effect.GenerateEffect(EffectGenerationContext.Spell) as EffectInteger;

            if (effect != null)
                effect.Value = (short)( effect.Value * Efficiency );

            return effect;
        }

        public FightDispellableEffectExtendedInformations GetFightDispellableEffectExtendedInformations()
        {
            return new FightDispellableEffectExtendedInformations(GetActionId(), Caster.Id, GetAbstractFightDispellableEffect());
        }

        public abstract AbstractFightDispellableEffect GetAbstractFightDispellableEffect();

    }
}