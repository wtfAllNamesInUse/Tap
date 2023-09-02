using TMPro;
using UnityEngine;

namespace TapTapTap.Ui
{
    public class Badge : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI textField;

        public void SetText(string text)
        {
            textField.text = text;
        }
    }
}