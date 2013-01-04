
// Generated on 01/04/2013 14:35:55
using System;
using System.Collections.Generic;
using System.Linq;
using Stump.Core.IO;
using Stump.DofusProtocol.Types;

namespace Stump.DofusProtocol.Messages
{
    public class ExchangeClearPaymentForCraftMessage : Message
    {
        public const uint Id = 6145;
        public override uint MessageId
        {
            get { return Id; }
        }
        
        public sbyte paymentType;
        
        public ExchangeClearPaymentForCraftMessage()
        {
        }
        
        public ExchangeClearPaymentForCraftMessage(sbyte paymentType)
        {
            this.paymentType = paymentType;
        }
        
        public override void Serialize(IDataWriter writer)
        {
            writer.WriteSByte(paymentType);
        }
        
        public override void Deserialize(IDataReader reader)
        {
            paymentType = reader.ReadSByte();
        }
        
    }
    
}