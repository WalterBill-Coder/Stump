﻿using System.Linq;
using Stump.Core.Reflection;
using Stump.DofusProtocol.Enums;
using Stump.DofusProtocol.Messages;
using Stump.Core.Extensions;
using Stump.DofusProtocol.Types;
using Stump.Server.BaseServer.Network;
using Stump.Server.WorldServer.Core.Network;
using Stump.Server.WorldServer.Game;
using Stump.Server.WorldServer.Game.Dialogs.Guilds;
using Stump.Server.WorldServer.Game.Guilds;
using Stump.Server.WorldServer.Game.Actors.RolePlay.Characters;
using Stump.Server.WorldServer.Handlers.TaxCollector;
using GuildMember = Stump.Server.WorldServer.Game.Guilds.GuildMember;

namespace Stump.Server.WorldServer.Handlers.Guilds
{
    public class GuildHandler : WorldHandlerContainer
    {
        [WorldHandler(GuildGetInformationsMessage.Id)]
        public static void HandleGuildGetInformationsMessage(WorldClient client, GuildGetInformationsMessage message)
        {
            if (client.Character.Guild == null)
                return;

            switch (message.infoType)
            {
                case (sbyte)GuildInformationsTypeEnum.INFO_GENERAL:
                    SendGuildInformationsGeneralMessage(client);
                    break;
                case (sbyte)GuildInformationsTypeEnum.INFO_MEMBERS:
                    SendGuildInformationsMembersMessage(client);
                    break;
                case (sbyte)GuildInformationsTypeEnum.INFO_BOOSTS:
                    SendGuildInfosUpgradeMessage(client);
                    break;
                case (sbyte)GuildInformationsTypeEnum.INFO_PADDOCKS:
                    SendGuildInformationsPaddocksMessage(client);
                    break;
                case (sbyte)GuildInformationsTypeEnum.INFO_HOUSES:
                    SendGuildHousesInformationMessage(client);
                    break;
                case (sbyte)GuildInformationsTypeEnum.INFO_TAX_COLLECTOR:
                    TaxCollectorHandler.SendTaxCollectorListMessage(client);
                    break;
                case (sbyte)GuildInformationsTypeEnum.INFO_TAX_COLLECTOR_LEAVE:
                    break;
            }
        }

        [WorldHandler(GuildCharacsUpgradeRequestMessage.Id)]
        public static void HandleGuildCharacsUpgradeRequestMessage(WorldClient client, GuildCharacsUpgradeRequestMessage message)
        {
            if (client.Character.Guild == null)
                return;

            if (!client.Character.GuildMember.HasRight(GuildRightsBitEnum.GUILD_RIGHT_MANAGE_GUILD_BOOSTS))
                return;

            switch (message.charaTypeTarget)
            {
                case 0: //Pods
                    client.Character.Guild.UpgradePods();
                    break;
                case 1: //Prospecting
                    client.Character.Guild.UpgradeProspecting();
                    break;
                case 2: //Wisdom
                    client.Character.Guild.UpgradeWisdom();
                    break;
                case 3: //MaxTaxCollectors
                    client.Character.Guild.UpgradeMaxTaxCollectors();
                    break;
            }

            SendGuildInfosUpgradeMessage(client);
        }

        [WorldHandler(GuildSpellUpgradeRequestMessage.Id)]
        public static void HandleGuildSpellUpgradeRequestMessage(WorldClient client, GuildSpellUpgradeRequestMessage message)
        {
            if (client.Character.Guild == null)
                return;

            if (client.Character.Guild.UpgradeSpell(message.spellId))
                SendGuildInfosUpgradeMessage(client);
        }

        [WorldHandler(GuildCreationValidMessage.Id)]
        public static void HandleGuildCreationValidMessage(WorldClient client, GuildCreationValidMessage message)
        {
            var panel = client.Character.Dialog as GuildCreationPanel;
            if (panel != null)
            {
                panel.CreateGuild(message.guildName, message.guildEmblem);
            }
        }

