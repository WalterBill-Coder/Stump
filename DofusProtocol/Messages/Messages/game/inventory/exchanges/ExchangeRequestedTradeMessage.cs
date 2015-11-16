

// Generated on 11/16/2015 14:26:21
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Stump.Core.IO;
using Stump.DofusProtocol.Types;

namespace Stump.DofusProtocol.Messages
{
    public class ExchangeRequestedTradeMessage : ExchangeRequestedMessage
    {
        public const uint Id = 5523;
        public override uint MessageId
        {
            get { return Id; }
        }
        
        public int source;
        public int target;
        
        public ExchangeRequestedTradeMessage()
        {
        }
        
        public ExchangeRequestedTradeMessage(sbyte exchangeType, int source, int target)
         : base(exchangeType)
        {
            this.source = source;
            this.target = target;
        }
        
        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            writer.WriteVarInt(source);
            writer.WriteVarInt(target);
        }
        
        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            source = reader.ReadVarInt();
            if (source < 0)
                throw new Exception("Forbidden value on source = " + source + ", it doesn't respect the following condition : source < 0");
            target = reader.ReadVarInt();
            if (target < 0)
                throw new Exception("Forbidden value on target = " + target + ", it doesn't respect the following condition : target < 0");
        }
        
    }
    
}