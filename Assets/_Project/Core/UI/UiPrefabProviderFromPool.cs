using System;
using UnityEngine;
using Zenject;

namespace TapTapTap.Core
{
    public class UiPrefabProviderFromPool : IUiPrefabProvider
    {
        private readonly DamagePopup.Factory damagePopupPool;

        public UiPrefabProviderFromPool(
            DamagePopup.Factory damagePopupPool)
        {
            this.damagePopupPool = damagePopupPool;
        }

        public GameObject GetUiPrefab(Type type)
        {
            return type == typeof(DamagePopup) ? damagePopupPool.Create().gameObject : null;
        }
    }
}