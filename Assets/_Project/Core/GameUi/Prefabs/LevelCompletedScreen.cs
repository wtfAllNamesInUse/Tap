using TapTapTap.Ui;
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
            return CustomData.LevelCompletedResult switch {
                LevelCompletedResult.TimesUp => "Times Up!",
                LevelCompletedResult.Defeated => "Defeated",
                LevelCompletedResult.Won => "Level Won",
                _ => ""
            };
        }

        public void OnRestartClicked()
        {
            screenController.ShowScreen<PerkChooserScreen>();
            Close();
        }
    }
}