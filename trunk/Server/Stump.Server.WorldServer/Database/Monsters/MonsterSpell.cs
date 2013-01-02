using Stump.DofusProtocol.Enums;
using Stump.ORM;
using Stump.ORM.SubSonic.SQLGeneration.Schema;
using Stump.Server.WorldServer.Database.Spells;
using Stump.Server.WorldServer.Game.Actors.RolePlay.Monsters;

namespace Stump.Server.WorldServer.Database.Monsters
{
    public class MonsterSpellRelator
    {
        public static string FetchQuery = "SELECT * FROM monsters_spells";
    }

    [TableName("monsters_spells")]
    public class MonsterSpell : ISpellRecord, IAutoGeneratedRecord
    {
        private MonsterGrade m_monsterGrade;

        public int Id
        {
            get;
            set;
        }

        public int MonsterGradeId
        {
            get;
            set;
        }

        [Ignore]
        public MonsterGrade MonsterGrade
        {
            get { return m_monsterGrade ?? (m_monsterGrade = MonsterManager.Instance.GetMonsterGrade(MonsterGradeId)); }
            set
            {
                m_monsterGrade = value;
                MonsterGradeId = value.Id;
            }
        }

        #region ISpellRecord Members

        public int SpellId
        {
            get;
            set;
        }

        [SubSonicDefaultSetting(1)]
        public sbyte Level
        {
            get;
            set;
        }

        #endregion

        public override string ToString()
        {
            return (SpellIdEnum) SpellId + " (" + SpellId + ")";
        }
    }
}