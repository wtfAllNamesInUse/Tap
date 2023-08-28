using System.Collections.Generic;

namespace TapTapTap.Core
{
    public interface ILevelProvider
    {
        IEnumerable<LevelDescription> LoadLevels(int levelsToLoad);
        IEnumerable<LevelDescription> LoadAllLevels();
    }
}