using System.Collections.Generic;
using Unity.Plastic.Newtonsoft.Json;

namespace TapTapTap.Core
{
    public class LevelConverter : ILevelConverter
    {
        public IEnumerable<LevelDescription> Convert(IEnumerable<string> levelsToConvert)
        {
            var result = new List<LevelDescription>();
            foreach (var levelData in levelsToConvert) {
                var convertedLevel = ConvertSingleLevel(levelData);
                if (convertedLevel != null) {
                    result.Add(convertedLevel);
                }
            }

            return result;
        }

        private LevelDescription ConvertSingleLevel(string levelData)
        {
            return JsonConvert.DeserializeObject<LevelDescription>(levelData);
        }
    }
}