using System;
using TapTapTap.Ui;
using Zenject;

namespace TapTapTap.Core
{
    public class LevelFinishHandler : IInitializable, IDisposable
    {
        private readonly SignalBus signalBus;
        private readonly ScreenController screenController;
        private readonly ITimer globalTimer;
        private readonly GameStateData gameStateData;

        public LevelFinishHandler(
            ITimersContainer timersContainer,
            SignalBus signalBus,
            ScreenController screenController,
            GameStateData gameStateData)
        {
            this.signalBus = signalBus;
            this.screenController = screenController;
            this.gameStateData = gameStateData;

            globalTimer = timersContainer.GetTimer(GameplayTimersContainer.GlobalTimer);
        }

        public void Initialize()
        {
            signalBus.Subscribe<GameStateChangedSignal>(OnGameStateChanged);
        }

        public void Dispose()
        {
            signalBus.Unsubscribe<GameStateChangedSignal>(OnGameStateChanged);
        }

        private void OnGameStateChanged(GameStateChangedSignal signal)
        {
            if (signal.NewGameState != GameState.NewGame) {
                globalTimer.Stop();
            }

            switch (signal.NewGameState) {
                case GameState.TimesUp:
                    HandleTimesUp();
                    break;

                case GameState.Defeat:
                    HandleDefeated();
                    break;

                case GameState.Finish:
                    HandleWin();
                    break;
            }
        }

        private void HandleDefeated()
        {
            screenController.ShowScreen<LevelCompletedScreen, LevelCompletedData>(
                new LevelCompletedData {
                    LevelCompletedResult = LevelCompletedResult.Defeated
                });
        }

        private void HandleTimesUp()
        {
            screenController.ShowScreen<LevelCompletedScreen, LevelCompletedData>(
                new LevelCompletedData {
                    LevelCompletedResult = LevelCompletedResult.TimesUp
                });

            gameStateData.Player.Attributes.ApplyAttributeModifier(AttributeDefinition.Health, -float.MaxValue);
        }
        
        private void HandleWin()
        {
            screenController.ShowScreen<LevelCompletedScreen, LevelCompletedData>(
                new LevelCompletedData {
                    LevelCompletedResult = LevelCompletedResult.Won
                });

            gameStateData.Player.Attributes.ApplyAttributeModifier(AttributeDefinition.Health, -float.MaxValue);
        }
    }
}