
// Generated on 01/04/2013 14:35:42
using System;
using System.Collections.Generic;
using System.Linq;
using Stump.Core.IO;
using Stump.DofusProtocol.Types;

namespace Stump.DofusProtocol.Messages
{
    public class ServerOptionalFeaturesMessage : Message
    {
        public const uint Id = 6305;
        public override uint MessageId
        {
            get { return Id; }
        }
        
        public IEnumerable<short> features;
        
        public ServerOptionalFeaturesMessage()
        {
        }
        
        public ServerOptionalFeaturesMessage(IEnumerable<short> features)
        {
            this.features = features;
        }
        
        public override void Serialize(IDataWriter writer)
        {
            writer.WriteUShort((ushort)features.Count());
            foreach (var entry in features)
            {
                 writer.WriteShort(entry);
            }
        }
        
        public override void Deserialize(IDataReader reader)
        {
            var limit = reader.ReadUShort();
            features = new short[limit];
            for (int i = 0; i < limit; i++)
            {
                 (features as short[])[i] = reader.ReadShort();
            }
        }
        
    }
    
}