

// Generated on 09/26/2016 01:49:54
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Stump.Core.IO;
using Stump.DofusProtocol.Types;

namespace Stump.DofusProtocol.Messages
{
    public class CharacterSelectedErrorMessage : Message
    {
        public const uint Id = 5836;
        public override uint MessageId
        {
            get { return Id; }
        }
        
        
        public CharacterSelectedErrorMessage()
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