using MelonLoader;
using HarmonyLib;
using Il2Cpp;
using piercing_magma_axis_06;

[assembly: MelonInfo(typeof(PiercingMagmaAxis06), "Piercing Magma Axis [no Repel] (ver. 0.6)", "1.0.0", "Matthiew Purple")]
[assembly: MelonGame("アトラス", "smt3hd")]

namespace piercing_magma_axis_06;
public class PiercingMagmaAxis06 : MelonMod
{
    // After getting the effectiveness of an attack
    [HarmonyPatch(typeof(nbCalc), nameof(nbCalc.nbGetAisyo))]
    private class Patch
    {
        public static void Postfix(ref int nskill, ref uint __result)
        {
            // If the attack is Magma Axis and it's resisted/blocked/drained/repelled
            if (nskill == 161 && (__result < 100 || (__result > 999 && __result < 100000) || (__result > 200000 && __result < 1000000000)))
            {
                __result = 100; // Forces the affinity to become "neutral"
                nbMainProcess.nbGetMainProcessData().d31_kantuu = 1; // Displays the "Pierced!" message
            }
        }
    }

    // After getting the description of a skill
    [HarmonyPatch(typeof(datSkillHelp_msg), nameof(datSkillHelp_msg.Get))]
    private class Patch2
    {
        public static void Postfix(ref int id, ref string __result)
        {
            if (id == 161) __result = "Fire damage to one foe. \nIgnores all resistances \nexcept Repel."; // New skill description of Magma Axis
        }
    }
}
