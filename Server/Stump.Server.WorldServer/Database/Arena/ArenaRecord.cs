﻿using Stump.ORM.SubSonic.SQLGeneration.Schema;
using Stump.Server.WorldServer.Game.Maps;

namespace Stump.Server.WorldServer.Database.Arena
{
    public class ArenaRelator
    {
        public const string FetchQuery = "SELECT * FROM arenas";
    }

    [TableName("arenas")]
    public class ArenaRecord
    {
        private Map m_map;

        [PrimaryKey("Id")]
        public int Id
        {
            get;
            set;
        }

        public int MapId
        {
            get;
            set;
        }

        public Map Map
        {
            get { return m_map ?? (m_map = Game.World.Instance.GetMap(MapId)); }
        }
    }
}