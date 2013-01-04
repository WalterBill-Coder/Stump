
// Generated on 01/04/2013 14:35:44
using System;
using System.Collections.Generic;
using System.Linq;
using Stump.Core.IO;
using Stump.DofusProtocol.Types;

namespace Stump.DofusProtocol.Messages
{
    public class GameMapMovementCancelMessage : Message
    {
        public const uint Id = 953;
        public override uint MessageId
        {
            get { return Id; }
        }
        
        public short cellId;
        
        public GameMapMovementCancelMessage()
        {
        }
        
        public GameMapMovementCancelMessage(short cellId)
        {
            this.cellId = cellId;
        }
        
        public override void Serialize(IDataWriter writer)
        {
            writer.WriteShort(cellId);
        }
        
        public override void Deserialize(IDataReader reader)
        {
            cellId = reader.ReadShort();
            if (cellId < 0 || cellId > 559)
                throw new Exception("Forbidden value on cellId = " + cellId + ", it doesn't respect the following condition : cellId < 0 || cellId > 559");
        }
        
    }
    
}