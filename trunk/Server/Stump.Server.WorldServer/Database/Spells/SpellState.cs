using System;
using Stump.DofusProtocol.D2oClasses.Tools.D2o;
using Stump.DofusProtocol.D2oClasses;
using Stump.ORM;
using Stump.ORM.SubSonic.SQLGeneration.Schema;
using Stump.Server.WorldServer.Database.I18n;

namespace Stump.Server.WorldServer.Database.Spells
{
    public class SpellStateRelator
    {
        public static string FetchQuery = "SELECT * FROM spells_states";
    }

    [TableName("spells_states")]
    [D2OClass("SpellState", "com.ankamagames.dofus.datacenter.spells")]
    public sealed class SpellState : IAssignedByD2O, IAutoGeneratedRecord
    {
        private string m_name;

        [PrimaryKey("Id", false)]
        public int Id
        {
            get;
            set;
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

        public Boolean PreventsSpellCast
        {
            get;
            set;
        }

        public Boolean PreventsFight
        {
            get;
            set;
        }

        public Boolean Critical
        {
            get;
            set;
        }

        #region IAssignedByD2O Members

        public void AssignFields(object d2oObject)
        {
            var state = (DofusProtocol.D2oClasses.SpellState) d2oObject;
            Id = state.id;
            NameId = state.nameId;
            PreventsSpellCast = state.preventsSpellCast;
            PreventsFight = state.preventsFight;
            Critical = state.critical;
        }

        #endregion
    }
}