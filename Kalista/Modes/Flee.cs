using System;
using System.Linq;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Events;
using EloBuddy.SDK.Rendering;
using SharpDX;
using Settings = Hellsing.Kalista.Config.Modes.Flee;

namespace Hellsing.Kalista.Modes
{
    public class Flee : ModeBase
    {
        public override void Execute()
        {
            if (Settings.UseWallJumps && Game.MapId == GameMapId.SummonersRift)
            {
                var spot = WallJump.GetJumpSpot();
                if (spot != null && Q.IsReady())
                {
                    Orbwalker.DisableAttacking = true;
                    Orbwalker.DisableMovement = true;

                    WallJump.JumpWall();
                    return;
                }
            }

            if (Settings.UseAutoAttacks)
            {
                Orbwalker.DisableAttacking = false;
                Orbwalker.DisableMovement = false;

                var target =
                    ObjectManager.Get<Obj_AI_Base>()
                    .FirstOrDefault(
                        x =>
                            x.IsValidTarget(Player.GetAutoAttackRange())
                            && !x.IsMe
                            && !x.IsAlly);

                Orbwalker.ForcedTarget = target;
            }
        }

        public override bool ShouldBeExecuted()
        {
            return Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.Flee);
        }
    }
}
