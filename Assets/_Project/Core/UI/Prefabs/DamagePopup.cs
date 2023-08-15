using System.Globalization;
using TMPro;
using UnityEngine;
using Zenject;

namespace TapTapTap.Core
{
    public class DamagePopup : ScreenWithData<DamagePopupData>
    {
        [SerializeField]
        private TextMeshProUGUI text;

        public override void OnScreenInitialized()
        {
            text.text = CustomData.Damage.ToString(CultureInfo.InvariantCulture);
        }

        public class Pool : MonoMemoryPool<DamagePopup>
        {
        }
    }
}