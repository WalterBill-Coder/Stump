

// Generated on 09/26/2016 01:50:09
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Stump.Core.IO;
using Stump.DofusProtocol.Types;

namespace Stump.DofusProtocol.Messages
{
    public class IdolSelectedMessage : Message
    {
        public const uint Id = 6581;
        public override uint MessageId
        {
            get { return Id; }
        }
        
        public bool activate;
        public bool party;
        public short idolId;
        
        public IdolSelectedMessage()
        {
        }
        
        public IdolSelectedMessage(bool activate, bool party, short idolId)
        {
            this.activate = activate;
            this.party = party;
            this.idolId = idolId;
        }
        
        public override void Serialize(IDataWriter writer)
        {
            byte flag1 = 0;
            flag1 = BooleanByteWrapper.SetFlag(flag1, 0, activate);
            flag1 = BooleanByteWrapper.SetFlag(flag1, 1, party);
            writer.WriteByte(flag1);
            writer.WriteVarShort(idolId);
        }
        
        public override void Deserialize(IDataReader reader)
        {
            byte flag1 = reader.ReadByte();
            activate = BooleanByteWrapper.GetFlag(flag1, 0);
            party = BooleanByteWrapper.GetFlag(flag1, 1);
            idolId = reader.ReadVarShort();
            if (idolId < 0)
                throw new Exception("Forbidden value on idolId = " + idolId + ", it doesn't respect the following condition : idolId < 0");
        }
        
    }
    
}