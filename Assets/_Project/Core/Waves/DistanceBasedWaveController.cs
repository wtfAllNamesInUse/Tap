using System;
using System.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace TapTapTap.Core
{
    public class DistanceBasedWaveController : IInitializable, IDisposable, IWavesController
    {
        private readonly SignalBus signalBus;
        private readonly SpawnerSystem spawnerSystem;
        private readonly PositionProvider positionProvider;
        private readonly GameStateData gameStateData;
        private readonly DistanceEvaluator distanceEvaluator;

        private int spawnedEnemiesCount;

        public DistanceBasedWaveController(
            SignalBus signalBus,
            SpawnerSystem spawnerSystem,
            PositionProvider positionProvider,
            GameStateData gameStateData,
            DistanceEvaluator distanceEvaluator)
        {
            this.signalBus = signalBus;
            this.spawnerSystem = spawnerSystem;
            this.positionProvider = positionProvider;
            this.gameStateData = gameStateData;
            this.distanceEvaluator = distanceEvaluator;
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
            var newGameState = signal.NewGameState;
            if (newGameState == GameState.NewGame)
            {
                RunController();
            }
        }

        public async void RunController()
        {
            while (true) {
                var distance = (int)distanceEvaluator.Distance;
                if (distance < 6 * spawnedEnemiesCount) {
                    await Task.Delay(500);
                    continue;
                }

                var spawnedEnemyPosition = gameStateData.Player.transform.position;
                spawnedEnemyPosition.y = 0;
                spawnedEnemyPosition.x += 6.0f;

                spawnerSystem.SpawnEntity("ENEMY", positionProvider.EnemyStart,
                    spawnedEnemyPosition);

                spawnedEnemiesCount++;

                if (!Application.isPlaying) {
                    return;
                }
            }
        }
    }
}