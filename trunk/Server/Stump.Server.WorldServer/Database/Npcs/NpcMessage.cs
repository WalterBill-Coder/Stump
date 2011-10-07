using System.Collections.Generic;
using Castle.ActiveRecord;
using Stump.DofusProtocol.D2oClasses;
using Stump.DofusProtocol.D2oClasses.Tool;

namespace Stump.Server.WorldServer.Database.Npcs
{
    [ActiveRecord("npcs_messages")]
    [D2OClass("NpcMessage", "com.ankamagames.dofus.datacenter.npcs")]
    public sealed class NpcMessage : WorldBaseRecord<NpcMessage>
    {
        private IList<string> m_parameters;
        private string m_parametersAsString;
        private IList<NpcReply> m_replies;

        [D2OField("id")]
        [PrimaryKey(PrimaryKeyType.Assigned, "Id")]
        public int Id
        {
            get;
            set;
        }

        [D2OField("messageId")]
        [Property("MessageId")]
        public uint MessageId
        {
            get;
            set;
        }

        [D2OField("messageParams")]
        [Property("MessageParams")]
        internal string ParametersAsString
        {
            get { return m_parametersAsString; }
            set
            {
                m_parametersAsString = value;
                m_parameters = value.Split('|');
            }
        }

        public IList<string> Parameters
        {
            get { return m_parameters; }
            set
            {
                m_parameters = value;
                ParametersAsString = string.Join("|", value);
            }
        }

        [HasMany(typeof (NpcReply), "MessageId", "npcs_replies", Cascade = ManyRelationCascadeEnum.Delete)]
        public IList<NpcReply> Replies
        {
            get { return m_replies ?? (m_replies = new List<NpcReply>()); }
            set { m_replies = value; }
        }
    }
}