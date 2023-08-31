using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace TapTapTap.Core
{
    public class SimpleButton : MonoBehaviour
    {
        [SerializeField]
        private Button button;

        [SerializeField]
        private TextMeshProUGUI text;

        public string Text {
            get => text.text;
            set => text.text = value;
        }

        public void AddListener(Action action)
        {
            button.onClick.AddListener(() => action?.Invoke());
        }

        private void OnDestroy()
        {
            button.onClick.RemoveAllListeners();
        }
    }
}