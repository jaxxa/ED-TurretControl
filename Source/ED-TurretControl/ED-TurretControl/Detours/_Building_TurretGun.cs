using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Verse;
using RimWorld;
using UnityEngine;

namespace ED_TurretControl.Detours
{
    internal class _Building_TurretGunt : RimWorld.Building_TurretGun
    {

        private Boolean _CanSetForcedTarget
        {
            get
            {
                //This mirrors the A14 Implementation of CanToggleHoldFire

                //Allow for all Turrets belonging to the Player
                if (this.Faction == Faction.OfPlayer)
                {
                    return true;
                }

                //Allow for all Turrets manned by the player.
                if (this.mannableComp != null && this.mannableComp.ManningPawn != null)
                {
                    return this.mannableComp.ManningPawn.Faction == Faction.OfPlayer;
                }

                //Return False if neither  of those conditions has been met.
                return false;


            }
        }

    }
}
