﻿using System.ComponentModel;
using Stump.ORM;
using Stump.ORM.SubSonic.SQLGeneration.Schema;

namespace Stump.Server.WorldServer.Database
{
    [TableName("startup_actions_items")]
    public sealed class StartupActionItemRecord : IAutoGeneratedRecord
    {
        public uint Id
        {
            get;
            set;
        }

        public int ItemTemplate
        {
            get;
            set;
        }

        public int Amount
        {
            get;
            set;
        }

        [DefaultValue(true)]
        public bool MaxEffects
        {
            get;
            set;
        }
    }
}