

// Generated on 04/19/2016 10:17:36
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Stump.Core.IO;
using Stump.DofusProtocol.Types;

namespace Stump.DofusProtocol.Messages
{
    public class GoldAddedMessage : Message
    {
        public const uint Id = 6030;
        public override uint MessageId
        {
            get { return Id; }
        }
        
        public Types.GoldItem gold;
        
        public GoldAddedMessage()
        {
        }
        
        public GoldAddedMessage(Types.GoldItem gold)
        {
            this.gold = gold;
        }
        
        public override void Serialize(IDataWriter writer)
        {
            gold.Serialize(writer);
        }
        
        public override void Deserialize(IDataReader reader)
        {
            gold = new Types.GoldItem();
            gold.Deserialize(reader);
        }
        
    }
    
}