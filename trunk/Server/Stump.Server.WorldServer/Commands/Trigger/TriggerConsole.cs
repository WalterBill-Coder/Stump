using System;
using Stump.Core.IO;
using Stump.DofusProtocol.Enums;
using Stump.Server.BaseServer.Commands;
using Stump.Server.BaseServer.Network;
using Stump.Server.WorldServer.Handlers;
using Stump.Server.WorldServer.Handlers.Authorized;
using Stump.Server.WorldServer.Worlds.Actors.RolePlay.Characters;

namespace Stump.Server.WorldServer.Commands.Trigger
{
    public class TriggerConsole : GameTrigger
    {
        public TriggerConsole(StringStream args, Character character)
            : base(args, character == null ? RoleEnum.Administrator : character.Client.Account.Role, character)
        {
        }

        public TriggerConsole(string args, Character character)
            : base(args, character == null ? RoleEnum.Administrator : character.Client.Account.Role, null)
        {
        }

        public override void Reply(string text)
        {
            AuthorizedHandler.SendConsoleMessage(Character.Client, text);
        }

        public override void ReplyError(string message)
        {
            AuthorizedHandler.SendConsoleMessage(Character.Client, ConsoleMessageTypeEnum.CONSOLE_ERR_MESSAGE, message);
        }

        public override BaseClient GetSource()
        {
            return Character != null ? Character.Client : null;
        }
    }
}