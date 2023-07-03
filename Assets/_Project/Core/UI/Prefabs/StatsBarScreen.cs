using System;
using TMPro;
using UnityEngine;
using Zenject;

namespace TapTapTap.Core
{
    public class StatsBarScreen : Screen
    {
        [SerializeField]
        private TextMeshProUGUI timeLeftS;
        [SerializeField]
        private TextMeshProUGUI distance;
        [SerializeField]
        private SpeedBar speedBar;

        private ITimer globalTimer;
        private DistanceEvaluator distanceEvaluator;
        private GameplaySettings gameplaySettings;

        [Inject]
        public void Inject(
            GameplayTimersContainer gameplayTimersContainer,
            DistanceEvaluator distanceEvaluator,
            GameplaySettings gameplaySettings,
            GameStateData gameStateData)
        {
            this.distanceEvaluator = distanceEvaluator;
            this.gameplaySettings = gameplaySettings;

            globalTimer = gameplayTimersContainer.GetTimer(GameplayTimersContainer.GlobalTimer);
            speedBar.InitWithEntity(gameStateData.Player);
        }

        private void Update()
        {
            if (!globalTimer.IsRunning) {
                return;
            }

            timeLeftS.text =
                (TimeSpan.FromSeconds(gameplaySettings.LevelTimeS) - globalTimer.ElapsedTime).ToString(@"ss\:ff");
            distance.text = distanceEvaluator.Distance.ToString("0000");
        }
    }
}