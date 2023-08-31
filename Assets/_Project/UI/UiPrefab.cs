using System;
using UnityEngine;

namespace TapTapTap.Ui
{
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
