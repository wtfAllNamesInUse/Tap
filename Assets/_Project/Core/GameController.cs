using System;
using Zenject;

namespace TapTapTap.Core
{
    public class GameController : IInitializable, IDisposable
    {
        private readonly SpawnerSystem spawnerSystem;
        private readonly PositionProvider positionProvider;
        private readonly ClickDetector clickDetector;
        private readonly CameraController cameraController;
        private readonly GameStateData gameStateData;
        private readonly ScreenController screenController;
        private readonly ITutorialsContainer tutorialsContainer;
        private readonly IBlocker playerInputBlocker;

        private Entity Player => gameStateData.Player;

        public GameController(
            SignalBus signalBus,
            SpawnerSystem spawnerSystem,
            PositionProvider positionProvider,
            ClickDetector clickDetector,
            CameraController cameraController,
            GameStateData gameStateData,
            ScreenController screenController,
            ITutorialsContainer tutorialsContainer,
            IBlocker playerInputBlocker)
        {
            this.spawnerSystem = spawnerSystem;
            this.positionProvider = positionProvider;
            this.clickDetector = clickDetector;
            this.cameraController = cameraController;
            this.gameStateData = gameStateData;
            this.screenController = screenController;
            this.tutorialsContainer = tutorialsContainer;
            this.playerInputBlocker = playerInputBlocker;

            signalBus.Subscribe<GameStateChangedSignal>(OnGameStateChanged);
        }

        public void Initialize()
        {
            screenController.ShowScreen<WelcomeScreen>();
        }

        public void Dispose()
        {
            clickDetector.OnClick -= OnClick;
        }

        private async void OnGameStateChanged(GameStateChangedSignal signal)
        {
            gameStateData.Player = spawnerSystem
                .SpawnEntity("PLAYER", positionProvider.PlayerStart);
            gameStateData.OriginalPlayerPosition = gameStateData.Player.transform.position;

            cameraController.Initialize(Player);

            await tutorialsContainer.TryShowTutorials();

            clickDetector.OnClick += OnClick;

            screenController.ShowScreen<StatsBarScreen>();
        }

        private void OnClick()
        {
            if (playerInputBlocker.IsBlocked || !Player.IsAlive) {
                return;
            }

            Player.TryAttack();
        }
    }
}