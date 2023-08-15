using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace TapTapTap.Core
{
    public class UiPrefabProviderFromScriptableObject : IUiPrefabProvider
    {
        private readonly UiPrefabs uiPrefabs;
        private readonly Canvas rootCanvas;

        public UiPrefabProviderFromScriptableObject(
            UiPrefabs uiPrefabs,
            Canvas rootCanvas)
        {
            this.uiPrefabs = uiPrefabs;
            this.rootCanvas = rootCanvas;
        }

        public GameObject GetUiPrefab(Type type)
        {
            var uiPrefab = uiPrefabs.GetUiPrefab(type);
            return uiPrefab != null ? Object.Instantiate(uiPrefab, rootCanvas.transform) : null;
        }
    }
}