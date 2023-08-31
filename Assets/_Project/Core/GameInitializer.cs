using TapTapTap.Ui;
using UnityEngine;
using Zenject;

namespace TapTapTap.Core
{
    public class GameInitializer : IInitializable
    {
        private readonly LevelsManager levelsManager;
        private readonly ISpawner spawner;
        private readonly PositionProvider positionProvider;
        private readonly ScreenController screenController;

        public GameInitializer(
            LevelsManager levelsManager,
            ISpawner spawner,
            PositionProvider positionProvider,
            ScreenController screenController)
        {
            this.levelsManager = levelsManager;
            this.spawner = spawner;
            this.positionProvider = positionProvider;
            this.screenController = screenController;
        }

        public void Initialize()
        {
            SpawnLevelEntities();

            screenController.ShowScreen<WelcomeScreen>();
        }

        private void SpawnLevelEntities()
        {
            var levelDescription = levelsManager.GetLevel();
            var levelEntities = levelDescription.levelEntityDatas;

            var position = positionProvider.EnemyStart.position;
            foreach (var entity in levelEntities) {
                spawner.Spawn<Component>(entity.id, positionProvider.EnemyStart, position);
                position.x += levelDescription.positionScale;
            }
        }
    }
}