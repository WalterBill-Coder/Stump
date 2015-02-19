﻿using Stump.DofusProtocol.Enums;
using Stump.Server.WorldServer.Database.Items;
using Stump.Server.WorldServer.Game.Actors.RolePlay.Characters;

namespace Stump.Server.WorldServer.Game.Items.Player.Custom
{
    [ItemType(ItemTypeEnum.PETSMOUNT)]
    public class PetItem : BasePlayerItem
    {
        public PetItem(Character owner, PlayerItemRecord record)
            : base(owner, record)
        {
        }

        public override bool OnEquipItem(bool unequip)
        {
            if (unequip)
                return base.OnEquipItem(true);


            if (Owner.HasEquipedMount())
                Owner.Inventory.MoveItem(this, CharacterInventoryPositionEnum.INVENTORY_POSITION_NOT_EQUIPED);

            return true;
        }
    }
}