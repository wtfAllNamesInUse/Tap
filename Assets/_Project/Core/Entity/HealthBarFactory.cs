using TapTapTap.Ui;
using UnityEngine;
using Zenject;

namespace TapTapTap.Core
{
    public class HealthBarFactory : IFactory<HealthBar>
    {
        private readonly IUiPrefabProvider uiPrefabProvider;
        private readonly Canvas rootCanvas;
        private readonly DiContainer diContainer;

        public HealthBarFactory(
            IUiPrefabProvider uiPrefabProvider,
            Canvas rootCanvas,
            DiContainer diContainer)
        {
            this.uiPrefabProvider = uiPrefabProvider;
            this.rootCanvas = rootCanvas;
            this.diContainer = diContainer;
        }

        public HealthBar Create()
        {
            var prefab = uiPrefabProvider.GetUiPrefab(typeof(HealthBar));
            if (prefab == null) {
                return null;
            }
            
            diContainer.InjectGameObject(prefab);
            prefab.transform.SetParent(rootCanvas.transform);
            return prefab.GetComponent<HealthBar>();
        }
    }
}