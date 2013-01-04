using Stump.ORM;
using Stump.ORM.SubSonic.SQLGeneration.Schema;

namespace Stump.Server.AuthServer.Database
{
    public class LangTextUIRelator
    {
        public static string FetchQuery = "SELECT * FROM langs_ui";
    }

    [TableName("langs_ui")]
    public partial class LangTextUI// : IAutoGeneratedRecord not used yet
    {
        // Primitive properties

        public int Id
        {
            get;
            set;
        }
        public string Name
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
    }
}