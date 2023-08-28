using Zenject;

namespace TapTapTap.Core
{
    public class LevelsManager : IInitializable
    {
        private readonly LevelsContainer levelsContainer;
        private readonly ILevelProvider[] levelProviders;

        public LevelsManager(
            LevelsContainer levelsContainer,
            ILevelProvider[] levelProviders)
        {
            this.levelsContainer = levelsContainer;
            this.levelProviders = levelProviders;
        }

        public void Initialize()
        {
            foreach (var levelProvider in levelProviders) {
                levelsContainer.AddLevels(levelProvider.LoadAllLevels());
            }
        }

        public LevelDescription GetLevel()
        {
            return levelsContainer.GetLevel("Test");
        }
    }
}