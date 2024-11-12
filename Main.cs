using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BepInEx;
using HarmonyLib;
using BepInEx.Logging;
using System.Reflection;
using System.IO;
using BepInEx.Unity.IL2CPP;

namespace RF5_FastMining
{
    [BepInPlugin(GUID, NAME, VERSION)]
    [BepInProcess(GAME_PROCESS)]
    public class Main : BasePlugin
    {
        #region PluginInfo
        private const string GUID = "0328C54A-E525-486C-9D69-AD02526598F8";
        private const string NAME = "RF5_FastMining";
        private const string VERSION = "1.0.1";
        private const string GAME_PROCESS = "Rune Factory 5.exe";
        #endregion

        public static ManualLogSource Logger;
        private static string FILENAME = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\" + NAME + ".ini";
        public static new IniParser Config;

        public override void Load()
        {
            Logger = Log;
            Config = new IniParser(FILENAME);
            Harmony.CreateAndPatchAll(typeof(MiningPointDoChop));
            Harmony.CreateAndPatchAll(typeof(MiningPointDoBreak));
            Logger.LogInfo($"Plugin {MyPluginInfo.PLUGIN_GUID} is loaded!");
        }
    }
}
