using System;
using System.Diagnostics;
using Stump.DofusProtocol.Enums;

namespace Stump.Server.BaseServer.Commands.Commands
{
    public class InfoCommand : CommandBase
    {
        public InfoCommand()
        {
            Aliases = new[] { "info" };
            RequiredRole = RoleEnum.Moderator;
            Description = "Display some informations";
        }

        public override void Execute(TriggerBase trigger)
        {
            trigger.Reply("Uptime : " + trigger.Bold("{0}") + " Players : " + trigger.Bold("{1}"), ServerBase.InstanceAsBase.UpTime.ToString("hh:mm:ss"), ServerBase.InstanceAsBase.ClientManager.Count);
        }
    }
}