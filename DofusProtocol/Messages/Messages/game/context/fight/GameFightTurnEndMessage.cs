

// Generated on 04/24/2015 03:38:01
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Stump.Core.IO;
using Stump.DofusProtocol.Types;

namespace Stump.DofusProtocol.Messages
{
    public class GameFightTurnEndMessage : Message
    {
        public const uint Id = 719;
        public override uint MessageId
        {
            get { return Id; }
        }
        
        public int id;
        
        public GameFightTurnEndMessage()
        {
        }
        
        public GameFightTurnEndMessage(int id)
        {
            this.id = id;
        }
        
        public override void Serialize(IDataWriter writer)
        {
            writer.WriteInt(id);
        }
        
        public override void Deserialize(IDataReader reader)
        {
            id = reader.ReadInt();
        }
        
    }
    
}