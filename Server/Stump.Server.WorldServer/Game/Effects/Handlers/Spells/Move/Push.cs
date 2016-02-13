using System.Linq;
using Stump.DofusProtocol.Enums;
using Stump.Server.WorldServer.Database.World;
using Stump.Server.WorldServer.Game.Actors.Fight;
using Stump.Server.WorldServer.Game.Effects.Instances;
using Stump.Server.WorldServer.Game.Fights.Buffs;
using Stump.Server.WorldServer.Handlers.Actions;

using Stump.Server.WorldServer.Game.Spells.Casts;
namespace Stump.Server.WorldServer.Game.Effects.Handlers.Spells.Move
{
    [EffectHandler(EffectsEnum.Effect_PushBack)]
    [EffectHandler(EffectsEnum.Effect_PushBack_1103)]
    public class Push : SpellEffectHandler
    {
        public Push(EffectDice effect, FightActor caster, SpellCastHandler castHandler, Cell targetedCell, bool critical)
            : base(effect, caster, castHandler, targetedCell, critical)
        {
            DamagesDisabled = effect.EffectId == EffectsEnum.Effect_PushBack_1103;
        }

        bool DamagesDisabled
        {
            get;
            set;
        }

        public FightActor SubRangeForActor
        {
            get;
            set;
        }

        public override bool Apply()
        {
            var integerEffect = GenerateEffect();

            if (integerEffect == null)
                return false;

            foreach (var actor in GetAffectedActors().OrderByDescending(entry => entry.Position.Point.ManhattanDistanceTo(TargetedPoint)))
            {
                if (actor.HasState((int)SpellStatesEnum.INDEPLACABLE_97) || actor.HasState((int)SpellStatesEnum.ENRACINE_6)
                    || actor.HasState((int)SpellStatesEnum.INEBRANLABLE_157))
                    continue;

                var referenceCell = TargetedCell.Id == actor.Cell.Id ? CastPoint : TargetedPoint;

                if (referenceCell.CellId == actor.Position.Cell.Id)
                    continue;

                var pushDirection = referenceCell.OrientationTo(actor.Position.Point);
                var startCell = actor.Position.Point;
                var lastCell = startCell;
                var range = SubRangeForActor == actor ? (integerEffect.Value - 1) : integerEffect.Value;
                var takeDamage = false;
                var stopCell = startCell.GetCellInDirection(pushDirection, range);
                var fightersInline = Fight.GetAllFightersInLine(startCell, range, pushDirection);


                if (fightersInline.Any())
                {
                    stopCell = fightersInline.First().Position.Point.GetCellInDirection(pushDirection, -1);

                    if (!DamagesDisabled)
                    {
                        var distance = range - (fightersInline.First().Position.Point.EuclideanDistanceTo(startCell) - 1);
                        var targets = 0;

                        foreach (var fighter in fightersInline)
                        {
                            var pushDamages = Formulas.FightFormulas.CalculatePushBackDamages(Caster, fighter, (int)distance, targets) / 2;

                            if (pushDamages > 0)
                            {
                                var pushDamage = new Fights.Damage(pushDamages)
                                {
                                    Source = actor,
                                    School = EffectSchoolEnum.Pushback,
                                    IgnoreDamageBoost = true,
                                    IgnoreDamageReduction = false
                                };

                                fighter.InflictDamage(pushDamage);
                            }

                            targets++;
                        }
                    }
                }
                else
                {
                    for (var i = 0; i < range; i++)
                    {
                        var nextCell = lastCell.GetNearestCellInDirection(pushDirection);

                        if (nextCell == null || !Fight.IsCellFree(Map.Cells[nextCell.CellId]) || Fight.ShouldTriggerOnMove(Fight.Map.Cells[nextCell.CellId], actor))
                        {
                            if (Fight.ShouldTriggerOnMove(Fight.Map.Cells[nextCell.CellId], actor))
                                DamagesDisabled = true;

                            stopCell = nextCell;
                            break;
                        }

                        if (nextCell != null)
                            lastCell = nextCell;
                    }
                }

                if (!DamagesDisabled)
                {
                    var pushbackDamages = Formulas.FightFormulas.CalculatePushBackDamages(Caster, actor, (range - (int)(startCell.EuclideanDistanceTo(stopCell))), 0);

                    if (pushbackDamages > 0)
                    {
                        var damage = new Fights.Damage(pushbackDamages)
                        {
                            Source = actor,
                            School = EffectSchoolEnum.Pushback,
                            IgnoreDamageBoost = true,
                            IgnoreDamageReduction = false
                        };

                        takeDamage = true;
                        actor.InflictDamage(damage);
                    }
                }

                if (actor.IsCarrying())
                    actor.ThrowActor(Map.Cells[startCell.CellId], true);

                foreach (var character in Fight.GetCharactersAndSpectators().Where(actor.IsVisibleFor))
                    ActionsHandler.SendGameActionFightSlideMessage(character.Client, Caster, actor, startCell.CellId, stopCell.CellId);

                actor.Position.Cell = Map.Cells[stopCell.CellId];
                actor.OnActorMoved(Caster, takeDamage);

                actor.TriggerBuffs(actor, BuffTriggerType.OnPush);
            }

            return true;
        }
    }
}