
// Generated on 01/04/2013 14:35:53
using System;
using System.Collections.Generic;
using System.Linq;
using Stump.Core.IO;
using Stump.DofusProtocol.Types;

namespace Stump.DofusProtocol.Messages
{
    public class SpouseGetInformationsMessage : Message
    {
        public const uint Id = 6355;
        public override uint MessageId
        {
            get { return Id; }
        }
        
        
        public SpouseGetInformationsMessage()
        {
        }
        
        
        public override void Serialize(IDataWriter writer)
        {
        }
        
        public override void Deserialize(IDataReader reader)
        {
        }
        
    }
    
}