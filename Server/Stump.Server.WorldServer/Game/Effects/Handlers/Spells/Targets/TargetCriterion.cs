﻿using Stump.DofusProtocol.Enums;
using Stump.Server.WorldServer.Game.Actors.Fight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stump.Server.WorldServer.Game.Effects.Handlers.Spells.Targets
{
    public abstract class TargetCriterion
    {
        private static readonly Dictionary<char, SpellTargetType> m_targetsMapping = new Dictionary<char, SpellTargetType>()
        {
            {'C', SpellTargetType.SELF_ONLY},
            {'c', SpellTargetType.SELF},

            {'g', SpellTargetType.ALLY_PLAYER},
            {'s', SpellTargetType.ALLY_MONSTER_SUMMON},
            {'j', SpellTargetType.ALLY_SUMMON},
            {'i', SpellTargetType.ALLY_NON_MONSTER_SUMMON},
            {'d', SpellTargetType.ALLY_COMPANION},
            {'m', SpellTargetType.ALLY_MONSTER},
            {'h', SpellTargetType.ALLY_UNKN_1},
            {'l', SpellTargetType.ALLY_UNKN_2},

            {'a', SpellTargetType.ALLY_ALL},

            {'G', SpellTargetType.ENEMY_PLAYER},
            {'S', SpellTargetType.ENEMY_MONSTER_SUMMON},
            {'J', SpellTargetType.ENEMY_SUMMON},
            {'I', SpellTargetType.ENEMY_NON_MONSTER_SUMMON},
            {'D', SpellTargetType.ENEMY_COMPANION},
            {'M', SpellTargetType.ENEMY_MONSTER},
            {'H', SpellTargetType.ENEMY_UNKN_1},
            {'L', SpellTargetType.ENEMY_UNKN_2},

            {'A', SpellTargetType.ENEMY_ALL},
        };

        public abstract bool IsTargetValid(FightActor actor, SpellEffectHandler handler);

        public static TargetCriterion ParseCriterion(string str)
        {
            try
            {
                if (m_targetsMapping.ContainsKey(str[0]))
                {
                    return new TargetTypeCriterion(m_targetsMapping[str[0]]);
                }
                bool caster = str[0] == '*';

                if (caster)
                    str = str.Remove(0, 1);

                switch(str[0])
                {
                    case 'e':
                        return new StateCriterion(int.Parse(str.Remove(0, 1)), caster, false);
                    case 'E':
                        return new StateCriterion(int.Parse(str.Remove(0, 1)), caster, true);
                    case 'f':
                        return new MonsterCriterion(int.Parse(str.Remove(0, 1)), false);
                    case 'F':
                        return new MonsterCriterion(int.Parse(str.Remove(0, 1)), true);
                    case 'v':
                        return new LifeCriterion(int.Parse(str.Remove(0, 1)), true);
                    case 'V':
                        return new LifeCriterion(int.Parse(str.Remove(0, 1)), false);
                }

                return new UnknownCriterion(str);
            }
            catch(Exception ex)
            {
                throw new Exception("Invalid target criterion : " + str, ex);
            }
        }
    }
}