using TapTapTap.Ui;
using TMPro;
using UnityEngine;

namespace TapTapTap.Core
{
    public class SimplePopupScreen : ScreenWithData<SimplePopupScreenData>
    {
        [SerializeField]
        private TextMeshProUGUI text;

        [SerializeField]
        private SimpleButton acceptButton;

        public override void OnScreenInitialized()
        {
            text.text = CustomData.Text;

            var isAcceptButtonConfigured = !string.IsNullOrEmpty(CustomData.AcceptButtonText);
            if (!isAcceptButtonConfigured) {
                return;
            }
            
            acceptButton.gameObject.SetActive(true);
            acceptButton.AddListener(CustomData.AcceptButtonClicked);
            acceptButton.Text = CustomData.AcceptButtonText;
        }
    }
}