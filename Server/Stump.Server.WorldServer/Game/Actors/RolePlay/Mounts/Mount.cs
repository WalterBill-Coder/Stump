﻿using NLog;
using Stump.Core.Attributes;
using Stump.DofusProtocol.Enums;
using Stump.DofusProtocol.Enums.Custom;
using Stump.DofusProtocol.Types;
using Stump.Server.WorldServer.Database.Mounts;
using Stump.Server.WorldServer.Game.Actors.RolePlay.Characters;
using Stump.Server.WorldServer.Handlers.Basic;
using Stump.Server.WorldServer.Handlers.Mounts;

namespace Stump.Server.WorldServer.Game.Actors.RolePlay.Mounts
{
    public class Mount
    {
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();

        private static readonly double[][] XP_PER_GAP =
        {
            new double[] {0, 10},
            new double[] {10, 8},
            new double[] {20, 6},
            new double[] {30, 4},
            new double[] {40, 3},
            new double[] {50, 2},
            new double[] {60, 1.5},
            new double[] {70, 1}
        };

        [Variable(true)]
        public static int RequiredLevel = 60;

        public Mount(Character character)
        {
            Record = MountManager.Instance.TryGetMount(character.Id);
            Level = ExperienceManager.Instance.GetMountLevel(Experience);
            ExperienceLevelFloor = ExperienceManager.Instance.GetMountLevelExperience(Level);
            ExperienceNextLevelFloor = ExperienceManager.Instance.GetMountNextLevelExperience(Level);
        }

        public Mount(MountRecord record)
        {
            Record = record;
            Level = ExperienceManager.Instance.GetMountLevel(Experience);
            ExperienceLevelFloor = ExperienceManager.Instance.GetMountLevelExperience(Level);
            ExperienceNextLevelFloor = ExperienceManager.Instance.GetMountNextLevelExperience(Level);
        }

        public Mount(Character character, int globalId, bool sex, int modelid)
        {
            Record = new MountRecord
            {
                Id = globalId,
                OwnerId = character.Id,
                IsNew = true,
                ModelId = modelid,
                State = MountStateEnum.EQUIPED, 
                Experience = ExperienceManager.Instance.GetMountLevelExperience(1)
            };
            Level = ExperienceManager.Instance.GetMountLevel(Experience);
            Sex = sex;
            Name = Model.Name;
        }

        #region Properties

        public MountRecord Record
        {
            get;
            set;
        }

        public bool IsDirty
        {
            get;
            set;
        }

        public int Id
        {
            get { return Record.Id; }
            private set { Record.Id = value; }
        }

        public int OwnerId
        {
            get { return Record.OwnerId; }
            private set { Record.OwnerId = value; }
        }

        public bool IsRiding
        {
            get;
            private set;
        }

        public bool Sex
        {
            get { return Record.Sex; }
            private set
            {
                Record.Sex = value;
                IsDirty = true;
            }

        }

        public MountStateEnum State
        {
            get { return Record.State; }
            set
            {
                Record.State = value;
                IsDirty = true;
            }
        }

        public MountTemplate Model
        {
            get { return Record.Model; }
        }

        public int ModelId
        {
            get { return Record.ModelId; }
            set
            {
                Record.ModelId = value;
                IsDirty = true;
            }
        }

        public byte Level
        {
            get;
            protected set;
        }

        public long Experience
        {
            get { return Record.Experience; }
            protected set
            {
                Record.Experience = value;
                IsDirty = true;
            }
        }

        public long ExperienceLevelFloor
        {
            get;
            protected set;
        }

        public long ExperienceNextLevelFloor
        {
            get;
            protected set;
        }

        public sbyte GivenExperience
        {
            get { return Record.GivenExperience; }
            protected set
            {
                Record.GivenExperience = value;
                IsDirty = true;
            }
        }

        public string Name
        {
            get { return Record.Name; }
            private set
            {
                Record.Name = value;
                IsDirty = true;
            }
        }

        public int Stamina
        {
            get { return Record.Stamina; }
            protected set
            {
                Record.Stamina = value;
                IsDirty = true;
            }
        }

        public int StaminaMax
        {
            get { return 10000; }
        }

        public int Maturity
        {
            get { return Record.Maturity; }
            protected set
            {
                Record.Maturity = value;
                IsDirty = true;
            }
        }

        public int MaturityForAdult
        {
            get { return 10000; }
        }

        public int Energy
        {
            get { return Record.Energy; }
            protected set
            {
                Record.Energy = value;
                IsDirty = true;
            }
        }

        public int EnergyMax
        {
            get { return 7400; }
        }

        public int Serenity
        {
            get { return Record.Serenity; }
            protected set
            {
                Record.Serenity = value;
                IsDirty = true;
            }
        }

        public int SerenityMax
        {
            get { return 10000; }
        }

        public int AggressivityMax
        {
            get { return -10000; }
        }

        public int Love
        {
            get { return Record.Love; }
            protected set
            {
                Record.Love = value;
                IsDirty = true;
            }
        }

        public int LoveMax
        {
            get { return 10000; }
        }

