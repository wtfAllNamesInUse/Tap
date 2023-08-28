using System.Collections.Generic;
using UnityEngine;

namespace TapTapTap.Core
{
    [CreateAssetMenu(fileName = "CollectibleConfig", menuName = "ScriptableObjects/CollectibleConfig")]
    public class CollectibleConfig : ScriptableObject
    {
        [SerializeField]
        private List<CollectibleArchetype> archetypes;

        public CollectibleArchetype GetArchetype(string name)
        {
            return archetypes.Find(p => p.CollectibleName.Equals(name));
        }
    }
}