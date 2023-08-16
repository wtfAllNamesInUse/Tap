using System.Collections.Generic;
using UnityEngine;

namespace TapTapTap.Core
{
    public class RandomPerkProvider : IPerkProvider
    {
        private readonly PerksConfig perksConfig;

        public RandomPerkProvider(
            PerksConfig perksConfig)
        {
            this.perksConfig = perksConfig;
        }

        public IList<PerkArchetype> GetPerks(int perksCount)
        {
            return GetRandomPerks(perksCount);
        }
        
        private IList<PerkArchetype> GetRandomPerks(int count)
        {
            var result = new List<PerkArchetype>(count);

            var perks = perksConfig.Perks;
            var perksCount = perksConfig.PerksCount;
            
            var randomIndex = Random.Range(0, perksCount);
            result.Add(perks[randomIndex]);

            randomIndex = (randomIndex + 1) % perksCount;
            result.Add(perks[randomIndex]);

            return result;
        }
    }
}