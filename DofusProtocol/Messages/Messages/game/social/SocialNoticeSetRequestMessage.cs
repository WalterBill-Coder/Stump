

// Generated on 09/26/2016 01:50:17
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Stump.Core.IO;
using Stump.DofusProtocol.Types;

namespace Stump.DofusProtocol.Messages
{
    public class SocialNoticeSetRequestMessage : Message
    {
        public const uint Id = 6686;
        public override uint MessageId
        {
            get { return Id; }
        }
        
        
        public SocialNoticeSetRequestMessage()
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