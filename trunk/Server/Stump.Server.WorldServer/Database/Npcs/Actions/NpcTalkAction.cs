using Stump.Server.WorldServer.Game.Actors.RolePlay.Characters;
using Stump.Server.WorldServer.Game.Actors.RolePlay.Npcs;
using Stump.Server.WorldServer.Game.Dialogs.Npcs;

namespace Stump.Server.WorldServer.Database
{
    public class NpcTalkAction : NpcAction
    {
        /*public override NpcActionTypeEnum ActionType
        {
            get
            {
                return NpcActionTypeEnum.ACTION_TALK;
            }
        }*/

        private NpcMessage m_message;

        /// <summary>
        /// Parameter 0
        /// </summary>
        public int MessageId
        {
            get { return Record.GetParameter<int>(0); }
            set { Record.SetParameter(0, value); }
        }

        public NpcMessage Message
        {
            get { return m_message ?? (m_message = NpcManager.Instance.GetNpcMessage(MessageId)); }
            set
            {
                m_message = value;
                MessageId = value.Id;
            }
        }

        public override void Execute(Npc npc, Character character)
        {
            var dialog = new NpcDialog(character, npc);

            dialog.Open();
            dialog.ChangeMessage(Message);
        }
    }
}