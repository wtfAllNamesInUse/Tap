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
        private readonly ClickDetector tapDetector;
        private readonly EntityGatherer entityGatherer;
        private readonly CameraController cameraController;
        private readonly GameStateData gameStateData;

        private Entity Player => gameStateData.Player;

        public GameController(
            SignalBus signalBus,
            SpawnerSystem spawnerSystem,
            PositionProvider positionProvider,
            ClickDetector tapDetector,
            EntityGatherer entityGatherer,
            CameraController cameraController,
            GameStateData gameStateData)
        {
            this.signalBus = signalBus;
            this.spawnerSystem = spawnerSystem;
            this.positionProvider = positionProvider;
            this.tapDetector = tapDetector;
            this.entityGatherer = entityGatherer;
            this.cameraController = cameraController;
            this.gameStateData = gameStateData;
        }

        public void Initialize()
        {
            gameStateData.Player =
                spawnerSystem.SpawnEntity("PLAYER", EntityDirection.Right, positionProvider.PlayerStart);
            cameraController.Initialize(Player);

            signalBus.Fire(new GameStateChangedSignal() { NewGameState = GameState.NewGame });
            tapDetector.OnTap += OnTap;
        }

        public void Dispose()
        {
            tapDetector.OnTap -= OnTap;
        }

        private void OnTap()
        {
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