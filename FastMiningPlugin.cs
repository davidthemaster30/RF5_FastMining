using BepInEx;
using HarmonyLib;
using BepInEx.Logging;
using System.Reflection;
using BepInEx.Unity.IL2CPP;

namespace RF5_FastMining;

[BepInPlugin(MyPluginInfo.PLUGIN_GUID, MyPluginInfo.PLUGIN_NAME, MyPluginInfo.PLUGIN_VERSION)]
[BepInProcess(GAME_PROCESS)]
public class FastMiningPlugin : BasePlugin
{
    private const string GAME_PROCESS = "Rune Factory 5.exe";

    internal static new readonly ManualLogSource Log = BepInEx.Logging.Logger.CreateLogSource("RF5_FastMining");

    internal void LoadConfig()
    {
        MiningPointDoBreak.LoadConfig(Config);
        MiningPointDoChop.LoadConfig(Config);
    }

    public override void Load()
    {
        Log.LogInfo($"Plugin {MyPluginInfo.PLUGIN_NAME} {MyPluginInfo.PLUGIN_VERSION} is loading!");

        LoadConfig();
        Harmony.CreateAndPatchAll(typeof(MiningPointDoChop));
        Harmony.CreateAndPatchAll(typeof(MiningPointDoBreak));

        Log.LogInfo($"Plugin {MyPluginInfo.PLUGIN_NAME} {MyPluginInfo.PLUGIN_VERSION} is loaded!");
    }
}
