using System.Collections.Generic;
using UnityEngine;

namespace TapTapTap.Inventory
{
    [CreateAssetMenu(fileName = nameof(InventoryItemConfig),
        menuName = "ScriptableObjects/" + nameof(InventoryItemConfig))]
    public class InventoryItemConfig : ScriptableObject
    {
        [SerializeField]
        private List<InventoryItemArchetype> archetypes;

        public InventoryItemArchetype GetArchetype(string name)
        {
            return archetypes.Find(p => p.InventoryItemName.Equals(name));
        }
    }
}