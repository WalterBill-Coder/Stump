

// Generated on 08/04/2015 13:25:00
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Stump.Core.IO;
using Stump.DofusProtocol.Types;

namespace Stump.DofusProtocol.Messages
{
    public class GameRolePlayArenaSwitchToGameServerMessage : Message
    {
        public const uint Id = 6574;
        public override uint MessageId
        {
            get { return Id; }
        }
        
        
        public GameRolePlayArenaSwitchToGameServerMessage()
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