        [WorldHandler(GuildChangeMemberParametersMessage.Id)]
        public static void HandleGuildChangeMemberParametersMessage(WorldClient client, GuildChangeMemberParametersMessage message)
        {
            if (client.Character.Guild == null)
                return;

            var target = client.Character.Guild.TryGetMember(message.memberId);
            if (target == null)
                return;

            client.Character.Guild.ChangeParameters(client.Character, target, message.rank,
                                                    (byte) message.experienceGivenPercent, message.rights);
        }

        [WorldHandler(GuildKickRequestMessage.Id)]
        public static void HandleGuildKickRequestMessage(WorldClient client, GuildKickRequestMessage message)
        {
            if (client.Character.Guild == null)
                return;

            var target = client.Character.Guild.TryGetMember(message.kickedId);
            if (target == null)
                return;

            target.Guild.KickMember(client.Character, target);
        }

        [WorldHandler(GuildInvitationMessage.Id)]
        public static void HandleGuildInvitationMessage(WorldClient client, GuildInvitationMessage message)
        {
            if (client.Character.Guild == null)
                return;

            if (!client.Character.GuildMember.HasRight(GuildRightsBitEnum.GUILD_RIGHT_INVITE_NEW_MEMBERS))
            {
                // Vous n'avez pas le droit requis pour inviter des joueurs dans votre guilde.
                client.Character.SendInformationMessage(TextInformationTypeEnum.TEXT_INFORMATION_ERROR, 207);
                return;
            }

            var target = Singleton<World>.Instance.GetCharacter(message.targetId);
            if (target == null)
            {
                // Impossible d'inviter, ce joueur est inconnu ou non connecté.
                client.Character.SendInformationMessage(TextInformationTypeEnum.TEXT_INFORMATION_ERROR, 208);
                return;
            }

            if (target.Guild != null)
            {
                // Impossible, ce joueur est déjà dans une guilde
                client.Character.SendInformationMessage(TextInformationTypeEnum.TEXT_INFORMATION_ERROR, 206);
                return;
            }

            if (target.IsBusy())
            {
                // Ce joueur est occupé. Impossible de l'inviter.                    
                client.Character.SendInformationMessage(TextInformationTypeEnum.TEXT_INFORMATION_ERROR, 209);
                return;
            }

            if (!client.Character.Guild.CanAddMember())
            {
                client.Character.SendInformationMessage(TextInformationTypeEnum.TEXT_INFORMATION_ERROR, 55, Guild.MaxMembersNumber);
                return;
            }

            var request = new GuildInvitationRequest(client.Character, target);
            request.Open();
        }

        [WorldHandler(GuildInvitationByNameMessage.Id)]
        public static void HandleGuildInvitationByNameMessage(WorldClient client, GuildInvitationByNameMessage message)
        {
            if (client.Character.Guild == null)
                return;

            if (!client.Character.GuildMember.HasRight(GuildRightsBitEnum.GUILD_RIGHT_INVITE_NEW_MEMBERS))
            {
                // Vous n'avez pas le droit requis pour inviter des joueurs dans votre guilde.
                client.Character.SendInformationMessage(TextInformationTypeEnum.TEXT_INFORMATION_ERROR, 207);
                return;
            }

            var target = Singleton<World>.Instance.GetCharacter(message.name);
            if (target == null)
            {
                // Impossible d'inviter, ce joueur est inconnu ou non connecté.
                client.Character.SendInformationMessage(TextInformationTypeEnum.TEXT_INFORMATION_ERROR, 208);
                return;
            }

            if (target.Guild != null)
            {
                // Impossible, ce joueur est déjà dans une guilde
                client.Character.SendInformationMessage(TextInformationTypeEnum.TEXT_INFORMATION_ERROR, 206);
                return;
            }

            if (target.IsBusy())
            {
                // Ce joueur est occupé. Impossible de l'inviter.                    
                client.Character.SendInformationMessage(TextInformationTypeEnum.TEXT_INFORMATION_ERROR, 209);
                return;
            }

            var request = new GuildInvitationRequest(client.Character, target);
            request.Open();
        }

