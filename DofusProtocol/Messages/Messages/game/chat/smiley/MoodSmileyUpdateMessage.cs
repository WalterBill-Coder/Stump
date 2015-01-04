

// Generated on 01/04/2015 11:54:10
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Stump.Core.IO;
using Stump.DofusProtocol.Types;

namespace Stump.DofusProtocol.Messages
{
    public class MoodSmileyUpdateMessage : Message
    {
        public const uint Id = 6388;
        public override uint MessageId
        {
            get { return Id; }
        }
        
        public int accountId;
        public int playerId;
        public sbyte smileyId;
        
        public MoodSmileyUpdateMessage()
        {
        }
        
        public MoodSmileyUpdateMessage(int accountId, int playerId, sbyte smileyId)
        {
            this.accountId = accountId;
            this.playerId = playerId;
            this.smileyId = smileyId;
        }
        
        public override void Serialize(IDataWriter writer)
        {
            writer.WriteInt(accountId);
            writer.WriteVarInt(playerId);
            writer.WriteSByte(smileyId);
        }
        
        public override void Deserialize(IDataReader reader)
        {
            accountId = reader.ReadInt();
            if (accountId < 0)
                throw new Exception("Forbidden value on accountId = " + accountId + ", it doesn't respect the following condition : accountId < 0");
            playerId = reader.ReadVarInt();
            if (playerId < 0)
                throw new Exception("Forbidden value on playerId = " + playerId + ", it doesn't respect the following condition : playerId < 0");
            smileyId = reader.ReadSByte();
        }
        
    }
    
}