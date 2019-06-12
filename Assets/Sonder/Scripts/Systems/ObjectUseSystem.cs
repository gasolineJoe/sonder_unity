using System;
using LeopotamGroup.Ecs;
using Sonder.Scripts.Components.Abilities;
using Sonder.Scripts.Components.Abilities.Mind;
using Sonder.Scripts.Components.Entities;
using UnityEngine;
using Action = Sonder.Scripts.Components.Abilities.Mind.Action;

namespace Sonder.Scripts.Systems {
    [EcsInject]
    public class ObjectUseSystem : Delayed, IEcsRunSystem {
        EcsFilter<Human, ActionQueue> _humanUsers = null;

        public void Run() {
            if (CantUpdate()) return;

            for (var i = 0; i < _humanUsers.EntitiesCount; i++) {
                var actionQueue = _humanUsers.Components2[i];
                var human = _humanUsers.Components1[i];
                if (!actionQueue.HasActions() || actionQueue.GetAction().Item1 != Action.Use) continue;
                if (!(actionQueue.GetAction().Item2 is Usable usable)) continue;
                switch (usable.UsableType) {
                    case Usable.Type.Box:
                        break;
                    case Usable.Type.Door:
                        if (!(usable.UsableObject is Door door)) break;
                        var newPos = door.Destination.Usable.Body.Tr.position;
                        var newRoom = door.Destination.Source;

                        newPos = new Vector3(newPos.x, newRoom.Floor, newPos.z);
                        human.Body.Tr.position = newPos;

                        human.TravelTo(newRoom);
                        actionQueue.ActionDone();
                        break;
                }
            }
        }
    }
}