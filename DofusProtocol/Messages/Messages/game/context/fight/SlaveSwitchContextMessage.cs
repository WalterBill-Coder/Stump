

// Generated on 10/30/2016 16:20:27
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Stump.Core.IO;
using Stump.DofusProtocol.Types;

namespace Stump.DofusProtocol.Messages
{
    public class SlaveSwitchContextMessage : Message
    {
        public const uint Id = 6214;
        public override uint MessageId
        {
            get { return Id; }
        }
        
        public double masterId;
        public double slaveId;
        public IEnumerable<Types.SpellItem> slaveSpells;
        public Types.CharacterCharacteristicsInformations slaveStats;
        public IEnumerable<Types.Shortcut> shortcuts;
        
        public SlaveSwitchContextMessage()
        {
        }
        
        public SlaveSwitchContextMessage(double masterId, double slaveId, IEnumerable<Types.SpellItem> slaveSpells, Types.CharacterCharacteristicsInformations slaveStats, IEnumerable<Types.Shortcut> shortcuts)
        {
            this.masterId = masterId;
            this.slaveId = slaveId;
            this.slaveSpells = slaveSpells;
            this.slaveStats = slaveStats;
            this.shortcuts = shortcuts;
        }
        
        public override void Serialize(IDataWriter writer)
        {
            writer.WriteDouble(masterId);
            writer.WriteDouble(slaveId);
            var slaveSpells_before = writer.Position;
            var slaveSpells_count = 0;
            writer.WriteUShort(0);
            foreach (var entry in slaveSpells)
            {
                 entry.Serialize(writer);
                 slaveSpells_count++;
            }
            var slaveSpells_after = writer.Position;
            writer.Seek((int)slaveSpells_before);
            writer.WriteUShort((ushort)slaveSpells_count);
            writer.Seek((int)slaveSpells_after);

            slaveStats.Serialize(writer);
            var shortcuts_before = writer.Position;
            var shortcuts_count = 0;
            writer.WriteUShort(0);
            foreach (var entry in shortcuts)
            {
                 writer.WriteShort(entry.TypeId);
                 entry.Serialize(writer);
                 shortcuts_count++;
            }
            var shortcuts_after = writer.Position;
            writer.Seek((int)shortcuts_before);
            writer.WriteUShort((ushort)shortcuts_count);
            writer.Seek((int)shortcuts_after);

        }
        
        public override void Deserialize(IDataReader reader)
        {
            masterId = reader.ReadDouble();
            if (masterId < -9007199254740990 || masterId > 9007199254740990)
                throw new Exception("Forbidden value on masterId = " + masterId + ", it doesn't respect the following condition : masterId < -9007199254740990 || masterId > 9007199254740990");
            slaveId = reader.ReadDouble();
            if (slaveId < -9007199254740990 || slaveId > 9007199254740990)
                throw new Exception("Forbidden value on slaveId = " + slaveId + ", it doesn't respect the following condition : slaveId < -9007199254740990 || slaveId > 9007199254740990");
            var limit = reader.ReadUShort();
            var slaveSpells_ = new Types.SpellItem[limit];
            for (int i = 0; i < limit; i++)
            {
                 slaveSpells_[i] = new Types.SpellItem();
                 slaveSpells_[i].Deserialize(reader);
            }
            slaveSpells = slaveSpells_;
            slaveStats = new Types.CharacterCharacteristicsInformations();
            slaveStats.Deserialize(reader);
            limit = reader.ReadUShort();
            var shortcuts_ = new Types.Shortcut[limit];
            for (int i = 0; i < limit; i++)
            {
                 shortcuts_[i] = Types.ProtocolTypeManager.GetInstance<Types.Shortcut>(reader.ReadShort());
                 shortcuts_[i].Deserialize(reader);
            }
            shortcuts = shortcuts_;
        }
        
    }
    
}