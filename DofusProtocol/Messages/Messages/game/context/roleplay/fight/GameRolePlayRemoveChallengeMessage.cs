

// Generated on 10/30/2016 16:20:30
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Stump.Core.IO;
using Stump.DofusProtocol.Types;

namespace Stump.DofusProtocol.Messages
{
    public class GameRolePlayRemoveChallengeMessage : Message
    {
        public const uint Id = 300;
        public override uint MessageId
        {
            get { return Id; }
        }
        
        public int fightId;
        
        public GameRolePlayRemoveChallengeMessage()
        {
        }
        
        public GameRolePlayRemoveChallengeMessage(int fightId)
        {
            this.fightId = fightId;
        }
        
        public override void Serialize(IDataWriter writer)
        {
            writer.WriteInt(fightId);
        }
        
        public override void Deserialize(IDataReader reader)
        {
            fightId = reader.ReadInt();
        }
        
    }
    
}