using BepInEx;
using HarmonyLib;
using BepInEx.Logging;
using System.Reflection;
using BepInEx.Unity.IL2CPP;

namespace RF5_FastMining;

[BepInPlugin(GUID, NAME, VERSION)]
[BepInProcess(GAME_PROCESS)]
public class Main : BasePlugin
{
    #region PluginInfo
    private const string GUID = "RF5_FastMining";
    private const string NAME = "RF5_FastMining";
    private const string VERSION = "1.1.0";
    private const string GAME_PROCESS = "Rune Factory 5.exe";
    #endregion

    internal static readonly ManualLogSource Logger = BepInEx.Logging.Logger.CreateLogSource("RF5_FastMining");

    internal void LoadConfig()
    {
        MiningPointDoBreak.LoadConfig(Config);
        MiningPointDoChop.LoadConfig(Config);
    }

    public override void Load()
    {
        LoadConfig();
        Harmony.CreateAndPatchAll(typeof(MiningPointDoChop));
        Harmony.CreateAndPatchAll(typeof(MiningPointDoBreak));
        Logger.LogInfo($"Plugin {NAME} is loaded!");
    }
}
