﻿using System;
using System.Collections.Generic;
using System.Linq;
using Stump.DofusProtocol.Enums;
using Stump.DofusProtocol.Messages;
using Stump.Server.BaseServer.Network;
using Stump.Server.WorldServer.Game.Actors.RolePlay.Characters;
using Stump.Server.WorldServer.Game.Interactives;
using Stump.Server.WorldServer.Game.Maps;
using Stump.Server.WorldServer.Handlers.Dialogs;

namespace Stump.Server.WorldServer.Game.Dialogs.Interactives
{
    public class ZaapDialog : IDialog
    {
        private List<Map> m_destinations = new List<Map>();

        public ZaapDialog(Character character, InteractiveObject zaap)
        {
            Character = character;
            Zaap = zaap;
        }

        public ZaapDialog(Character character, InteractiveObject zaap, IEnumerable<Map> destinations)
        {
            Character = character;
            Zaap = zaap;
            m_destinations = destinations.ToList();
        }

        public Character Character
        {
            get;
            private set;
        }

        public InteractiveObject Zaap
        {
            get;
            private set;
        }

        public void AddDestination(Map map)
        {
            m_destinations.Add(map);
        }

        public void Open()
        {
            Character.SetDialog(this);
            SendZaapListMessage(Character.Client);
        }

        public void Close()
        {
            Character.ResetDialog();
            DialogHandler.SendLeaveDialogMessage(Character.Client);
        }

        public void Teleport(Map map)
        {
            if (!m_destinations.Contains(map))
                return;

            var cell = map.Cells[280];

            if (map.Zaap != null)
                cell = map.Zaap.Position.Cell;

            Character.Teleport(map, cell);
        }

        public void SendZaapListMessage(IPacketReceiver client)
        {
            client.Send(new ZaapListMessage((sbyte)TeleporterTypeEnum.TELEPORTER_ZAAP,
                m_destinations.Select(entry => entry.Id),
                m_destinations.Select(entry => (short)entry.SubArea.Id),
                m_destinations.Select(GetCostTo),
                Zaap.Map.Id));
        }

        public short GetCostTo(Map map)
        {
            var pos = map.Position;
            var pos2 = Zaap.Map.Position;

            return (short)Math.Floor(Math.Sqrt(( pos2.X - pos.X ) * ( pos2.X - pos.X ) + ( pos2.Y - pos.Y ) * ( pos2.Y - pos.Y )) * 10);
        }
    }
}