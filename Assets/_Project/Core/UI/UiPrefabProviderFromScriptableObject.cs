using System;
using UnityEngine;

namespace TapTapTap.Core
{
    public class UiPrefabProviderFromScriptableObject : IUiPrefabProvider
    {
        private readonly UiPrefabs uiPrefabs;

        public UiPrefabProviderFromScriptableObject(UiPrefabs uiPrefabs)
        {
            this.uiPrefabs = uiPrefabs;
        }

        public GameObject GetUiPrefab(Type type)
        {
            return uiPrefabs.GetUiPrefab(type);
        }
    }
}