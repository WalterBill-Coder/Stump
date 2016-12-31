﻿using System;
using System.Collections.Generic;
using System.Linq;
using NLog;
using Stump.DofusProtocol.Enums;
using Stump.Server.WorldServer.Database.Items;
using Stump.Server.WorldServer.Database.Items.Templates;
using Stump.Server.WorldServer.Game.Actors.RolePlay.Characters;
using Stump.Server.WorldServer.Game.Effects.Instances;
using Stump.Server.WorldServer.Game.Actors.Look;

namespace Stump.Server.WorldServer.Game.Items.Player.Custom.LivingObjects
{
    [ItemHasEffect(EffectsEnum.Effect_LivingObjectId)]
    public sealed class BoundLivingObjectItem : CommonLivingObject
    {
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();


        private readonly ItemTemplate m_livingObjectTemplate;

        public BoundLivingObjectItem(Character owner, PlayerItemRecord record)
            : base(owner, record)
        {
            var idEffect = (EffectInteger) Effects.First(x => x.EffectId == EffectsEnum.Effect_LivingObjectId);
            m_livingObjectTemplate = ItemManager.Instance.TryGetTemplate(idEffect.Value);
            LivingObjectRecord = ItemManager.Instance.TryGetLivingObjectRecord(idEffect.Value);

            if (LivingObjectRecord == null || m_livingObjectTemplate == null)
                logger.Error("Living Object {0} has no template", Template.Id);

            Initialize();
        }

        public override bool CanFeed(BasePlayerItem item) => false;

        public override bool Feed(BasePlayerItem food)
        {
            if (food.Template.TypeId != LivingObjectRecord.ItemType)
                return false;

            var xp = (short)Math.Ceiling(food.Template.Level/2d);
            Experience += xp;

            // todo, manage it
            Mood = 1;
            LastMeal = DateTime.Now;

            Owner.Inventory.RefreshItem(this);

            return true;
        }

        public void Dissociate()
        {
            if (Owner.IsInExchange())
                return;

            var effects = new List<EffectBase> { MoodEffect, ExperienceEffect, CategoryEffect, SelectedLevelEffect };
            var effectsLiving = new List<EffectBase> { MoodEffect, ExperienceEffect, CategoryEffect, SelectedLevelEffect };

            if (LastMealEffect != null)
            {
                effects.Add(LastMealEffect);
                effectsLiving.Add(LastMealEffect);
            }

            if (!Template.Effects.Exists(x => x.EffectId == EffectsEnum.Effect_NonExchangeable_982))
                effects.Add(new EffectInteger(EffectsEnum.Effect_NonExchangeable_982, 0));

            if (!Template.Effects.Exists(x => x.EffectId == EffectsEnum.Effect_NonExchangeable_981))
                effects.Add(new EffectInteger(EffectsEnum.Effect_NonExchangeable_981, 0));

            Effects.RemoveAll(effects.Contains);
            Effects.RemoveAll(x => x.EffectId == EffectsEnum.Effect_LivingObjectId);
            var newInstance = Owner.Inventory.RefreshItemInstance(this);

            var livingObject = ItemManager.Instance.CreatePlayerItem(Owner, m_livingObjectTemplate, 1, effectsLiving);

            if (!livingObject.Effects.Any(x => x.EffectId == EffectsEnum.Effect_NonExchangeable_982))
                livingObject.Effects.Add(new EffectInteger(EffectsEnum.Effect_NonExchangeable_982, 1));

            Owner.Inventory.AddItem(livingObject);

            Owner.UpdateLook();
            newInstance.OnObjectModified();
        }
    }
}