        [WorldHandler(GuildInvitationAnswerMessage.Id)]
        public static void HandleGuildInvitationAnswerMessage(WorldClient client, GuildInvitationAnswerMessage message)
        {
            var request = client.Character.RequestBox as GuildInvitationRequest;

            if (request == null)
                return;

            if (client.Character == request.Source && !message.accept)
                request.Cancel();
            else if (client.Character == request.Target)
            {
                if (message.accept)
                    request.Accept();
                else
                    request.Deny();
            }
        }

        [WorldHandler(GuildMemberSetWarnOnConnectionMessage.Id)]
        public static void HandleGuildMemberSetWarnOnConnectionMessage(WorldClient client, GuildMemberSetWarnOnConnectionMessage message)
        {
            client.Character.WarnOnGuildConnection = message.enable;
        }

        public static void SendGuildMemberWarnOnConnectionStateMessage(IPacketReceiver client, bool state)
        {
            client.Send(new GuildMemberWarnOnConnectionStateMessage(state));
        }

        public static void SendGuildInvitedMessage(IPacketReceiver client, Character recruter)
        {
            client.Send(new GuildInvitedMessage(recruter.Id, recruter.Name, recruter.Guild.GetBasicGuildInformations()));
        }

        public static void SendGuildInvitationStateRecrutedMessage(IPacketReceiver client, GuildInvitationStateEnum state)
        {
            client.Send(new GuildInvitationStateRecrutedMessage((sbyte)state));
        }

        public static void SendGuildInvitationStateRecruterMessage(IPacketReceiver client, Character recruted, GuildInvitationStateEnum state)
        {
            client.Send(new GuildInvitationStateRecruterMessage(recruted.Name, (sbyte)state));
        }

        public static void SendGuildLeftMessage(IPacketReceiver client)
        {
            client.Send(new GuildLeftMessage());
        }

        public static void SendGuildCreationResultMessage(IPacketReceiver client, GuildCreationResultEnum result)
        {
            client.Send(new GuildCreationResultMessage((sbyte)result));
        }

        public static void SendGuildMembershipMessage(IPacketReceiver client, GuildMember member)
        {
            client.Send(new GuildMembershipMessage(member.Guild.GetGuildInformations(), (uint)member.Rights, true));
        }

        public static void SendGuildInformationsGeneralMessage(WorldClient client)
        {
            client.Send(new GuildInformationsGeneralMessage(true, false, client.Character.Guild.Level, client.Character.Guild.ExperienceLevelFloor, client.Character.Guild.Experience,
                client.Character.Guild.ExperienceNextLevelFloor, client.Character.Guild.CreationDate.GetUnixTimeStamp())); 
        }

        public static void SendGuildInformationsMembersMessage(WorldClient client)
        {
            client.Send(new GuildInformationsMembersMessage(client.Character.Guild.Members.Select(x => x.GetNetworkGuildMember())));
        }

        public static void SendGuildInformationsMemberUpdateMessage(IPacketReceiver client, GuildMember member)
        {
            client.Send(new GuildInformationsMemberUpdateMessage(member.GetNetworkGuildMember()));
        }

        public static void SendGuildInfosUpgradeMessage(WorldClient client)
        {
            client.Send(client.Character.Guild.GetGuildInfosUpgrade());
        }

        public static void SendGuildInformationsPaddocksMessage(IPacketReceiver client)
        {
            client.Send(new GuildInformationsPaddocksMessage(0, new PaddockContentInformations[0]));
        }

        public static void SendGuildHousesInformationMessage(IPacketReceiver client)
        {
            client.Send(new GuildHousesInformationMessage(new HouseInformationsForGuild[0]));
        }

        public static void SendGuildJoinedMessage(IPacketReceiver client, GuildMember member)
        {
            client.Send(new GuildJoinedMessage(member.Guild.GetGuildInformations(), (uint)member.Rights, true));
        }

        public static void SendGuildMemberLeavingMessage(IPacketReceiver client, GuildMember member, bool kicked)
        {
            client.Send(new GuildMemberLeavingMessage(kicked, member.Id));
        }

        public static void SendGuildCreationStartedMessage(IPacketReceiver client)
        {
            client.Send(new GuildCreationStartedMessage());
        }
    }
}
