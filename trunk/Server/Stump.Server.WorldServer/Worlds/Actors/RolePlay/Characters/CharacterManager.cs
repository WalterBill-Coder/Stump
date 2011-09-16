using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Castle.ActiveRecord;
using Stump.Core.Attributes;
using Stump.Core.Reflection;
using Stump.DofusProtocol.Enums;
using Stump.DofusProtocol.Types;
using Stump.DofusProtocol.Types.Extensions;
using Stump.Server.WorldServer.Core.IPC;
using Stump.Server.WorldServer.Core.Network;
using Stump.Server.WorldServer.Database.Breeds;
using Stump.Server.WorldServer.Database.Characters;
using Stump.Server.WorldServer.Database.Items;
using Stump.Server.WorldServer.Worlds.Breeds;
using Stump.Server.WorldServer.Worlds.Spells;

namespace Stump.Server.WorldServer.Worlds.Actors.RolePlay.Characters
{
    public class CharacterManager : Singleton<CharacterManager>
    {
        /// <summary>
        ///   Maximum number of characters you can create/store in your account
        /// </summary>
        [Variable]
        public static uint MaxCharacterSlot = 5;

        private static readonly Regex m_nameCheckerRegex = new Regex("^[A-Z][a-z]{2,9}(?:-[A-Z][a-z]{2,9}|[a-z]{1,10})$", RegexOptions.Compiled);

        public List<CharacterRecord> GetCharactersByAccount(WorldClient client)
        {
            var characters = new List<CharacterRecord>();

            characters.AddRange(
                client.Account.CharactersId.Select(delegate(uint id)
                                                       {
                                                           try
                                                           {
                                                               return CharacterRecord.FindById((int) id);
                                                           }
                                                           catch (NotFoundException)
                                                           {
                                                               // character do not exist, then we remove it from the auth database
                                                               WorldServer.Instance.IOTaskPool.EnqueueTask(() =>
                                                                                                           IpcAccessor.Instance.ProxyObject.DeleteAccountCharacter(
                                                                                                               WorldServer.ServerInformation,
                                                                                                               client.Account.Id,
                                                                                                               id));
                                                               return null;
                                                           }
                                                       }).Where(character => character != null));

            return characters;
        }

        public CharacterCreationResultEnum CreateCharacter(WorldClient client, string name, sbyte breedId, bool sex, IEnumerable<int> colors)
        {
            if (client.Characters.Count >= MaxCharacterSlot)
                return CharacterCreationResultEnum.ERR_TOO_MANY_CHARACTERS;

            if (CharacterRecord.DoesNameExists(name))
                return CharacterCreationResultEnum.ERR_NAME_ALREADY_EXISTS;

            if (!m_nameCheckerRegex.IsMatch(name))
                return CharacterCreationResultEnum.ERR_INVALID_NAME;

            Breed breed = BreedManager.Instance.GetBreed(breedId);

            if (breed == null ||
                !client.Account.CanUseBreed(breedId) || !BreedManager.Instance.IsBreedAvailable(breedId))
                return CharacterCreationResultEnum.ERR_NOT_ALLOWED;

            var indexedColors = new List<int>();
            int i = 0;
            foreach (int color in colors)
            {
                List<uint> breedColors = !sex ? breed.MaleColors : breed.FemaleColors;
                if (breedColors.Count > i)
                {
                    if (color == -1)
                        indexedColors.Add((i + 1) << 24 | (int) breedColors[i]);
                    else
                        indexedColors.Add((i + 1) << 24 | color);
                }

                i++;
            }

            EntityLook look = !sex ? breed.MaleLook.Copy() : breed.FemaleLook.Copy();
            look.indexedColors = indexedColors;

            var record = new CharacterRecord(breed)
                             {
                                 Experience = ExperienceManager.Instance.GetCharacterLevel(breed.StartLevel),
                                 Name = name,
                                 Sex = sex ? SexTypeEnum.SEX_FEMALE : SexTypeEnum.SEX_MALE,
                                 EntityLook = look,
                             };

            record.Save(); // is it a good way ?

            var inventory = new InventoryRecord(record);
            record.Inventory = inventory;
            inventory.Save();
            // add items here

            var spells = new List<CharacterSpellRecord>();
            foreach (LearnableSpell learnableSpell in breed.LearnableSpells.Where(entry => entry.ObtainLevel <= breed.StartLevel))
            {
                CharacterSpellRecord spellRecord = SpellManager.Instance.CreateSpellRecord(record, SpellManager.Instance.GetSpellTemplate(learnableSpell.Id));
                spellRecord.Save();
                spells.Add(spellRecord);
            }

            record.Spells = spells;

            if (client.Characters == null)
                client.Characters = new List<CharacterRecord>();


            client.Characters.Insert(0, record);

            WorldServer.Instance.IOTaskPool.EnqueueTask(() => IpcAccessor.Instance.ProxyObject.AddAccountCharacter(WorldServer.ServerInformation,
                                                                                                                   client.Account.Id,
                                                                                                                   (uint) record.Id));

            return CharacterCreationResultEnum.OK;
        }

        public void DeleteCharacterOnAccount(CharacterRecord character, WorldClient client)
        {
            character.Delete();
            client.Characters.Remove(character);

            WorldServer.Instance.IOTaskPool.EnqueueTask(
                () => IpcAccessor.Instance.ProxyObject.DeleteAccountCharacter(WorldServer.ServerInformation, client.Account.Id, (uint) character.Id));
        }

        #region Character Name Random Generation

        private const string Vowels = "aeiouy";
        private const string Consonants = "bcdfghjklmnpqrstvwxz";

        public string GenerateName()
        {
            string name;

            do
            {
                var rand = new Random();
                int namelen = rand.Next(5, 10);
                name = string.Empty;

                name += char.ToUpper(RandomConsonant(rand));

                for (int i = 0; i < namelen - 1; i++)
                {
                    name += ((i & 1) != 1) ? RandomConsonant(rand) : RandomVowel(rand);
                }
            } while (CharacterRecord.DoesNameExists(name));

            return name;
        }

        private static char RandomVowel(Random rand)
        {
            return Vowels[rand.Next(0, Vowels.Length - 1)];
        }

        private static char RandomConsonant(Random rand)
        {
            return Consonants[rand.Next(0, Consonants.Length - 1)];
        }

        #endregion
    }
}