        public int ReproductionCount
        {
            get { return Record.ReproductionCount; }
            protected set
            {
                Record.ReproductionCount = value;
                IsDirty = true;
            }
        }

        public int ReproductionCountMax
        {
            get { return 80; }
        }

        public int PodsMax
        {
            get { return Record.Model.PodsBase + (Record.Model.PodsPerLevel * Level); }
        }

        public int FecondationTime
        {
            get { return 0; }
        }

        #endregion

        public void SetOwner(Character character)
        {
            OwnerId = character.Id;
        }

        public void RenameMount(Character character, string name)
        {
            Name = name;

            MountHandler.SendMountRenamedMessage(character.Client, Id, name);
        }

        public void Release(Character character, bool delete = true)
        {
            Dismount(character);

            MountHandler.SendMountUnSetMessage(character.Client);
            MountHandler.SendMountReleaseMessage(character.Client, character.Mount.Id);

            if (delete)
                MountManager.Instance.DeleteMount(character.Mount);

            character.Mount = null;
        }

        public void Sterelize(Character character)
        {
            character.Mount.ReproductionCount = -1;
            MountHandler.SendMountSterelizeMessage(character.Client, character.Mount.Id);
        }

        public void SetGivenExperience(Character character, sbyte xp)
        {
            GivenExperience = xp > 90 ? (sbyte)90 : (xp < 0 ? (sbyte)0 : xp);

            MountHandler.SendMountXpRatioMessage(character.Client, GivenExperience);
        }

        public void ToggleRiding(Character character)
        {
            if (character.IsBusy() || character.IsInFight())
            {
                //Une action est déjà en cours. Impossible de monter ou de descendre de votre monture.
                BasicHandler.SendTextInformationMessage(character.Client, TextInformationTypeEnum.TEXT_INFORMATION_ERROR, 355);
                return;
            }

            IsRiding = !IsRiding;

            character.RefreshActor();

            MountHandler.SendMountRidingMessage(character.Client, IsRiding);

            if (!IsRiding)
            {
                //Vous descendez de votre monture.
                BasicHandler.SendTextInformationMessage(character.Client, TextInformationTypeEnum.TEXT_INFORMATION_ERROR, 273);
            }
        }

        public void Dismount(Character character)
        {
            IsRiding = false;

            character.RefreshActor();

            MountHandler.SendMountRidingMessage(character.Client, false);
        }

        public void AddXP(Character character, long experience)
        {
            Experience += experience;

            var level = ExperienceManager.Instance.GetMountLevel(Experience);

            if (level == Level)
                return;

            Level = level;
            OnLevelChanged(character);
        }

        protected virtual void OnLevelChanged(Character character)
        {
            ExperienceLevelFloor = ExperienceManager.Instance.GetGuildLevelExperience(Level);
            ExperienceNextLevelFloor = ExperienceManager.Instance.GetGuildNextLevelExperience(Level);

            MountHandler.SendMountSetMessage(character.Client, GetMountClientData());
        }

        public long AdjustGivenExperience(Character giver, long amount)
        {
            var gap = giver.Level - Level;

            for (var i = XP_PER_GAP.Length - 1; i >= 0; i--)
            {
                if (gap > XP_PER_GAP[i][0])
                    return (long)(amount * XP_PER_GAP[i][1] * 0.01);
            }

            return (long)(amount * XP_PER_GAP[0][1] * 0.01);
        }

        #region Network

        public MountClientData GetMountClientData()
        {
            return new MountClientData
            {
                sex = Sex,
                isRideable = true,
                isWild = false,
                isFecondationReady = false,
                id = Id,
                model = Model.Id,
                ancestor = new int[0],
                behaviors = new int[0],
                name = Name,
                ownerId = Record.OwnerId,
                experience = Experience,
                experienceForLevel = ExperienceLevelFloor,
                experienceForNextLevel = ExperienceNextLevelFloor,
                level = (sbyte)Level,
                maxPods = PodsMax,
                stamina = Stamina,
                staminaMax = StaminaMax,
                maturity = Maturity,
                maturityForAdult = MaturityForAdult,
                energy = Energy,
                energyMax = EnergyMax,
                serenity = Serenity,
                serenityMax = SerenityMax,
                aggressivityMax = AggressivityMax,
                love = Love,
                loveMax = LoveMax,
                fecondationTime = FecondationTime,
                boostLimiter = 100,
                boostMax = 1000,
                reproductionCount = ReproductionCount,
                reproductionCountMax = ReproductionCountMax,
                effectList = new ObjectEffectInteger[0]
            };
        }

        public MountInformationsForPaddock GetMountInformationsForPaddock()
        {
            var character = CharacterManager.Instance.GetCharacterById(OwnerId);
            return new MountInformationsForPaddock((sbyte)Model.Id, Name, character.Name);
        }

        #endregion

        public void Save(ORM.Database database)
        {
            WorldServer.Instance.IOTaskPool.AddMessage(() =>
            {
                if (Record.IsNew)
                    database.Insert(Record);
                else
                    database.Update(Record);

                IsDirty = false;
                Record.IsNew = false;
            });
        }
    }
}