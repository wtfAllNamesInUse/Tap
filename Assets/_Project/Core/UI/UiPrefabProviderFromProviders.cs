using System;
using UnityEngine;
using Zenject;

namespace TapTapTap.Core
{
    public class UiPrefabProviderFromProviders : IUiPrefabProvider
    {
        private readonly IUiPrefabProvider[] prefabProviders;

        public UiPrefabProviderFromProviders(
            IUiPrefabProvider[] prefabProviders)
        {
            this.prefabProviders = prefabProviders;
        }

        public GameObject GetUiPrefab(Type type)
        {
            foreach (var uiPrefabProvider in prefabProviders) {
                var uiPrefab = uiPrefabProvider.GetUiPrefab(type);
                if (uiPrefab != null) {
                    return uiPrefab;
                }
            }

            return null;
        }
    }
}