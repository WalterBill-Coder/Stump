﻿using Stump.DofusProtocol.Enums.Custom;
using Stump.Server.WorldServer.Game.Actors.Fight;

namespace Stump.Server.WorldServer.Game.Fights.Challenges.Custom
{
    [ChallengeIdentifier((int)ChallengeEnum.ABNÉGATION)]
    public class SelfSacrificeChallenge : DefaultChallenge
    {
        public SelfSacrificeChallenge(IFight fight)
            : base(fight)
        {
        }

        public SelfSacrificeChallenge(int id, IFight fight)
            : base(id, fight)
        {
            Bonus = 10;

            foreach (var fighter in Fight.GetAllFighters<CharacterFighter>())
            {
                fighter.LifePointsChanged += OnLifePointsChanged;
            }
        }

        private void OnLifePointsChanged(FightActor fighter, int delta, int shieldDamages, int permanentDamages, FightActor from)
        {
            if (delta > 0 && Fight.FighterPlaying == fighter)
                UpdateStatus(ChallengeStatusEnum.FAILED, fighter);
        }
    }
}
