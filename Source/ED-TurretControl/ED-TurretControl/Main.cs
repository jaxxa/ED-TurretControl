using Harmony;
using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Verse;

namespace EDTurretControl.ED_TurretControl
{
    [StaticConstructorOnStartup]
    class Main
    {
        static Main()
        {
            Log.Message("TurretControl, Starting Patching.");

            //var harmony = HarmonyInstance.Create("com.company.project.product");
            //var original = typeof(TheClass).GetMethod("TheMethod");
            //var prefix = typeof(MyPatchClass1).GetMethod("SomeMethod");
            //var postfix = typeof(MyPatchClass2).GetMethod("SomeMethod");
            //harmony.Patch(original, new HarmonyMethod(prefix), new HarmonyMethod(postfix));

            var harmony = HarmonyInstance.Create("Jaxxa.EnhancedDevelopment.TurretControl");
            //harmony.PatchAll(Assembly.GetExecutingAssembly());

            //Get the Origional Resting Property
            PropertyInfo RimWorld_BuildingTurretGun_CanSetForcedTarget = typeof(RimWorld.Building_TurretGun).GetProperty("CanSetForcedTarget", BindingFlags.NonPublic | BindingFlags.Instance);
            Main.LogNULL(RimWorld_BuildingTurretGun_CanSetForcedTarget, "RimWorld_BuildingTurretGun_CanSetForcedTarget", false);

            //Get the Resting Property Getter Method
            MethodInfo RimWorld_BuildingTurretGun_CanSetForcedTarget_Getter = RimWorld_BuildingTurretGun_CanSetForcedTarget.GetGetMethod(true);
            Main.LogNULL(RimWorld_BuildingTurretGun_CanSetForcedTarget, "RimWorld_BuildingTurretGun_CanSetForcedTarget", false);

            //Get the Prefix Patch
            var prefix = typeof(TurretControlPatcher).GetMethod("Prefix", BindingFlags.Public | BindingFlags.Static);
            Main.LogNULL(prefix, "Prefix", false);

            //Apply the Prefix Patch
            harmony.Patch(RimWorld_BuildingTurretGun_CanSetForcedTarget_Getter, new HarmonyMethod(prefix), null);

            Log.Message("TurretControl, Finished Patching.");

        }



        /// <summary>
        /// Debug Logging Helper
        /// </summary>
        /// <param name="objectToTest"></param>
        /// <param name="name"></param>
        /// <param name="logSucess"></param>
        private static void LogNULL(object objectToTest, String name, bool logSucess = false)
        {
            if (objectToTest == null)
            {
                Log.Error(name + " Is NULL.");
            }
            else
            {
                if (logSucess)
                {
                    Log.Message(name + " Is Not NULL.");
                }
            }
        }
        
        //[HarmonyPatch(typeof(Plant))]
        //[HarmonyPatch("Add")]
        //[HarmonyPatch("Resting_Getter")]
        static class TurretControlPatcher
        {

            // prefix
            // - wants instance, result and count
            // - wants to change count
            // - returns a boolean that controls if original is executed (true) or not (false)
            public static Boolean Prefix(ref bool __result, ref Building_TurretGun __instance)
            {

                //Write to log to debug id the patch is running.
                //Log.Message("Prefix Running");

                
                //Allow for all Turrets belonging to the Player
                if (__instance.Faction == Faction.OfPlayer)
                {
                    //Set result to true so targeting can be used and return fasle to stop origional check.
                    __result = true;
                    return false;
                }

                //Retuen true so the origional method is executed.
                return true;
            }

        }
    }
}
