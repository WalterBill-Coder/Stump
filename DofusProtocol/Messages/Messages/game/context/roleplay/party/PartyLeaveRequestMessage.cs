

// Generated on 08/04/2015 00:37:08
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Stump.Core.IO;
using Stump.DofusProtocol.Types;

namespace Stump.DofusProtocol.Messages
{
    public class PartyLeaveRequestMessage : AbstractPartyMessage
    {
        public const uint Id = 5593;
        public override uint MessageId
        {
            get { return Id; }
        }
        
        
        public PartyLeaveRequestMessage()
        {
        }
        
        public PartyLeaveRequestMessage(int partyId)
         : base(partyId)
        {
        }
        
        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
        }
        
        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
        }
        
    }
    
}