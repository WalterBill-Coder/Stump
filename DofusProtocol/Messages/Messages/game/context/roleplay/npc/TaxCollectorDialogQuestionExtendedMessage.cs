

// Generated on 04/19/2016 10:17:22
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Stump.Core.IO;
using Stump.DofusProtocol.Types;

namespace Stump.DofusProtocol.Messages
{
    public class TaxCollectorDialogQuestionExtendedMessage : TaxCollectorDialogQuestionBasicMessage
    {
        public const uint Id = 5615;
        public override uint MessageId
        {
            get { return Id; }
        }
        
        public short maxPods;
        public short prospecting;
        public short wisdom;
        public sbyte taxCollectorsCount;
        public int taxCollectorAttack;
        public int kamas;
        public long experience;
        public int pods;
        public int itemsValue;
        
        public TaxCollectorDialogQuestionExtendedMessage()
        {
        }
        
        public TaxCollectorDialogQuestionExtendedMessage(Types.BasicGuildInformations guildInfo, short maxPods, short prospecting, short wisdom, sbyte taxCollectorsCount, int taxCollectorAttack, int kamas, long experience, int pods, int itemsValue)
         : base(guildInfo)
        {
            this.maxPods = maxPods;
            this.prospecting = prospecting;
            this.wisdom = wisdom;
            this.taxCollectorsCount = taxCollectorsCount;
            this.taxCollectorAttack = taxCollectorAttack;
            this.kamas = kamas;
            this.experience = experience;
            this.pods = pods;
            this.itemsValue = itemsValue;
        }
        
        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            writer.WriteVarShort(maxPods);
            writer.WriteVarShort(prospecting);
            writer.WriteVarShort(wisdom);
            writer.WriteSByte(taxCollectorsCount);
            writer.WriteInt(taxCollectorAttack);
            writer.WriteVarInt(kamas);
            writer.WriteVarLong(experience);
            writer.WriteVarInt(pods);
            writer.WriteVarInt(itemsValue);
        }
        
        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            maxPods = reader.ReadVarShort();
            if (maxPods < 0)
                throw new Exception("Forbidden value on maxPods = " + maxPods + ", it doesn't respect the following condition : maxPods < 0");
            prospecting = reader.ReadVarShort();
            if (prospecting < 0)
                throw new Exception("Forbidden value on prospecting = " + prospecting + ", it doesn't respect the following condition : prospecting < 0");
            wisdom = reader.ReadVarShort();
            if (wisdom < 0)
                throw new Exception("Forbidden value on wisdom = " + wisdom + ", it doesn't respect the following condition : wisdom < 0");
            taxCollectorsCount = reader.ReadSByte();
            if (taxCollectorsCount < 0)
                throw new Exception("Forbidden value on taxCollectorsCount = " + taxCollectorsCount + ", it doesn't respect the following condition : taxCollectorsCount < 0");
            taxCollectorAttack = reader.ReadInt();
            kamas = reader.ReadVarInt();
            if (kamas < 0)
                throw new Exception("Forbidden value on kamas = " + kamas + ", it doesn't respect the following condition : kamas < 0");
            experience = reader.ReadVarLong();
            if (experience < 0 || experience > 9007199254740990)
                throw new Exception("Forbidden value on experience = " + experience + ", it doesn't respect the following condition : experience < 0 || experience > 9007199254740990");
            pods = reader.ReadVarInt();
            if (pods < 0)
                throw new Exception("Forbidden value on pods = " + pods + ", it doesn't respect the following condition : pods < 0");
            itemsValue = reader.ReadVarInt();
            if (itemsValue < 0)
                throw new Exception("Forbidden value on itemsValue = " + itemsValue + ", it doesn't respect the following condition : itemsValue < 0");
        }
        
    }
    
}