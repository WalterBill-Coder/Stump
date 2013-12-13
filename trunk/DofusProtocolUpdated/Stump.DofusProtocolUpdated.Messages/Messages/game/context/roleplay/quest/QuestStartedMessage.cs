

// Generated on 12/12/2013 16:57:08
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Stump.Core.IO;
using Stump.DofusProtocol.Types;

namespace Stump.DofusProtocol.Messages
{
    public class QuestStartedMessage : Message
    {
        public const uint Id = 6091;
        public override uint MessageId
        {
            get { return Id; }
        }
        
        public ushort questId;
        
        public QuestStartedMessage()
        {
        }
        
        public QuestStartedMessage(ushort questId)
        {
            this.questId = questId;
        }
        
        public override void Serialize(IDataWriter writer)
        {
            writer.WriteUShort(questId);
        }
        
        public override void Deserialize(IDataReader reader)
        {
            questId = reader.ReadUShort();
            if (questId < 0 || questId > 65535)
                throw new Exception("Forbidden value on questId = " + questId + ", it doesn't respect the following condition : questId < 0 || questId > 65535");
        }
        
        public override int GetSerializationSize()
        {
            return sizeof(ushort);
        }
        
    }
    
}