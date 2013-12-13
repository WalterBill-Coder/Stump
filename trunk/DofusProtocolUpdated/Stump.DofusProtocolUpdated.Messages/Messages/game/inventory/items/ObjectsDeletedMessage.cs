

// Generated on 12/12/2013 16:57:20
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Stump.Core.IO;
using Stump.DofusProtocol.Types;

namespace Stump.DofusProtocol.Messages
{
    public class ObjectsDeletedMessage : Message
    {
        public const uint Id = 6034;
        public override uint MessageId
        {
            get { return Id; }
        }
        
        public IEnumerable<int> objectUID;
        
        public ObjectsDeletedMessage()
        {
        }
        
        public ObjectsDeletedMessage(IEnumerable<int> objectUID)
        {
            this.objectUID = objectUID;
        }
        
        public override void Serialize(IDataWriter writer)
        {
            writer.WriteUShort((ushort)objectUID.Count());
            foreach (var entry in objectUID)
            {
                 writer.WriteInt(entry);
            }
        }
        
        public override void Deserialize(IDataReader reader)
        {
            var limit = reader.ReadUShort();
            objectUID = new int[limit];
            for (int i = 0; i < limit; i++)
            {
                 (objectUID as int[])[i] = reader.ReadInt();
            }
        }
        
        public override int GetSerializationSize()
        {
            return sizeof(short) + objectUID.Sum(x => sizeof(int));
        }
        
    }
    
}