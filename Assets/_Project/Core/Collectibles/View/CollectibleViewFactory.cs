using UnityEngine;
using Zenject;

namespace TapTapTap.Core
{
    public class CollectibleViewFactory : IFactory<Object, CollectibleView>
    {
        private readonly IInstantiator instantiator;
        private readonly DiContainer diContainer;

        public CollectibleViewFactory(
            IInstantiator instantiator,
            DiContainer diContainer)
        {
            this.instantiator = instantiator;
            this.diContainer = diContainer;
        }

        public CollectibleView Create(Object prefab)
        {
            var entityView = instantiator.InstantiatePrefabForComponent<CollectibleView>(prefab);

            // Bind to subContainer
            diContainer.BindInstance(entityView);
            return entityView;
        }
    }
}