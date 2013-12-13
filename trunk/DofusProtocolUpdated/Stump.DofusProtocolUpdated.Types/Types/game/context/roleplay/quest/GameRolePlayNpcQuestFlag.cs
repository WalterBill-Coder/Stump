

// Generated on 12/12/2013 16:57:32
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Stump.Core.IO;

namespace Stump.DofusProtocol.Types
{
    public class GameRolePlayNpcQuestFlag
    {
        public const short Id = 384;
        public virtual short TypeId
        {
            get { return Id; }
        }
        
        public IEnumerable<short> questsToValidId;
        public IEnumerable<short> questsToStartId;
        
        public GameRolePlayNpcQuestFlag()
        {
        }
        
        public GameRolePlayNpcQuestFlag(IEnumerable<short> questsToValidId, IEnumerable<short> questsToStartId)
        {
            this.questsToValidId = questsToValidId;
            this.questsToStartId = questsToStartId;
        }
        
        public virtual void Serialize(IDataWriter writer)
        {
            writer.WriteUShort((ushort)questsToValidId.Count());
            foreach (var entry in questsToValidId)
            {
                 writer.WriteShort(entry);
            }
            writer.WriteUShort((ushort)questsToStartId.Count());
            foreach (var entry in questsToStartId)
            {
                 writer.WriteShort(entry);
            }
        }
        
        public virtual void Deserialize(IDataReader reader)
        {
            var limit = reader.ReadUShort();
            questsToValidId = new short[limit];
            for (int i = 0; i < limit; i++)
            {
                 (questsToValidId as short[])[i] = reader.ReadShort();
            }
            limit = reader.ReadUShort();
            questsToStartId = new short[limit];
            for (int i = 0; i < limit; i++)
            {
                 (questsToStartId as short[])[i] = reader.ReadShort();
            }
        }
        
        public virtual int GetSerializationSize()
        {
            return sizeof(short) + questsToValidId.Sum(x => sizeof(short)) + sizeof(short) + questsToStartId.Sum(x => sizeof(short));
        }
        
    }
    
}