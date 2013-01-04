
// Generated on 01/04/2013 14:35:48
using System;
using System.Collections.Generic;
using System.Linq;
using Stump.Core.IO;
using Stump.DofusProtocol.Types;

namespace Stump.DofusProtocol.Messages
{
    public class EmotePlayMessage : EmotePlayAbstractMessage
    {
        public const uint Id = 5683;
        public override uint MessageId
        {
            get { return Id; }
        }
        
        public int actorId;
        public int accountId;
        
        public EmotePlayMessage()
        {
        }
        
        public EmotePlayMessage(sbyte emoteId, double emoteStartTime, int actorId, int accountId)
         : base(emoteId, emoteStartTime)
        {
            this.actorId = actorId;
            this.accountId = accountId;
        }
        
        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            writer.WriteInt(actorId);
            writer.WriteInt(accountId);
        }
        
        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            actorId = reader.ReadInt();
            accountId = reader.ReadInt();
        }
        
    }
    
}