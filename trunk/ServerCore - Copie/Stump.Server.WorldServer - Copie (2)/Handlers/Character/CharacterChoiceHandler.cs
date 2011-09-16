// /*************************************************************************
//  *
//  *  Copyright (C) 2010 - 2011 Stump Team
//  *
//  *  This program is free software: you can redistribute it and/or modify
//  *  it under the terms of the GNU General Public License as published by
//  *  the Free Software Foundation, either version 3 of the License, or
//  *  (at your option) any later version.
//  *
//  *  This program is distributed in the hope that it will be useful,
//  *  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  *  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//  *  GNU General Public License for more details.
//  *
//  *  You should have received a copy of the GNU General Public License
//  *  along with this program.  If not, see <http://www.gnu.org/licenses/>.
//  *
//  *************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Stump.BaseCore.Framework.Utils;
using Stump.Database.WorldServer;
using Stump.Database.WorldServer.Character;
using Stump.DofusProtocol.Classes;
using Stump.DofusProtocol.Enums;
using Stump.DofusProtocol.Messages;
using Stump.Server.DataProvider.Data.Threshold;
using Stump.Server.WorldServer.Helpers;
using Stump.Server.WorldServer.World.Actors.Character;
using Stump.Server.WorldServer.World.Entities.Characters;

namespace Stump.Server.WorldServer.Handlers
{
    public partial class CharacterHandler
    {

        [WorldHandler(typeof(CharacterFirstSelectionMessage))]
        public static void HandleCharacterFirstSelectionMessage(WorldClient client, CharacterFirstSelectionMessage message)
        {
            // TODO ADD TUTORIAL EFFECTS
            HandleCharacterSelectionMessage(client, message);
        }

        [WorldHandler(typeof(CharacterSelectionMessage))]
        public static void HandleCharacterSelectionMessage(WorldClient client, CharacterSelectionMessage message)
        {
            CharacterRecord character = client.Characters.First(entry => entry.Id == message.id);

            /* Check null */
            if (character == null)
            {
                client.Send(new CharacterSelectedErrorMessage());
                return;
            }

            /* Common selection */
            CommonCharacterSelection(client, character);
        }

        [WorldHandler(typeof(CharacterSelectionWithRecolorMessage))]
        public static void HandleCharacterSelectionWithRecolorMessage(WorldClient client, CharacterSelectionWithRecolorMessage message)
        {
            CharacterRecord character = client.Characters.First(entry => entry.Id == message.id);

            /* Check null */
            if (character == null)
            {
                client.Send(new CharacterSelectedErrorMessage());
                return;
            }

            /* Set Colors */
            character.BaseLook.indexedColors = message.indexedColor;
            character.Save();

            /* Common selection */
            CommonCharacterSelection(client, character);
        }

        [WorldHandler(typeof(CharacterSelectionWithRenameMessage))]
        public static void HandleCharacterSelectionWithRenameMessage(WorldClient client, CharacterSelectionWithRenameMessage message)
        {
            CharacterRecord character = client.Characters.First(entry => entry.Id == message.id);

            /* Check null */
            if (character == null)
            {
                client.Send(new CharacterSelectedErrorMessage());
                return;
            }

            /* Check if name is free */
            if (CharacterRecord.IsNameExists(message.name) || !Regex.IsMatch(StringUtils.FirstLetterUpper(message.name.ToLower()),
                               "^[A-Z][a-z]{2,9}(?:-[A-Z][a-z]{2,9}|[a-z]{1,10})$", RegexOptions.Compiled))
            {
                client.Send(new CharacterCreationResultMessage((int)CharacterCreationResultEnum.ERR_NAME_ALREADY_EXISTS));
                return;
            }

            /* Set new name */
            character.Name = StringUtils.FirstLetterUpper(message.name.ToLower());
            character.Save();

            /* Common selection */
            CommonCharacterSelection(client, character);
        }

