using System.Collections.Generic;
using UnityEngine;

namespace TapTapTap.Core
{
    public class LevelsContainer
    {
        public IReadOnlyDictionary<string, LevelDescription> LevelDictionary => levelDictionary;

        private Dictionary<string, LevelDescription> levelDictionary = new();

        public void AddLevel(LevelDescription levelDescription)
        {
            var isLevelAdded = levelDictionary.ContainsKey(levelDescription.id);
            if (isLevelAdded) {
                Debug.LogError("Level with same id is already added to container");
                return;
            }

            levelDictionary[levelDescription.id] = levelDescription;
        }

        public void AddLevels(IEnumerable<LevelDescription> levelDescriptions)
        {
            foreach (var levelDescription in levelDescriptions) {
                AddLevel(levelDescription);
            }
        }

        public LevelDescription GetLevel(string id)
        {
            if (levelDictionary.TryGetValue(id, out var levelDescription)) {
                return levelDescription;
            }

            Debug.LogError($"No level with id {id}");
            return null;
        }
    }
}