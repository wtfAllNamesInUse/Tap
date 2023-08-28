using System.Collections.Generic;

namespace TapTapTap.Core
{
    public interface ILevelConverter
    {
        IEnumerable<LevelDescription> Convert(IEnumerable<string> levelsToConvert);
    }
}
