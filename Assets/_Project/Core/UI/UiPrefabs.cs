using System;
using System.Collections.Generic;
using UnityEngine;

namespace TapTapTap.Core
{
    [CreateAssetMenu(fileName = "UiPrefabs", menuName = "ScriptableObjects/UiPrefabs")]
    public class UiPrefabs : ScriptableObject
    {
        [SerializeField]
        private List<UiPrefab> uiPrefabs;

        public GameObject GetUiPrefab(Type type)
        {
            return uiPrefabs.Find(p => p.GameObject.GetComponent(type))?.GameObject;
        }
    }

    [Serializable]
    public class UiPrefab
    {
        public string Name => name;
        public GameObject GameObject => gameObject;

        [SerializeField]
        private string name;

        [SerializeField]
        private GameObject gameObject;
    }
}