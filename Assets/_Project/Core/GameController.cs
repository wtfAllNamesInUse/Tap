using System;
using Zenject;

namespace TapTapTap.Core
{
    public class GameController : IInitializable, IDisposable
    {
        private readonly SignalBus signalBus;
        private readonly ISpawner spawner;
        private readonly PositionProvider positionProvider;
        private readonly CameraController cameraController;
        private readonly GameStateData gameStateData;
        private readonly ScreenController screenController;
        private readonly ITutorialsContainer tutorialsContainer;

        private Entity Player => gameStateData.Player;

        public GameController(
            SignalBus signalBus,
            ISpawner spawner,
            PositionProvider positionProvider,
            CameraController cameraController,
            GameStateData gameStateData,
            ScreenController screenController,
            ITutorialsContainer tutorialsContainer)
        {
            this.signalBus = signalBus;
            this.spawner = spawner;
            this.positionProvider = positionProvider;
            this.cameraController = cameraController;
            this.gameStateData = gameStateData;
            this.screenController = screenController;
            this.tutorialsContainer = tutorialsContainer;
        }

        public void Initialize()
        {
            signalBus.Subscribe<GameStateChangedSignal>(OnGameStateChanged);
        }

        public void Dispose()
        {
            signalBus.Unsubscribe<GameStateChangedSignal>(OnGameStateChanged);
        }

        private async void OnGameStateChanged(GameStateChangedSignal signal)
        {
            gameStateData.Player = spawner
                .Spawn<Entity>("PLAYER", positionProvider.PlayerStart);
            gameStateData.OriginalPlayerPosition = gameStateData.Player.transform.position;

            cameraController.Initialize(Player);

            await tutorialsContainer.TryShowTutorials();

            screenController.ShowScreen<StatsBarScreen>();
        }

        private void OnClick()
        {
            // if (playerInputBlocker.IsBlocked || !Player.IsAlive) {
            //     return;
            // }
            //
            // Player.TryAttack();
        }
    }
}