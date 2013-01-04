
// Generated on 01/04/2013 14:35:39
using System;
using System.Collections.Generic;
using System.Linq;
using Stump.Core.IO;
using Stump.DofusProtocol.Types;

namespace Stump.DofusProtocol.Messages
{
    public class ServerSelectionMessage : Message
    {
        public const uint Id = 40;
        public override uint MessageId
        {
            get { return Id; }
        }
        
        public short serverId;
        
        public ServerSelectionMessage()
        {
        }
        
        public ServerSelectionMessage(short serverId)
        {
            this.serverId = serverId;
        }
        
        public override void Serialize(IDataWriter writer)
        {
            writer.WriteShort(serverId);
        }
        
        public override void Deserialize(IDataReader reader)
        {
            serverId = reader.ReadShort();
        }
        
    }
    
}