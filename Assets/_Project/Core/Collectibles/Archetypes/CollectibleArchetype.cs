using System.Collections.Generic;
using TapTapTap.Inventory;
using UnityEngine;
using Object = UnityEngine.Object;

namespace TapTapTap.Core
{
    [CreateAssetMenu(fileName = "CollectibleArchetype", menuName = "ScriptableObjects/CollectibleArchetype")]
    public class CollectibleArchetype : ScriptableObject
    {
        public string CollectibleName => collectibleName;
        public Object Prefab => prefab;
        public bool IsResolvingRequired => isResolvingRequired;

        public List<AttributeInfo> Attributes {
            get {
                var result = new List<AttributeInfo>(attributes.Count);
                foreach (var attribute in attributes) {
                    result.Add(attribute.DeepCopy());
                }

                return result;
            }
        }

        public List<InventoryItemModel> Items => items;

        [SerializeField]
        private string collectibleName;

        [SerializeField]
        private Object prefab;

        [SerializeField]
        private bool isResolvingRequired;
        
        [SerializeField]
        private List<AttributeInfo> attributes = new();
        
        [SerializeField]
        private List<InventoryItemModel> items = new();
    }
}