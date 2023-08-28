using System.Collections.Generic;
using UnityEngine;

namespace TapTapTap.Core
{
    public class SpawnerContainer : ISpawner
    {
        private readonly IEnumerable<ISpawner> spawners;

        public SpawnerContainer(
            IEnumerable<ISpawner> spawners)
        {
            this.spawners = spawners;
        }

        public TObject Spawn<TObject>(string name) where TObject : Component
        {
            foreach (var spawner in spawners) {
                var result = spawner.Spawn<TObject>(name);
                if (result != null) {
                    return result;
                }
            }

            return null;
        }

        public TObject Spawn<TObject>(string name, Transform parent) where TObject : Component
        {
            foreach (var spawner in spawners) {
                var result = spawner.Spawn<TObject>(name, parent);
                if (result != null) {
                    return result;
                }
            }

            return null;
        }

        public TObject Spawn<TObject>(string name, Transform parent, Vector3 position) where TObject : Component
        {
            foreach (var spawner in spawners) {
                var result = spawner.Spawn<TObject>(name, parent, position);
                if (result != null) {
                    return result;
                }
            }

            return null;
        }
    }
}