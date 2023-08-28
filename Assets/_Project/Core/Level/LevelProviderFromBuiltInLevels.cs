using System.Collections.Generic;
using System.Linq;

namespace TapTapTap.Core
{
    public class LevelProviderFromBuiltInLevels : ILevelProvider
    {
        private readonly BuiltInLevelsContainer builtInLevelsContainer;
        private readonly ILevelConverter levelConverter;

        public LevelProviderFromBuiltInLevels(
            BuiltInLevelsContainer builtInLevelsContainer,
            ILevelConverter levelConverter)
        {
            this.builtInLevelsContainer = builtInLevelsContainer;
            this.levelConverter = levelConverter;
        }

        public IEnumerable<LevelDescription> LoadLevels(int levelsToLoad)
        {
            var levels = builtInLevelsContainer.BuiltInLevels.Take(levelsToLoad).Select(p => p.text);
            return levelConverter.Convert(levels);
        }

        public IEnumerable<LevelDescription> LoadAllLevels()
        {
            return LoadLevels(builtInLevelsContainer.BuiltInLevels.Count);
        }
    }
}