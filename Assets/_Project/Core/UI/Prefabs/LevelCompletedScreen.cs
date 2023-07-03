using TMPro;
using UnityEngine;
using Zenject;

namespace TapTapTap.Core
{
    public class LevelCompletedScreen : ScreenWithData<LevelCompletedData>
    {
        [SerializeField]
        private TextMeshProUGUI header;
        [SerializeField]
        private TextMeshProUGUI score;

        private DistanceEvaluator distanceEvaluator;
        private ScreenController screenController;

        [Inject]
        public void Inject(
            DistanceEvaluator distanceEvaluator,
            ScreenController screenController)
        {
            this.distanceEvaluator = distanceEvaluator;
            this.screenController = screenController;
        }

        public override void OnScreenInitialized()
        {
            base.OnScreenInitialized();

            header.text = GetHeaderText();
            score.text = "Score: " + distanceEvaluator.Distance.ToString("0000");
        }

        private string GetHeaderText()
        {
            switch (CustomData.LevelCompletedResult) {
                case LevelCompletedResult.TimesUp:
                    return "Times Up!";

                case LevelCompletedResult.Defeated:
                    return "Defeated";

                case LevelCompletedResult.Won:
                    return "Level Won";

                default:
                    return "";
            }
        }

        public void OnRestartClicked()
        {
            screenController.ShowScreen<PerkChooserScreen>();
            Close();
        }
    }
}