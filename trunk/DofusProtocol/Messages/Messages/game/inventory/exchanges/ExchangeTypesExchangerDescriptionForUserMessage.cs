
// Generated on 01/04/2013 14:35:58
using System;
using System.Collections.Generic;
using System.Linq;
using Stump.Core.IO;
using Stump.DofusProtocol.Types;

namespace Stump.DofusProtocol.Messages
{
    public class ExchangeTypesExchangerDescriptionForUserMessage : Message
    {
        public const uint Id = 5765;
        public override uint MessageId
        {
            get { return Id; }
        }
        
        public IEnumerable<int> typeDescription;
        
        public ExchangeTypesExchangerDescriptionForUserMessage()
        {
        }
        
        public ExchangeTypesExchangerDescriptionForUserMessage(IEnumerable<int> typeDescription)
        {
            this.typeDescription = typeDescription;
        }
        
        public override void Serialize(IDataWriter writer)
        {
            writer.WriteUShort((ushort)typeDescription.Count());
            foreach (var entry in typeDescription)
            {
                 writer.WriteInt(entry);
            }
        }
        
        public override void Deserialize(IDataReader reader)
        {
            var limit = reader.ReadUShort();
            typeDescription = new int[limit];
            for (int i = 0; i < limit; i++)
            {
                 (typeDescription as int[])[i] = reader.ReadInt();
            }
        }
        
    }
    
}