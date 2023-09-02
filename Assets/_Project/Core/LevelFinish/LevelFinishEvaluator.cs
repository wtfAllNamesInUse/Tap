using Zenject;

namespace TapTapTap.Core
{
    public class LevelFinishEvaluator : ITickable
    {
        private readonly ITimer globalTimer;
        private readonly GameplaySettings gameplaySettings;
        private readonly GameStateData gameStateData;
        private readonly SignalBus signalBus;

        public LevelFinishEvaluator(
            ITimersContainer timersContainer,
            GameplaySettings gameplaySettings,
            GameStateData gameStateData,
            SignalBus signalBus)
        {
            this.gameplaySettings = gameplaySettings;
            this.gameStateData = gameStateData;
            this.signalBus = signalBus;

            globalTimer = timersContainer.GetTimer(GameplayTimersContainer.GlobalTimer);
        }

        public void Tick()
        {
            var player = gameStateData.Player;
            if (!globalTimer.IsRunning || player == null) {
                return;
            }

            if (globalTimer.ElapsedSeconds >= gameplaySettings.LevelTimeS) {
                signalBus.Fire(new GameStateChangedSignal {
                    NewGameState = GameState.TimesUp
                });
            }
            else if (!player.IsAlive) {
                signalBus.Fire(new GameStateChangedSignal {
                    NewGameState = GameState.Defeat
                });
            }
        }
    }
}