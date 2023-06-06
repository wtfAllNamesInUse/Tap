using UnityEngine;

namespace TapTapTap.Core
{
    public class SpawnerSystem
    {
        private readonly Entity.Factory entityFactory;
        private readonly ArchetypeProvider archetypeProvider;

        public SpawnerSystem(
            Entity.Factory entityFactory,
            ArchetypeProvider archetypeProvider)
        {
            this.entityFactory = entityFactory;
            this.archetypeProvider = archetypeProvider;
        }

        public Entity SpawnEntity(string name, EntityDirection direction)
        {
            var archetype = archetypeProvider.GetArchetype(name);
            var entity = entityFactory.Create(archetype.Prefab, new EntityData {
                Direction = direction,
                EntityArchetype = archetype,
            });

            return entity;
        }

        public Entity SpawnEntity(string name, EntityDirection direction, Transform parent)
        {
            var entity = SpawnEntity(name, direction);
            entity.transform.SetParent(parent, false);

            return entity;
        }

        public Entity SpawnEntity(string name, EntityDirection direction, Transform parent, Vector3 position)
        {
            var entity = SpawnEntity(name, direction, parent);
            entity.transform.localPosition = position;

            return entity;
        }
    }
}