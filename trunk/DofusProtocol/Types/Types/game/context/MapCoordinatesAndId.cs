
// Generated on 01/04/2013 14:36:04
using System;
using System.Collections.Generic;
using System.Linq;
using Stump.Core.IO;

namespace Stump.DofusProtocol.Types
{
    public class MapCoordinatesAndId : MapCoordinates
    {
        public const short Id = 392;
        public override short TypeId
        {
            get { return Id; }
        }
        
        public int mapId;
        
        public MapCoordinatesAndId()
        {
        }
        
        public MapCoordinatesAndId(short worldX, short worldY, int mapId)
         : base(worldX, worldY)
        {
            this.mapId = mapId;
        }
        
        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            writer.WriteInt(mapId);
        }
        
        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            mapId = reader.ReadInt();
        }
        
    }
    
}