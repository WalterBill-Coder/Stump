

// Generated on 04/19/2016 10:17:41
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Stump.Core.IO;
using Stump.DofusProtocol.Types;

namespace Stump.DofusProtocol.Messages
{
    public class OrnamentSelectRequestMessage : Message
    {
        public const uint Id = 6374;
        public override uint MessageId
        {
            get { return Id; }
        }
        
        public short ornamentId;
        
        public OrnamentSelectRequestMessage()
        {
        }
        
        public OrnamentSelectRequestMessage(short ornamentId)
        {
            this.ornamentId = ornamentId;
        }
        
        public override void Serialize(IDataWriter writer)
        {
            writer.WriteVarShort(ornamentId);
        }
        
        public override void Deserialize(IDataReader reader)
        {
            ornamentId = reader.ReadVarShort();
            if (ornamentId < 0)
                throw new Exception("Forbidden value on ornamentId = " + ornamentId + ", it doesn't respect the following condition : ornamentId < 0");
        }
        
    }
    
}