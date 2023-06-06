using System.Collections.Generic;
using UnityEngine;

namespace TapTapTap.Core
{
    [CreateAssetMenu(fileName = "EntityConfig", menuName = "ScriptableObjects/EntityConfig")]
    public class EntityConfig : ScriptableObject
    {
        [SerializeField]
        private List<EntityArchetype> archetypes;

        public EntityArchetype GetArchetype(string name)
        {
            return archetypes.Find(p => p.EntityName.Equals(name));
        }
    }
}