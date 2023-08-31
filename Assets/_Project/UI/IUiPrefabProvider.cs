using System;
using UnityEngine;

namespace TapTapTap.Ui
{
    public interface IUiPrefabProvider
    {
        GameObject GetUiPrefab(Type type);
    }
}