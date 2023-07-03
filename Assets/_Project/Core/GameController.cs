using System;
using TapTapTap.Core.FSM;
using Zenject;

namespace TapTapTap.Core
{
    public class GameController : IInitializable, IDisposable
    {
        private readonly SignalBus signalBus;
        private readonly SpawnerSystem spawnerSystem;
        private readonly PositionProvider positionProvider;
        private readonly ClickDetector clickDetector;
        private readonly EntityGatherer entityGatherer;
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
            EntityGatherer entityGatherer,
            CameraController cameraController,
            GameStateData gameStateData,
            ScreenController screenController,
            ITutorialsContainer tutorialsContainer,
            IBlocker playerInputBlocker)
        {
            this.signalBus = signalBus;
            this.spawnerSystem = spawnerSystem;
            this.positionProvider = positionProvider;
            this.clickDetector = clickDetector;
            this.entityGatherer = entityGatherer;
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
            gameStateData.Player =
                spawnerSystem.SpawnEntity("PLAYER", EntityDirection.Right, positionProvider.PlayerStart);
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

            var myFraction = Player.Data.EntityArchetype.Fraction;
            var enemy = entityGatherer.GetClosestEntityMatchingPredicate(Player.transform,
                p => p.Data.EntityArchetype.Fraction != myFraction, 1.5f);
            if (enemy != null) {
                if (Player.StateMachine.CurrentState?.StateID != EntityStates.Attack) {
                    Player.StateMachine.Blackboard.TargetEntity = enemy;
                    Player.StateMachine.ChangeState(EntityStates.Attack);
                }
                return;
            }

            if (Player.StateMachine.CurrentState?.StateID != EntityStates.Run) {
                Player.StateMachine.ChangeState(EntityStates.Run);
            }
        }
    }
}