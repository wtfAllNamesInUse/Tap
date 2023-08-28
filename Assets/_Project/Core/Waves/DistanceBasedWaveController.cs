using System;
using System.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace TapTapTap.Core
{
    public class DistanceBasedWaveController : IInitializable, IDisposable, IWavesController
    {
        private readonly SignalBus signalBus;
        private readonly SpawnerContainer spawners;
        private readonly PositionProvider positionProvider;
        private readonly GameStateData gameStateData;
        private readonly DistanceEvaluator distanceEvaluator;

        private int spawnedEnemiesCount;

        public DistanceBasedWaveController(
            SignalBus signalBus,
            SpawnerContainer spawners,
            PositionProvider positionProvider,
            GameStateData gameStateData,
            DistanceEvaluator distanceEvaluator)
        {
            this.signalBus = signalBus;
            this.spawners = spawners;
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
            if (newGameState == GameState.NewGame) {
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

                spawners.Spawn<Entity>("ENEMY", positionProvider.EnemyStart,
                    spawnedEnemyPosition);

                spawners.Spawn<CollectibleFacade>("Coins", positionProvider.EnemyStart,
                    new Vector3(spawnedEnemyPosition.x - 2.0f, spawnedEnemyPosition.y, spawnedEnemyPosition.z));

                spawnedEnemiesCount++;

                if (!Application.isPlaying) {
                    return;
                }
            }
        }
    }
}