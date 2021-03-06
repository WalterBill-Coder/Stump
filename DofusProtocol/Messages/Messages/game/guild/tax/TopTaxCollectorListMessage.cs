

// Generated on 10/30/2016 16:20:40
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Stump.Core.IO;
using Stump.DofusProtocol.Types;

namespace Stump.DofusProtocol.Messages
{
    public class TopTaxCollectorListMessage : AbstractTaxCollectorListMessage
    {
        public const uint Id = 6565;
        public override uint MessageId
        {
            get { return Id; }
        }
        
        public bool isDungeon;
        
        public TopTaxCollectorListMessage()
        {
        }
        
        public TopTaxCollectorListMessage(IEnumerable<Types.TaxCollectorInformations> informations, bool isDungeon)
         : base(informations)
        {
            this.isDungeon = isDungeon;
        }
        
        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            writer.WriteBoolean(isDungeon);
        }
        
        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            isDungeon = reader.ReadBoolean();
        }
        
    }
    
}