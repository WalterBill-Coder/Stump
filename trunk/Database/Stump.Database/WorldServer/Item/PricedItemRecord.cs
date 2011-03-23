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
using Castle.ActiveRecord;
using Stump.Database.WorldServer.Storage;

namespace Stump.Database.WorldServer.Item
{
    [ActiveRecord("priceditems"), JoinedBase]
    public class PricedItemRecord : ItemRecord
    {
        [JoinedKey("ItemGuid")]
        private long ItemGuid { get; set; }

        [BelongsTo("CharacterId", NotNull = true)]
        public SellBagRecord SellBag { get; set; }

        [Property("Price")]
        public uint Price { get; set; }
    }
}