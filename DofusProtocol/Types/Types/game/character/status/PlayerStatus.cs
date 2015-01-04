

// Generated on 01/04/2015 11:54:49
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Stump.Core.IO;

namespace Stump.DofusProtocol.Types
{
    public class PlayerStatus
    {
        public const short Id = 415;
        public virtual short TypeId
        {
            get { return Id; }
        }
        
        public sbyte statusId;
        
        public PlayerStatus()
        {
        }
        
        public PlayerStatus(sbyte statusId)
        {
            this.statusId = statusId;
        }
        
        public virtual void Serialize(IDataWriter writer)
        {
            writer.WriteSByte(statusId);
        }
        
        public virtual void Deserialize(IDataReader reader)
        {
            statusId = reader.ReadSByte();
            if (statusId < 0)
                throw new Exception("Forbidden value on statusId = " + statusId + ", it doesn't respect the following condition : statusId < 0");
        }
        
        
    }
    
}