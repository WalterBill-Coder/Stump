// /*************************************************************************
//  *
//  *  Copyright (C) 2010 - 2011 Stump Team
//  *
//  *  This program is free software: you can redistribute it and/or modify
//  *  it under the terms of the GNU General Public License as published by
//  *  the Free Software Foundation, either version 3 of the License, or
//  *  (at your option) any later version.
//  *
//  *  This program is distributed in the hope that it will be useful,
//  *  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  *  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//  *  GNU General Public License for more details.
//  *
//  *  You should have received a copy of the GNU General Public License
//  *  along with this program.  If not, see <http://www.gnu.org/licenses/>.
//  *
//  *************************************************************************/
using System.Linq;
using Stump.DofusProtocol.Classes;
using Stump.DofusProtocol.Enums;
using Stump.DofusProtocol.Messages;
using Stump.Server.WorldServer.Entities;
using Stump.Server.WorldServer.Fights;
using Stump.Server.WorldServer.Groups;

namespace Stump.Server.WorldServer.Handlers
{
    public partial class ContextHandler
    {
        public static void SendGameRolePlayPlayerFightFriendlyAnsweredMessage(WorldClient client, Fight fight,
                                                                              bool accepted)
        {
            client.Send(new GameRolePlayPlayerFightFriendlyAnsweredMessage(fight.SourceGroup.Id,
                                                                           (uint) fight.SourceGroup.Leader.Entity.Id,
                                                                           (uint) fight.TargetGroup.Leader.Entity.Id,
                                                                           accepted));
        }

        public static void SendGameRolePlayPlayerFightFriendlyRequestedMessage(WorldClient client, Entity source,
                                                                               Entity target, uint groupId)
        {
            client.Send(new GameRolePlayPlayerFightFriendlyRequestedMessage(groupId, (uint) source.Id, (uint) target.Id));
        }
    }
}