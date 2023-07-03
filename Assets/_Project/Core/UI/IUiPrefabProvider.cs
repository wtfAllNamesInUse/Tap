using System;
using UnityEngine;

namespace TapTapTap.Core
{
    public interface IUiPrefabProvider
    {
        GameObject GetUiPrefab(Type type);
    }
}