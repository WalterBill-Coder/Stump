

// Generated on 10/28/2013 14:03:17
using System;
using System.Collections.Generic;
using Stump.DofusProtocol.D2oClasses;
using Stump.DofusProtocol.D2oClasses.Tools.D2o;

namespace Stump.DofusProtocol.D2oClasses
{
    [D2OClass("AmbientSound", "com.ankamagames.dofus.datacenter.ambientSounds")]
    [Serializable]
    public class AmbientSound : IDataObject, IIndexedData
    {
        public const int AMBIENT_TYPE_ROLEPLAY = 1;
        public const int AMBIENT_TYPE_AMBIENT = 2;
        public const int AMBIENT_TYPE_FIGHT = 3;
        public const int AMBIENT_TYPE_BOSS = 4;
        private const String MODULE = "AmbientSounds";
        public int id;
        public uint volume;
        public int criterionId;
        public uint silenceMin;
        public uint silenceMax;
        public int channel;
        public int type_id;
        int IIndexedData.Id
        {
            get { return (int)id; }
        }
        [D2OIgnore]
        public int Id
        {
            get { return id; }
            set { id = value; }
        }
        [D2OIgnore]
        public uint Volume
        {
            get { return volume; }
            set { volume = value; }
        }
        [D2OIgnore]
        public int CriterionId
        {
            get { return criterionId; }
            set { criterionId = value; }
        }
        [D2OIgnore]
        public uint SilenceMin
        {
            get { return silenceMin; }
            set { silenceMin = value; }
        }
        [D2OIgnore]
        public uint SilenceMax
        {
            get { return silenceMax; }
            set { silenceMax = value; }
        }
        [D2OIgnore]
        public int Channel
        {
            get { return channel; }
            set { channel = value; }
        }
        [D2OIgnore]
        public int Type_id
        {
            get { return type_id; }
            set { type_id = value; }
        }
    }
}