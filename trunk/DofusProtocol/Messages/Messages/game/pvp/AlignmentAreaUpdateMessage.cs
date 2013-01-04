
// Generated on 01/04/2013 14:36:00
using System;
using System.Collections.Generic;
using System.Linq;
using Stump.Core.IO;
using Stump.DofusProtocol.Types;

namespace Stump.DofusProtocol.Messages
{
    public class AlignmentAreaUpdateMessage : Message
    {
        public const uint Id = 6060;
        public override uint MessageId
        {
            get { return Id; }
        }
        
        public short areaId;
        public sbyte side;
        
        public AlignmentAreaUpdateMessage()
        {
        }
        
        public AlignmentAreaUpdateMessage(short areaId, sbyte side)
        {
            this.areaId = areaId;
            this.side = side;
        }
        
        public override void Serialize(IDataWriter writer)
        {
            writer.WriteShort(areaId);
            writer.WriteSByte(side);
        }
        
        public override void Deserialize(IDataReader reader)
        {
            areaId = reader.ReadShort();
            if (areaId < 0)
                throw new Exception("Forbidden value on areaId = " + areaId + ", it doesn't respect the following condition : areaId < 0");
            side = reader.ReadSByte();
        }
        
    }
    
}