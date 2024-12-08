using BepInEx.Configuration;
using HarmonyLib;

namespace RF5_FastMining;

[HarmonyPatch(typeof(MiningPoint), nameof(MiningPoint.DoBreak))]
internal static class MiningPointDoBreak
{
    private static int counter = 0;
    private const string FastMiningSection = "FastMining";
    internal static ConfigEntry<int> BreakCount { get; private set; }
    private static int GameDefault = 1;
    private static int ModDefault = 9;
    private static readonly ConfigDescription BreakCountDescription = new ConfigDescription(
        $"Set to desired rock break hits . {Environment.NewLine} 1 = Mod Disabled/Game default, > 1 = number of hits to register",
        new AcceptableValueRange<int>(GameDefault, 10));

    internal static void LoadConfig(ConfigFile Config)
    {
        BreakCount = Config.Bind(FastMiningSection, nameof(BreakCount), ModDefault, BreakCountDescription);
    }

    internal static void Postfix(HumanController humanController, MiningPoint __instance)
    {
        if (BreakCount.Value == GameDefault)
        {
            return;
        }

        counter++;
        if (counter == GameDefault)
        {
            FastMiningPlugin.Log.LogDebug($"MiningPoint.DoBreak {__instance.mineType} {__instance.mineTypeDataTable.HP} {__instance.cropDataTable.CropHP}");
        }

        if (__instance.mineType == MineTypeID.Mine_Runecrystal || counter >= BreakCount.Value)
        {
            counter--;
            return;
        }

        if (__instance.mineTypeDataTable.HP > 0 || __instance.cropDataTable.CropHP > 0)
        {
            __instance.DoBreak(humanController);
        }

        counter--;
    }
}

[HarmonyPatch(typeof(MiningPoint), nameof(MiningPoint.DoChop))]
internal static class MiningPointDoChop
{
    private static int counter = 0;
    private const string FastChoppingSection = "FastChopping";
    internal static ConfigEntry<int> ChopCount { get; private set; }
    private static int GameDefault = 1;
    private static int ModDefault = 9;
    private static readonly ConfigDescription ChopCountDescription = new ConfigDescription(
    $"Set to desired tree break hits . {Environment.NewLine} 1 = Mod Disabled/Game default, > 1 = number of hits to register",
    new AcceptableValueRange<int>(GameDefault, 10));

    internal static void LoadConfig(ConfigFile Config)
    {
        ChopCount = Config.Bind(FastChoppingSection, nameof(ChopCount), ModDefault, ChopCountDescription);
    }

    internal static void Postfix(HumanController humanController, MiningPoint __instance)
    {
        if (ChopCount.Value == GameDefault)
        {
            return;
        }

        counter++;
        if (counter == GameDefault)
        {
            FastMiningPlugin.Log.LogDebug($"MiningPoint.DoChop {__instance.mineType} {__instance.mineTypeDataTable.HP} {__instance.cropDataTable.CropHP}");
        }

        if (__instance.mineType == MineTypeID.Mine_Flower || counter >= ChopCount.Value)
        {
            counter--;
            return;
        }

        if (__instance.mineTypeDataTable.HP > 0 || __instance.cropDataTable.CropHP > 0)
        {
            __instance.DoChop(humanController);
        }

        counter--;
    }
}
