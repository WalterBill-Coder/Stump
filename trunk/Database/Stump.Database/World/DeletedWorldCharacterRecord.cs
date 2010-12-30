﻿// /*************************************************************************
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
using System;
using Castle.ActiveRecord;
using NHibernate.Criterion;

namespace Stump.Database
{
    [Serializable]
    [AttributeDatabase(DatabaseService.AuthServer)]
    [ActiveRecord("worlds_characters")]
    public sealed class WorldCharacterRecord : ActiveRecordBase<WorldCharacterRecord>
    {

        [PrimaryKey(PrimaryKeyType.Native, "Id")]
        public long Id
        {
            get;
            set;
        }


        [BelongsTo(Column = "AccountId")]
        public AccountRecord Account
        {
            get;
            set;
        }

        [BelongsTo(Column = "WorldId")]
        public WorldRecord World
        {
            get;
            set;
        }

        [KeyProperty(Column = "CharacterId")]
        public uint CharacterId
        {
            get;
            set;
        }

        public static WorldCharacterRecord FindCharactersByUniqueId(long uniqueId)
        {
            return FindByPrimaryKey(uniqueId);
        }

        public static WorldCharacterRecord[] FindCharactersByServer(int serverid)
        {
            return FindAll((Restrictions.Eq("ServerId", serverid)));
        }

        public static WorldCharacterRecord[] FindCharactersByAccountId(uint accountId)
        {
            return FindAll((Restrictions.Eq("AccountId", accountId)));
        }

        public static WorldCharacterRecord FindCharacterByServerIdAndCharacterId(int serverId, uint characterId)
        {
            return FindOne(Restrictions.And(Restrictions.Eq("ServerId", serverId), Restrictions.Eq("CharacterId", characterId)));
        }

        public static WorldCharacterRecord[] FindCharactersByAccountIdAnsServerId(uint accountid, int serverId)
        {
            return FindAll(Restrictions.And(Restrictions.Eq("AccountId",accountid),Restrictions.Eq("ServerId",serverId)));
        }
    }
}