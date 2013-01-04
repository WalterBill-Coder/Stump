
// Generated on 01/04/2013 14:36:00
using System;
using System.Collections.Generic;
using System.Linq;
using Stump.Core.IO;
using Stump.DofusProtocol.Types;

namespace Stump.DofusProtocol.Messages
{
    public class SpellListMessage : Message
    {
        public const uint Id = 1200;
        public override uint MessageId
        {
            get { return Id; }
        }
        
        public bool spellPrevisualization;
        public IEnumerable<Types.SpellItem> spells;
        
        public SpellListMessage()
        {
        }
        
        public SpellListMessage(bool spellPrevisualization, IEnumerable<Types.SpellItem> spells)
        {
            this.spellPrevisualization = spellPrevisualization;
            this.spells = spells;
        }
        
        public override void Serialize(IDataWriter writer)
        {
            writer.WriteBoolean(spellPrevisualization);
            writer.WriteUShort((ushort)spells.Count());
            foreach (var entry in spells)
            {
                 entry.Serialize(writer);
            }
        }
        
        public override void Deserialize(IDataReader reader)
        {
            spellPrevisualization = reader.ReadBoolean();
            var limit = reader.ReadUShort();
            spells = new Types.SpellItem[limit];
            for (int i = 0; i < limit; i++)
            {
                 (spells as Types.SpellItem[])[i] = new Types.SpellItem();
                 (spells as Types.SpellItem[])[i].Deserialize(reader);
            }
        }
        
    }
    
}