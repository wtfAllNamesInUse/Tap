using UnityEngine;
using Zenject;

namespace TapTapTap.Core
{
    public class Spawner<TObject, TFactory, TArchetypeProvider, TArchetype> : ISpawner
        where TObject : Component
        where TFactory : PlaceholderFactory<TArchetype, TObject>
        where TArchetypeProvider : IArchetypeProvider<TArchetype>
    {
        private readonly TFactory factory;
        private readonly TArchetypeProvider archetypeProvider;

        public Spawner(
            TFactory factory,
            TArchetypeProvider archetypeProvider)
        {
            this.factory = factory;
            this.archetypeProvider = archetypeProvider;
        }

        public TSpawnedObject Spawn<TSpawnedObject>(string name)
            where TSpawnedObject : Component
        {
            var archetype = archetypeProvider.GetArchetype(name);
            if (archetype == null) {
                return null;
            }

            var entity = factory.Create(archetype) as TSpawnedObject;

            return entity;
        }

        public TSpawnedObject Spawn<TSpawnedObject>(string name, Transform parent)
            where TSpawnedObject : Component
        {
            var entity = Spawn<TSpawnedObject>(name);
            if (entity != null) {
                entity.transform.SetParent(parent, false);
            }

            return entity;
        }

        public TSpawnedObject Spawn<TSpawnedObject>(string name, Transform parent, Vector3 position)
            where TSpawnedObject : Component
        {
            var entity = Spawn<TSpawnedObject>(name, parent);
            if (entity != null) {
                entity.transform.position = position;
            }

            return entity;
        }
    }
}