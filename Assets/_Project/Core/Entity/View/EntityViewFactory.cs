using UnityEngine;
using Zenject;

namespace TapTapTap.Core
{
    public class EntityViewFactory : IFactory<Object, EntityView>
    {
        private readonly IInstantiator instantiator;
        private readonly DiContainer diContainer;

        public EntityViewFactory(
            IInstantiator instantiator,
            DiContainer diContainer)
        {
            this.instantiator = instantiator;
            this.diContainer = diContainer;
        }

        public EntityView Create(Object entityPrefab)
        {
            var entityView = instantiator.InstantiatePrefabForComponent<EntityView>(entityPrefab);

            // Bind to subContainer
            diContainer.BindInstance(entityView);
            return entityView;
        }
    }
}