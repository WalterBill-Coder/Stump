

// Generated on 04/24/2015 03:38:25
using System;
using System.Collections.Generic;
using Stump.DofusProtocol.D2oClasses;
using Stump.DofusProtocol.D2oClasses.Tools.D2o;

namespace Stump.DofusProtocol.D2oClasses
{
    [D2OClass("EmblemBackground", "com.ankamagames.dofus.datacenter.guild")]
    [Serializable]
    public class EmblemBackground : IDataObject, IIndexedData
    {
        public const String MODULE = "EmblemBackgrounds";
        public int id;
        public int order;
        int IIndexedData.Id
        {
            get { return (int)id; }
        }
        [D2OIgnore]
        public int Id
        {
            get { return this.id; }
            set { this.id = value; }
        }
        [D2OIgnore]
        public int Order
        {
            get { return this.order; }
            set { this.order = value; }
        }
    }
}