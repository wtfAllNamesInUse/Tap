using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace TapTapTap.Core
{
    [CreateAssetMenu(fileName = "EntityArchetype", menuName = "ScriptableObjects/EntityArchetype")]
    public class EntityArchetype : ScriptableObject
    {
        public string EntityName => entityName;
        public Object Prefab => prefab;
        public EntityFraction Fraction => fraction;

        public List<AttributeInfo> Attributes {
            get {
                var result = new List<AttributeInfo>(attributes.Count);
                foreach (var attribute in attributes) {
                    result.Add(attribute.DeepCopy());
                }

                return result;
            }
        }

        [SerializeField]
        private string entityName;

        [SerializeField]
        private Object prefab;

        [SerializeField]
        private EntityFraction fraction;

        [SerializeField]
        private List<AttributeInfo> attributes = new();
    }
}