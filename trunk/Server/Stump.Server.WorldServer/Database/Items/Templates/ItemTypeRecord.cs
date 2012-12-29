using System;
using Stump.DofusProtocol.D2oClasses;
using Stump.DofusProtocol.Enums;
using Stump.ORM;
using Stump.ORM.SubSonic.SQLGeneration.Schema;
using Stump.Server.WorldServer.Database.I18n;

namespace Stump.Server.WorldServer.Database.Items
{
    public class ItemTypeRecordRelator
    {
        public static string FetchQuery = "SELECT * FROM items_types";
    }

    [TableName("items_types")]
    [D2OClass("ItemType", "com.ankamagames.dofus.datacenter.items")]
    public sealed class ItemTypeRecord : IAssignedByD2O, IAutoGeneratedRecord
    {
        private string m_name;

        public int Id
        {
            get;
            set;
        }

        public ItemTypeEnum ItemType
        {
            get { return (ItemTypeEnum) Id; }
        }

        public uint NameId
        {
            get;
            set;
        }

        public string Name
        {
            get { return m_name ?? (m_name = TextManager.Instance.GetText(NameId)); }
        }

        public uint SuperTypeId
        {
            get;
            set;
        }

        public ItemSuperTypeEnum SuperType
        {
            get { return (ItemSuperTypeEnum) SuperTypeId; }
        }

        public Boolean Plural
        {
            get;
            set;
        }

        public uint Gender
        {
            get;
            set;
        }

        public uint ZoneMinSize
        {
            get;
            set;
        }

        public uint ZoneSize
        {
            get;
            set;
        }

        public SpellShapeEnum ZoneShape
        {
            get;
            set;
        }

        public Boolean NeedUseConfirm
        {
            get;
            set;
        }

        #region IAssignedByD2O Members

        public void AssignFields(object d2oObject)
        {
            var type = (ItemType) d2oObject;
            Id = type.id;
            NameId = type.nameId;
            SuperTypeId = type.superTypeId;
            Plural = type.plural;
            Gender = type.gender;
            ParseRawZone(type.rawZone);
            NeedUseConfirm = type.needUseConfirm;
        }

        #endregion

        private void ParseRawZone(string rawZone)
        {
            if (string.IsNullOrEmpty(rawZone) || rawZone == "null")
            {
                ZoneMinSize = 0;
                ZoneSize = 0;
                ZoneShape = 0;
                return;
            }

            char chr = rawZone[0];
            SpellShapeEnum shape;
            switch (chr)
            {
                case '+':
                    shape = SpellShapeEnum.plus;
                    break;
                case '-':
                    shape = SpellShapeEnum.minus;
                    break;
                case '/':
                    shape = SpellShapeEnum.slash;
                    break;
                case '*':
                    shape = SpellShapeEnum.star;
                    break;
                case '#':
                    shape = SpellShapeEnum.sharp;
                    break;
                default:
                    shape = (SpellShapeEnum) Enum.Parse(typeof (SpellShapeEnum), chr.ToString());
                    break;
            }
            ZoneShape = shape;

            if (rawZone.Length > 1)
            {
                int comma = rawZone.IndexOf(",", StringComparison.Ordinal);
                if (comma != -1)
                {
                    ZoneSize = uint.Parse(rawZone.Substring(1, rawZone.Length - comma - 1));
                    ZoneMinSize = uint.Parse(rawZone.Substring(comma + 1, rawZone.Length - (comma + 1)));
                }

                ZoneSize = uint.Parse(rawZone.Substring(1));
            }
            else
            {
                ZoneSize = 0;
                ZoneMinSize = 0;
            }
        }
    }
}