

// Generated on 04/19/2016 10:17:38
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Stump.Core.IO;
using Stump.DofusProtocol.Types;

namespace Stump.DofusProtocol.Messages
{
    public class StorageKamasUpdateMessage : Message
    {
        public const uint Id = 5645;
        public override uint MessageId
        {
            get { return Id; }
        }
        
        public int kamasTotal;
        
        public StorageKamasUpdateMessage()
        {
        }
        
        public StorageKamasUpdateMessage(int kamasTotal)
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