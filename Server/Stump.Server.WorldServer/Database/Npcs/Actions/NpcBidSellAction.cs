﻿using System;
using System.Collections.Generic;
using System.Linq;
using Stump.DofusProtocol.Enums;
using Stump.Server.BaseServer.Database;
using Stump.Server.WorldServer.Game.Actors.RolePlay.Characters;
using Stump.Server.WorldServer.Game.Actors.RolePlay.Npcs;
using Stump.Server.WorldServer.Game.Dialogs.Npcs;

namespace Stump.Server.WorldServer.Database.Npcs.Actions
{
    [Discriminator(Discriminator, typeof(NpcActionDatabase), typeof(NpcActionRecord))]
    public class NpcBidSellAction : NpcActionDatabase
    {
        public const string Discriminator = "BidSell";

        public NpcBidSellAction(NpcActionRecord record)
            : base(record)
        {
        }

        public override NpcActionTypeEnum ActionType
        {
            get { return NpcActionTypeEnum.ACTION_SELL; }
        }


        private IEnumerable<int> m_types;

        /// <summary>
        /// Parameter 0
        /// </summary>
        private string TypesId
        {
            get { return Record.GetParameter<string>(0); }
            set { Record.SetParameter(0, value); }
        }

        public IEnumerable<int> Types
        {
            get { return m_types ?? (m_types = TypesId.Split('|').Select(int.Parse)); }
            set
            {
                m_types = value;
                TypesId = String.Join("|", value);
            }
        }

        public override void Execute(Npc npc, Character character)
        {
            var dialog = new NpcBidDialog(character, npc, Types, false);
            dialog.Open();
        }
    }
}
