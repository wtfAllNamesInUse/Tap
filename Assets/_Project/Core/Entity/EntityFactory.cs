using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace TapTapTap.Core
{
    public class EntityFactory : IFactory<Object, EntityData, Entity>
    {
        private readonly IInstantiator instantiator;
        private readonly IUiPrefabProvider uiPrefabProvider;
        private readonly Canvas rootCanvas;
        private readonly Camera camera;

        public EntityFactory(
            IInstantiator instantiator,
            IUiPrefabProvider uiPrefabProvider,
            Canvas rootCanvas,
            Camera camera)
        {
            this.instantiator = instantiator;
            this.uiPrefabProvider = uiPrefabProvider;
            this.rootCanvas = rootCanvas;
            this.camera = camera;
        }

        public Entity Create(Object entityPrefab, EntityData data)
        {
            var healthbar = instantiator.InstantiatePrefabForComponent<HealthBar>(uiPrefabProvider.GetUiPrefab(typeof(HealthBar)), rootCanvas.transform);
            var entity = instantiator.InstantiatePrefabForComponent<Entity>(entityPrefab, new List<object>() { data, healthbar });

            healthbar.InitWithEntity(entity, camera);

            return entity;
        }
    }
}