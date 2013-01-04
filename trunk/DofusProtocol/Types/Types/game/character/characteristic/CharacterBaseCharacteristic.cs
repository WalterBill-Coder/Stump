
// Generated on 01/04/2013 14:36:03
using System;
using System.Collections.Generic;
using System.Linq;
using Stump.Core.IO;

namespace Stump.DofusProtocol.Types
{
    public class CharacterBaseCharacteristic
    {
        public const short Id = 4;
        public virtual short TypeId
        {
            get { return Id; }
        }
        
        public short @base;
        public short objectsAndMountBonus;
        public short alignGiftBonus;
        public short contextModif;
        
        public CharacterBaseCharacteristic()
        {
        }
        
        public CharacterBaseCharacteristic(short @base, short objectsAndMountBonus, short alignGiftBonus, short contextModif)
        {
            this.@base = @base;
            this.objectsAndMountBonus = objectsAndMountBonus;
            this.alignGiftBonus = alignGiftBonus;
            this.contextModif = contextModif;
        }
        
        public virtual void Serialize(IDataWriter writer)
        {
            writer.WriteShort(@base);
            writer.WriteShort(objectsAndMountBonus);
            writer.WriteShort(alignGiftBonus);
            writer.WriteShort(contextModif);
        }
        
        public virtual void Deserialize(IDataReader reader)
        {
            @base = reader.ReadShort();
            objectsAndMountBonus = reader.ReadShort();
            alignGiftBonus = reader.ReadShort();
            contextModif = reader.ReadShort();
        }
        
    }
    
}