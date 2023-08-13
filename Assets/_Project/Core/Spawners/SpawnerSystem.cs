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

        public Entity SpawnEntity(string name)
        {
            var archetype = archetypeProvider.GetArchetype(name);
            var entity = entityFactory.Create(new EntityData {
                EntityArchetype = archetype,
            });

            return entity;
        }

        public Entity SpawnEntity(string name, Transform parent)
        {
            var entity = SpawnEntity(name);
            entity.transform.SetParent(parent, false);

            return entity;
        }

        public Entity SpawnEntity(string name, Transform parent, Vector3 position)
        {
            var entity = SpawnEntity(name, parent);
            entity.transform.localPosition = position;

            return entity;
        }
    }
}