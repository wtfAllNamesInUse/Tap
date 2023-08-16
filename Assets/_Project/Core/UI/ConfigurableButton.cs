using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace TapTapTap.Core
{
    public class ConfigurableButton : MonoBehaviour
    {
        [SerializeField]
        private Button button;

        [SerializeField]
        private TextMeshProUGUI textField;

        public void Initialize(string text, Action<string> onClick)
        {
            textField.text = text;
            button.onClick.AddListener(() => onClick?.Invoke(textField.text));
        }
    }
}