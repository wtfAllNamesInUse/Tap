using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace TapTapTap.Core
{
    [CreateAssetMenu(fileName = "CollectibleArchetype", menuName = "ScriptableObjects/CollectibleArchetype")]
    public class CollectibleArchetype : ScriptableObject
    {
        public string CollectibleName => collectibleName;
        public Object Prefab => prefab;

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
        private string collectibleName;

        [SerializeField]
        private Object prefab;

        [SerializeField]
        private List<AttributeInfo> attributes = new();
    }
}