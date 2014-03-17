

// Generated on 03/06/2014 18:50:01
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Stump.Core.IO;
using Stump.DofusProtocol.Types;

namespace Stump.DofusProtocol.Messages
{
    public class GameActionFightDispellEffectMessage : GameActionFightDispellMessage
    {
        public const uint Id = 6113;
        public override uint MessageId
        {
            get { return Id; }
        }
        
        public int boostUID;
        
        public GameActionFightDispellEffectMessage()
        {
        }
        
        public GameActionFightDispellEffectMessage(short actionId, int sourceId, int targetId, int boostUID)
         : base(actionId, sourceId, targetId)
        {
            this.boostUID = boostUID;
        }
        
        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            writer.WriteInt(boostUID);
        }
        
        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            boostUID = reader.ReadInt();
            if (boostUID < 0)
                throw new Exception("Forbidden value on boostUID = " + boostUID + ", it doesn't respect the following condition : boostUID < 0");
        }
        
        public override int GetSerializationSize()
        {
            return base.GetSerializationSize() + sizeof(int);
        }
        
    }
    
}