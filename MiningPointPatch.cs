using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HarmonyLib;

namespace RF5_FastMining
{
    // 锤石头
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
            if (counter == 1)
                Main.Logger.LogDebug(string.Format("MiningPoint.DoBreak {0} {1} {2}", __instance.mineType, __instance.mineTypeDataTable.HP, __instance.cropDataTable.CropHP));

            if (__instance.mineType == MineTypeID.Mine_Runecrystal ||
                counter >= Main.Config.GetInt("FastMining", "BreakCount", 9))
            {
                --counter;
                return;
            }

            if (__instance.mineTypeDataTable.HP > 0 || __instance.cropDataTable.CropHP > 0)
                __instance.DoBreak(humanController);    // 递归调用

            --counter;
        }
    }

    // 劈树桩
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
            if (counter == 1)
                Main.Logger.LogDebug(string.Format("MiningPoint.DoChop {0} {1} {2}", __instance.mineType, __instance.mineTypeDataTable.HP, __instance.cropDataTable.CropHP));

            if (__instance.mineType == MineTypeID.Mine_Flower ||
                counter >= Main.Config.GetInt("FastMining", "ChopCount", 9))
            {
                --counter;
                return;
            }

            if (__instance.mineTypeDataTable.HP > 0 || __instance.cropDataTable.CropHP > 0)
                __instance.DoChop(humanController);     // 递归调用

            --counter;
        }
    }
}
