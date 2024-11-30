using HarmonyLib;

namespace RF5_FastMining;

[HarmonyPatch(typeof(MiningPoint), nameof(MiningPoint.DoBreak))]
public class MiningPointDoBreak
{
    private static int counter = 0;

    static void Prefix()
    {
        ++counter;
    }

    static void Postfix(HumanController humanController, MiningPoint __instance)
    {
        if (counter == 1){
            Main.Logger.LogDebug($"MiningPoint.DoBreak {__instance.mineType} {__instance.mineTypeDataTable.HP} {__instance.cropDataTable.CropHP}");
        }

        if (__instance.mineType == MineTypeID.Mine_Runecrystal ||
            counter >= Main.Config.GetInt("FastMining", "BreakCount", 9))
        {
            --counter;
            return;
        }

        if (__instance.mineTypeDataTable.HP > 0 || __instance.cropDataTable.CropHP > 0){
            __instance.DoBreak(humanController);
        }

        --counter;
    }
}

[HarmonyPatch(typeof(MiningPoint), nameof(MiningPoint.DoChop))]
public class MiningPointDoChop
{
    private static int counter = 0;

    static void Prefix()
    {
        ++counter;
    }

    static void Postfix(HumanController humanController, MiningPoint __instance)
    {
        if (counter == 1){
            Main.Logger.LogDebug($"MiningPoint.DoChop {__instance.mineType} {__instance.mineTypeDataTable.HP} {__instance.cropDataTable.CropHP}");
        }

        if (__instance.mineType == MineTypeID.Mine_Flower ||
            counter >= Main.Config.GetInt("FastMining", "ChopCount", 9))
        {
            --counter;
            return;
        }

        if (__instance.mineTypeDataTable.HP > 0 || __instance.cropDataTable.CropHP > 0){
            __instance.DoChop(humanController);
        }

        --counter;
    }
}
