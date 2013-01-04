
// Generated on 01/04/2013 14:36:02
using System;
using System.Collections.Generic;
using System.Linq;
using Stump.Core.IO;
using Stump.DofusProtocol.Types;

namespace Stump.DofusProtocol.Messages
{
    public class MailStatusMessage : Message
    {
        public const uint Id = 6275;
        public override uint MessageId
        {
            get { return Id; }
        }
        
        public short unread;
        public short total;
        
        public MailStatusMessage()
        {
        }
        
        public MailStatusMessage(short unread, short total)
        {
            this.unread = unread;
            this.total = total;
        }
        
        public override void Serialize(IDataWriter writer)
        {
            writer.WriteShort(unread);
            writer.WriteShort(total);
        }
        
        public override void Deserialize(IDataReader reader)
        {
            unread = reader.ReadShort();
            if (unread < 0)
                throw new Exception("Forbidden value on unread = " + unread + ", it doesn't respect the following condition : unread < 0");
            total = reader.ReadShort();
            if (total < 0)
                throw new Exception("Forbidden value on total = " + total + ", it doesn't respect the following condition : total < 0");
        }
        
    }
    
}