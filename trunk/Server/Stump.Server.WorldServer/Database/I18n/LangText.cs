using Stump.ORM;
using Stump.ORM.SubSonic.SQLGeneration.Schema;
using Stump.Server.BaseServer.Database.Interfaces;

namespace Stump.Server.WorldServer.Database.I18n
{
    public class LangTextRelator
    {
        public static string FetchQuery = "SELECT * FROM langs";
    }

    [TableName("langs")]
    public class LangText : ILangText, IAutoGeneratedRecord
    {
        // Primitive properties

        #region ILangText Members

        public uint Id
        {
            get;
            set;
        }

        public string French
        {
            get;
            set;
        }

        public string English
        {
            get;
            set;
        }

        public string German
        {
            get;
            set;
        }

        public string Spanish
        {
            get;
            set;
        }

        public string Italian
        {
            get;
            set;
        }

        public string Japanish
        {
            get;
            set;
        }

        public string Dutsh
        {
            get;
            set;
        }

        public string Portugese
        {
            get;
            set;
        }

        public string Russish
        {
            get;
            set;
        }

        #endregion
    }
}