

// Generated on 04/19/2016 10:17:13
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Stump.Core.IO;
using Stump.DofusProtocol.Types;

namespace Stump.DofusProtocol.Messages
{
    public class ChatAbstractClientMessage : Message
    {
        public const uint Id = 850;
        public override uint MessageId
        {
            get { return Id; }
        }
        
        public string content;
        
        public ChatAbstractClientMessage()
        {
        }
        
        public ChatAbstractClientMessage(string content)
        {
            this.content = content;
        }
        
        public override void Serialize(IDataWriter writer)
        {
            writer.WriteUTF(content);
        }
        
        public override void Deserialize(IDataReader reader)
        {
            content = reader.ReadUTF();
        }
        
    }
    
}