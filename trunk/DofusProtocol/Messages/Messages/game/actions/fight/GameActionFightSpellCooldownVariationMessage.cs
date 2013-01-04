
// Generated on 01/04/2013 14:35:41
using System;
using System.Collections.Generic;
using System.Linq;
using Stump.Core.IO;
using Stump.DofusProtocol.Types;

namespace Stump.DofusProtocol.Messages
{
    public class GameActionFightSpellCooldownVariationMessage : AbstractGameActionMessage
    {
        public const uint Id = 6219;
        public override uint MessageId
        {
            get { return Id; }
        }
        
        public int targetId;
        public int spellId;
        public short value;
        
        public GameActionFightSpellCooldownVariationMessage()
        {
        }
        
        public GameActionFightSpellCooldownVariationMessage(short actionId, int sourceId, int targetId, int spellId, short value)
         : base(actionId, sourceId)
        {
            this.targetId = targetId;
            this.spellId = spellId;
            this.value = value;
        }
        
        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            writer.WriteInt(targetId);
            writer.WriteInt(spellId);
            writer.WriteShort(value);
        }
        
        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            targetId = reader.ReadInt();
            spellId = reader.ReadInt();
            if (spellId < 0)
                throw new Exception("Forbidden value on spellId = " + spellId + ", it doesn't respect the following condition : spellId < 0");
            value = reader.ReadShort();
        }
        
    }
    
}