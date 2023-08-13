using UnityEngine;
using Zenject;

namespace TapTapTap.Core
{
    public class HealthBarFactory : IFactory<HealthBar>
    {
        private readonly IInstantiator instantiator;
        private readonly IUiPrefabProvider uiPrefabProvider;
        private readonly Canvas rootCanvas;

        public HealthBarFactory(
            IInstantiator instantiator,
            IUiPrefabProvider uiPrefabProvider,
            Canvas rootCanvas)
        {
            this.instantiator = instantiator;
            this.uiPrefabProvider = uiPrefabProvider;
            this.rootCanvas = rootCanvas;
        }

        public HealthBar Create()
        {
            return instantiator.InstantiatePrefabForComponent<HealthBar>(uiPrefabProvider.GetUiPrefab(typeof(HealthBar)), rootCanvas.transform);
        }
    }
}