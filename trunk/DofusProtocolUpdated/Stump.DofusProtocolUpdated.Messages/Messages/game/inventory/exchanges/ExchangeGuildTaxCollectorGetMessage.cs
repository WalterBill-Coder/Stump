

// Generated on 12/12/2013 16:57:15
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Stump.Core.IO;
using Stump.DofusProtocol.Types;

namespace Stump.DofusProtocol.Messages
{
    public class ExchangeGuildTaxCollectorGetMessage : Message
    {
        public const uint Id = 5762;
        public override uint MessageId
        {
            get { return Id; }
        }
        
        public string collectorName;
        public short worldX;
        public short worldY;
        public int mapId;
        public short subAreaId;
        public string userName;
        public double experience;
        public IEnumerable<Types.ObjectItemQuantity> objectsInfos;
        
        public ExchangeGuildTaxCollectorGetMessage()
        {
        }
        
        public ExchangeGuildTaxCollectorGetMessage(string collectorName, short worldX, short worldY, int mapId, short subAreaId, string userName, double experience, IEnumerable<Types.ObjectItemQuantity> objectsInfos)
        {
            this.collectorName = collectorName;
            this.worldX = worldX;
            this.worldY = worldY;
            this.mapId = mapId;
            this.subAreaId = subAreaId;
            this.userName = userName;
            this.experience = experience;
            this.objectsInfos = objectsInfos;
        }
        
        public override void Serialize(IDataWriter writer)
        {
            writer.WriteUTF(collectorName);
            writer.WriteShort(worldX);
            writer.WriteShort(worldY);
            writer.WriteInt(mapId);
            writer.WriteShort(subAreaId);
            writer.WriteUTF(userName);
            writer.WriteDouble(experience);
            writer.WriteUShort((ushort)objectsInfos.Count());
            foreach (var entry in objectsInfos)
            {
                 entry.Serialize(writer);
            }
        }
        
        public override void Deserialize(IDataReader reader)
        {
            collectorName = reader.ReadUTF();
            worldX = reader.ReadShort();
            if (worldX < -255 || worldX > 255)
                throw new Exception("Forbidden value on worldX = " + worldX + ", it doesn't respect the following condition : worldX < -255 || worldX > 255");
            worldY = reader.ReadShort();
            if (worldY < -255 || worldY > 255)
                throw new Exception("Forbidden value on worldY = " + worldY + ", it doesn't respect the following condition : worldY < -255 || worldY > 255");
            mapId = reader.ReadInt();
            subAreaId = reader.ReadShort();
            if (subAreaId < 0)
                throw new Exception("Forbidden value on subAreaId = " + subAreaId + ", it doesn't respect the following condition : subAreaId < 0");
            userName = reader.ReadUTF();
            experience = reader.ReadDouble();
            var limit = reader.ReadUShort();
            objectsInfos = new Types.ObjectItemQuantity[limit];
            for (int i = 0; i < limit; i++)
            {
                 (objectsInfos as Types.ObjectItemQuantity[])[i] = new Types.ObjectItemQuantity();
                 (objectsInfos as Types.ObjectItemQuantity[])[i].Deserialize(reader);
            }
        }
        
        public override int GetSerializationSize()
        {
            return sizeof(short) + Encoding.UTF8.GetByteCount(collectorName) + sizeof(short) + sizeof(short) + sizeof(int) + sizeof(short) + sizeof(short) + Encoding.UTF8.GetByteCount(userName) + sizeof(double) + sizeof(short) + objectsInfos.Sum(x => x.GetSerializationSize());
        }
        
    }
    
}