﻿using Stump.DofusProtocol.Enums.Custom;
using Stump.Server.WorldServer.Game.Actors.Fight;

namespace Stump.Server.WorldServer.Game.Fights.Challenges.Custom
{
    [ChallengeIdentifier((int)ChallengeEnum.INTOUCHABLE)]
    public class UntouchableChallenge : DefaultChallenge
    {
        public UntouchableChallenge(IFight fight)
            : base(fight)
        {
        }

        public UntouchableChallenge(int id, IFight fight)
            : base(id, fight)
        {
            Bonus = 40;

            foreach (var fighter in Fight.GetAllFighters<CharacterFighter>())
            {
                fighter.BeforeDamageInflicted += OnBeforeDamageInflicted;
            }
        }

        private void OnBeforeDamageInflicted(FightActor fighter, Damage damage)
        {
            UpdateStatus(ChallengeStatusEnum.FAILED, fighter);
        }
    }
}