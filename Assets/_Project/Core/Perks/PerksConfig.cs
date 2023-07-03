using System.Collections.Generic;
using UnityEngine;

namespace TapTapTap.Core
{
    [CreateAssetMenu(fileName = "PerksConfig", menuName = "ScriptableObjects/PerksConfig")]
    public class PerksConfig : ScriptableObject
    {
        [SerializeField]
        private List<PerkArchetype> archetypes;

        public PerkArchetype GetArchetype(string name)
        {
            return archetypes.Find(p => p.PerkName.Equals(name));
        }
    }
}