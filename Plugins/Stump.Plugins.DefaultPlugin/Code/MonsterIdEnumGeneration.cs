﻿using System.IO;
using System.Text.RegularExpressions;
using NLog;
using Stump.Core.Attributes;
using Stump.Server.BaseServer.Initialization;
using Stump.Server.WorldServer.Game.Items;
using Stump.Core.Extensions;
using System.Text;
using Stump.Server.WorldServer.Game.Actors.RolePlay.Monsters;

namespace Stump.Plugins.DefaultPlugin.Code
{
    public class MonsterIdEnumGeneration
    {
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();
        [Variable]
        public static bool Active = true;

        [Variable]
        public static string Output = "Gen/MonsterIdEnum.cs";

        [Initialization(typeof(MonsterManager), Silent = true)]
        public static void Initialize()
        {
            if (!Active)
                return;

            logger.Debug("Generate {0} ...", Output);

            var file = Path.Combine(Plugin.CurrentPlugin.GetPluginDirectory(), Output);

            if (!Directory.Exists(Path.GetDirectoryName(file)))
                Directory.CreateDirectory(Path.GetDirectoryName(file));

            using (var writer = File.CreateText(file))
            {
                writer.WriteLine("namespace Stump.DofusProtocol.Enums");
                writer.WriteLine("{");
                writer.WriteLine("\tpublic enum MonsterIdEnum");
                writer.WriteLine("\t{");
                foreach (var monster in MonsterManager.Instance.GetTemplates())
                {
                    writer.WriteLine("\t\t{0}_{1} = {1},", RemoveSpecialCharacters(EscapeString(monster.Name.RemoveAccents())).ToUpper(), monster.Id);
                }

                writer.WriteLine("\t}");
                writer.WriteLine("}");
                writer.Flush();
            }
        }

        private static string EscapeString(string str)
        {
            var builder = new StringBuilder(str);
            builder.Replace(" ", "_");
            builder.Replace("\"", "");
            builder.Replace("'", "");
            builder.Replace("-", "");
            return builder.ToString();
        }

        public static string RemoveSpecialCharacters(string str) => Regex.Replace(Regex.Replace(str, "[^a-zA-Z0-9_]+", "", RegexOptions.Compiled), "[_]{2,}", "_");
    }
}
