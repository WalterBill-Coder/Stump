
// Generated on 01/04/2013 14:35:55
using System;
using System.Collections.Generic;
using System.Linq;
using Stump.Core.IO;
using Stump.DofusProtocol.Types;

namespace Stump.DofusProtocol.Messages
{
    public class KamasUpdateMessage : Message
    {
        public const uint Id = 5537;
        public override uint MessageId
        {
            get { return Id; }
        }
        
        public int kamasTotal;
        
        public KamasUpdateMessage()
        {
        }
        
        public KamasUpdateMessage(int kamasTotal)
        {
            this.kamasTotal = kamasTotal;
        }
        
        public override void Serialize(IDataWriter writer)
        {
            writer.WriteInt(kamasTotal);
        }
        
        public override void Deserialize(IDataReader reader)
        {
            kamasTotal = reader.ReadInt();
        }
        
    }
    
}