using System;
using UnityEngine;
using Zenject;

namespace TapTapTap.Core
{
    public class UiPrefabProviderFromPool : IUiPrefabProvider
    {
        private readonly DamagePopup.Pool damagePopupPool;

        public UiPrefabProviderFromPool(
            DamagePopup.Pool damagePopupPool)
        {
            this.damagePopupPool = damagePopupPool;
        }

        public GameObject GetUiPrefab(Type type)
        {
            return type == typeof(DamagePopup) ? damagePopupPool.Spawn()?.gameObject : null;
        }
    }
}