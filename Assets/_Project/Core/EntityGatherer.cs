using System;
using System.Collections.Generic;
using UnityEngine;

namespace TapTapTap.Core.FSM
{
    public class EntityGatherer
    {
        private readonly IList<Entity> registeredEntities = new List<Entity>();

        public void Register(Entity entity)
        {
            registeredEntities.Add(entity);
        }

        public void Unregister(Entity entity)
        {
            registeredEntities.Remove(entity);
        }

        public Entity GetClosestEntityMatchingPredicate(Transform from, Predicate<Entity> predicate,
            float range = float.MaxValue)
        {
            var maxDistance = float.MaxValue;
            Entity candidate = null;

            foreach (var entity in registeredEntities) {
                if (!predicate(entity)) {
                    continue;
                }
                
                var distance = Vector3.Distance(from.position, entity.transform.position);
                if (maxDistance > distance && distance < range) {
                    maxDistance = distance;
                    candidate = entity;
                }
            }

            return candidate;
        }
    }
}