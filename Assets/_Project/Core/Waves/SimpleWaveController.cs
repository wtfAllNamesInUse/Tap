using System;
using System.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace TapTapTap.Core
{
    public class SimpleWaveController : IInitializable, IDisposable, IWavesController
    {
        private const int SpawnTimeMs = 5000;

        private readonly SignalBus signalBus;
        private readonly SpawnerSystem spawnerSystem;
        private readonly PositionProvider positionProvider;
        private readonly GameStateData gameStateData;

        public SimpleWaveController(
            SignalBus signalBus,
            SpawnerSystem spawnerSystem,
            PositionProvider positionProvider,
            GameStateData gameStateData)
        {
            this.signalBus = signalBus;
            this.spawnerSystem = spawnerSystem;
            this.positionProvider = positionProvider;
            this.gameStateData = gameStateData;
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
                var spawnedEnemyPosition = gameStateData.Player.transform.position;
                spawnedEnemyPosition.y = 0;
                spawnedEnemyPosition.x += 5.0f;

                spawnerSystem.SpawnEntity("ENEMY", positionProvider.EnemyStart,
                    spawnedEnemyPosition);
                await Task.Delay(SpawnTimeMs);

                if (!Application.isPlaying) {
                    return;
                }
            }
        }
    }
}