using CommunityCoreLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Verse;

namespace ED_TurretControl
{

    public class DetourInjector : SpecialInjector
    {

        public override bool Inject()
        {

            // ---------------------------------------------- Detour Plant.Resting ----------------------------------------------

            Log.Message("RimWorld_Building_TurretGun_CanSetForcedTarget.");
            PropertyInfo RimWorld_Building_TurretGun_CanSetForcedTarget = typeof(RimWorld.Building_TurretGun).GetProperty("CanSetForcedTarget", BindingFlags.NonPublic | BindingFlags.Instance);
            this.LogNULL(RimWorld_Building_TurretGun_CanSetForcedTarget, "RimWorld_Building_TurretGun_CanSetForcedTarget");

            Log.Message("RimWorld_Building_TurretGun_CanSetForcedTarget_Getter.");
            MethodInfo RimWorld_Building_TurretGun_CanSetForcedTarget_Getter = RimWorld_Building_TurretGun_CanSetForcedTarget.GetGetMethod(true);
            this.LogNULL(RimWorld_Building_TurretGun_CanSetForcedTarget_Getter, "RimWorld_Building_TurretGun_CanSetForcedTarget_Getter");
            
            Log.Message("ED_TurretGunt_CanSetForcedTarget.");
            PropertyInfo ED_TurretGunt_CanSetForcedTarget = typeof(ED_TurretControl.Detours._Building_TurretGunt).GetProperty("_CanSetForcedTarget", BindingFlags.NonPublic | BindingFlags.Instance);
            this.LogNULL(ED_TurretGunt_CanSetForcedTarget, "ED_TurretGunt_CanSetForcedTarget");

            Log.Message("ED_TurretGunt_CanSetForcedTarget_Getter.");
            MethodInfo ED_TurretGunt_CanSetForcedTarget_Getter = ED_TurretGunt_CanSetForcedTarget.GetGetMethod(true);
            this.LogNULL(ED_TurretGunt_CanSetForcedTarget_Getter, "ED_TurretGunt_CanSetForcedTarget_Getter");

            Log.Message("TryDetourFromTo.");
            if (!CommunityCoreLibrary.Detours.TryDetourFromTo(RimWorld_Building_TurretGun_CanSetForcedTarget_Getter, ED_TurretGunt_CanSetForcedTarget_Getter))
            {
                return false;
            }

            Log.Message("ED_TurretTargeting.DetourInjector.Inject() Compleated.");

            return true;
        }

        private void LogNULL(object objectToTest, String name, bool logSucess = false)
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
    }
}