        public static void CommonCharacterSelection(WorldClient client, CharacterRecord character)
        {
            client.ActiveCharacter = new Character(client, character);

            /* Check if we also have a world account */
            if (client.WorldAccount == null)
            {
                if (!WorldAccountRecord.Exists(client.Account.Id))
                    client.WorldAccount = WorldAccountManager.CreateWorldAccount(client);
                else
                    client.WorldAccount = WorldAccountRecord.FindById(client.Account.Id);
            }

            /* Update LastConnection and Last Ip */
            client.WorldAccount.LastConnection = DateTime.Now;
            client.WorldAccount.LastIp = client.IP;
            client.WorldAccount.Update();

            SendCharacterSelectedSuccessMessage(client);

            InventoryHandler.SendInventoryContentMessage(client);
            InventoryHandler.SendInventoryWeightMessage(client);

            InventoryHandler.SendSpellListMessage(client, true);
            ContextHandler.SendSpellForgottenMessage(client);
            ContextHandler.SendNotificationListMessage(client, new List<int>());

            ContextHandler.SendEmoteListMessage(client, new List<uint>());
            ChatHandler.SendEnabledChannelsMessage(client, new List<uint>(), new List<uint>());

            PvpHandler.SendAlignmentRankUpdateMessage(client);
            PvpHandler.SendAlignmentSubAreasListMessage(client);

            InitializationHandler.SendSetCharacterRestrictionsMessage(client);

            BasicHandler.SendTextInformationMessage(client, 1, 89);
            BasicHandler.SendTextInformationMessage(client, 0, 152,
                                                    client.Account.LastConnection.Year,
                                                    client.Account.LastConnection.Month,
                                                    client.Account.LastConnection.Day,
                                                    client.Account.LastConnection.Hour,
                                                    client.Account.LastConnection.Minute,
                                                    client.Account.LastIp ?? "{null}");

            InitializationHandler.SendOnConnectionEventMessage(client, 2);
        }

        [WorldHandler(typeof(CharactersListRequestMessage))]
        public static void HandleCharacterListRequest(WorldClient client, CharactersListRequestMessage message)
        {
            if (client.Account != null && client.Account.Login != "")
            {
                SendCharactersListMessage(client);
            }
            else
            {
                client.Send(new IdentificationFailedMessage((int)IdentificationFailureReasonEnum.KICKED));
                client.DisconnectLater(1000);
            }
        }

        public static void SendCharactersListMessage(WorldClient client)
        {
            List<CharacterBaseInformations> characters = client.Characters.Select(
                characterRecord =>
                new CharacterBaseInformations(
                    (uint)characterRecord.Id,
                    ThresholdManager.Instance["CharacterExp"].GetLevel(characterRecord.Experience),
                    characterRecord.Name,
                    CharacterManager.GetStuffedCharacterLook(characterRecord).EntityLook,
                    characterRecord.Breed,
                    characterRecord.Sex != SexTypeEnum.SEX_MALE)).ToList();

            client.Send(new CharactersListMessage(
                            client.WorldAccount.StartupActions.Count != 0,
                            characters
                            ));
        }

        public static void SendCharactersListWithModificationsMessage(WorldClient client)
        {
            var cbi = new List<CharacterBaseInformations>();
            var ctri = new List<CharacterToRecolorInformation>();
            var re = new List<int>();
            var oth = new List<int>();

            foreach (CharacterRecord c in client.Characters)
            {
                cbi.Add(new CharacterBaseInformations((uint)c.Id,
                                                      ThresholdManager.Instance["CharacterExp"].GetLevel(c.Experience),
                                                      c.Name, CharacterManager.GetStuffedCharacterLook(c).EntityLook,
                                                      c.Breed, c.Sex != SexTypeEnum.SEX_MALE));

                if (c.Rename)
                {
                    re.Add(c.Id);
                }

                if (c.Recolor)
                {
                    ctri.Add(new CharacterToRecolorInformation((uint)c.Id, c.BaseLook.indexedColors));
                }

                if (!(c.Recolor && c.Rename))
                {
                    oth.Add(c.Id);
                }
            }
            client.Send(new CharactersListWithModificationsMessage(client.WorldAccount.StartupActions.Count != 0, cbi, ctri,
                                                                   re, oth));
        }

        public static void SendCharacterSelectedSuccessMessage(WorldClient client)
        {
            client.Send(new CharacterSelectedSuccessMessage(client.ActiveCharacter.GetBaseInformations()));
        }
    }
}