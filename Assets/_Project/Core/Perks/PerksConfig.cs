using System.Collections.Generic;
using UnityEngine;

namespace TapTapTap.Core
{
    [CreateAssetMenu(fileName = "PerksConfig", menuName = "ScriptableObjects/PerksConfig")]
    public class PerksConfig : ScriptableObject
    {
        public IReadOnlyList<PerkArchetype> Perks => archetypes;

        public int PerksCount => Perks.Count;

        [SerializeField]
        private List<PerkArchetype> archetypes;
    }
}