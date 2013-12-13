

// Generated on 12/12/2013 16:57:03
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Stump.Core.IO;
using Stump.DofusProtocol.Types;

namespace Stump.DofusProtocol.Messages
{
    public class ObjectGroundRemovedMultipleMessage : Message
    {
        public const uint Id = 5944;
        public override uint MessageId
        {
            get { return Id; }
        }
        
        public IEnumerable<short> cells;
        
        public ObjectGroundRemovedMultipleMessage()
        {
        }
        
        public ObjectGroundRemovedMultipleMessage(IEnumerable<short> cells)
        {
            this.cells = cells;
        }
        
        public override void Serialize(IDataWriter writer)
        {
            writer.WriteUShort((ushort)cells.Count());
            foreach (var entry in cells)
            {
                 writer.WriteShort(entry);
            }
        }
        
        public override void Deserialize(IDataReader reader)
        {
            var limit = reader.ReadUShort();
            cells = new short[limit];
            for (int i = 0; i < limit; i++)
            {
                 (cells as short[])[i] = reader.ReadShort();
            }
        }
        
        public override int GetSerializationSize()
        {
            return sizeof(short) + cells.Sum(x => sizeof(short));
        }
        
    }
    
}