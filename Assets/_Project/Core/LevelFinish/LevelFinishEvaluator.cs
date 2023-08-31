using TapTapTap.Ui;
using Zenject;

namespace TapTapTap.Core
{
    public class LevelFinishEvaluator : ITickable
    {
        private readonly ITimer globalTimer;
        private readonly GameplaySettings gameplaySettings;
        private readonly ScreenController screenController;
        private readonly GameStateData gameStateData;

        public LevelFinishEvaluator(
            ITimersContainer timersContainer,
            GameplaySettings gameplaySettings,
            ScreenController screenController,
            GameStateData gameStateData)
        {
            this.gameplaySettings = gameplaySettings;
            this.screenController = screenController;
            this.gameStateData = gameStateData;

            globalTimer = timersContainer.GetTimer(GameplayTimersContainer.GlobalTimer);
        }

        public void Tick()
        {
            var player = gameStateData.Player;
            if (!globalTimer.IsRunning || player == null) {
                return;
            }

            if (globalTimer.ElapsedSeconds >= gameplaySettings.LevelTimeS) {
                screenController.ShowScreen<LevelCompletedScreen, LevelCompletedData>(
                    new LevelCompletedData {
                        LevelCompletedResult = LevelCompletedResult.TimesUp
                    });
                globalTimer.Stop();

                player.Attributes.ApplyAttributeModifier(AttributeDefinition.Health, -float.MaxValue);
            }
            else if (!player.IsAlive) {
                screenController.ShowScreen<LevelCompletedScreen, LevelCompletedData>(
                    new LevelCompletedData {
                        LevelCompletedResult = LevelCompletedResult.Defeated
                    });
                globalTimer.Stop();
            }
        }
    }
}