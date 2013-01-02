﻿using Stump.DofusProtocol.Enums;
using Stump.ORM;
using Stump.ORM.SubSonic.SQLGeneration.Schema;
using Stump.Server.WorldServer.Database.Spells;

namespace Stump.Server.WorldServer.Database.Characters
{
    public class CharacterSpellRelator
    {
        public static string FetchQuery = "SELECT * FROM characters_spells";
    }

    [TableName("characters_spells")]
    public class CharacterSpell : ISpellRecord, IAutoGeneratedRecord
    {
        // Primitive properties

        public int Id
        {
            get;
            set;
        }

        public int OwnerId
        {
            get;
            set;
        }

        public byte Position
        {
            get;
            set;
        }

        #region ISpellRecord Members

        public int SpellId
        {
            get;
            set;
        }

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