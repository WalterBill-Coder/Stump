

// Generated on 04/19/2016 10:17:34
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Stump.Core.IO;
using Stump.DofusProtocol.Types;

namespace Stump.DofusProtocol.Messages
{
    public class ExchangeRequestedMessage : Message
    {
        public const uint Id = 5522;
        public override uint MessageId
        {
            get { return Id; }
        }
        
        public sbyte exchangeType;
        
        public ExchangeRequestedMessage()
        {
        }
        
        public ExchangeRequestedMessage(sbyte exchangeType)
        {
            this.exchangeType = exchangeType;
        }
        
        public override void Serialize(IDataWriter writer)
        {
            writer.WriteSByte(exchangeType);
        }
        
        public override void Deserialize(IDataReader reader)
        {
            exchangeType = reader.ReadSByte();
        }
        
    }
    
}