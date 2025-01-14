﻿using Synapse;
using Synapse.Api;
using System.Linq;
using Synapse.Api.Events.SynapseEventArguments;
using Interactables.Interobjects;
using UnityEngine;
using MEC;
using PlayerStatsSystem;
using Logger = Synapse.Api.Logger;

namespace SCP_682_Synapse
{
    public class EventHandlers
    {
        public EventHandlers()
        {
            Server.Get.Events.Map.DoorInteractEvent += OnDoorInteract;
            Server.Get.Events.Player.PlayerSetClassEvent += OnSetClass;
            Server.Get.Events.Player.PlayerDamageEvent += OnDamage;
        }
        public void OnDoorInteract(DoorInteractEventArgs ev)
        {
            if (ev.Player.RoleID == 682)
            {
                if (SCP682.Config.can_PryGates && ev.Door.VDoor is PryableDoor pryableDoor)
                {
                    pryableDoor.TryPryGate();
                }
                else if (SCP682.Config.can_destroy_door)
                {
                    int destroychance = Random.Range(1, 100);
                    if (destroychance <= SCP682.Config.destroy_door_chance && ev.Door.IsBreakable)
                    {
                        ev.Door.TryBreakDoor();
                    }
                }
            }
        }

        public void OnSetClass(PlayerSetClassEventArgs ev)
        {
            if (ev.Role == RoleType.Scp93989)
            {
                int s = Random.Range(1, 100);
                if (s <= SCP682.Config.spawn_chance)
                {
                    Timing.CallDelayed(0.1f, () =>
                    {
                        ev.Player.RoleID = 682;
                    });
                }
            }
        }

        public void OnDamage(PlayerDamageEventArgs ev)
        {
            if (ev.Killer != null && ev.Victim != null && ev.Victim.RoleID != 682 && ev.Killer.RoleID == 682 && SynapseExtensions.CanHarmScp(ev.Killer, false))
            {
                if (SCP682.Config.can_kill_on_oneshot)
                {
                    ev.Victim.Kill();
                }
                ev.Killer.Heal(SCP682.Config.heal_hp_when_damage);
            }
        }
    }
}
