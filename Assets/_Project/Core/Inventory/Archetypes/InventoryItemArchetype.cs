using UnityEngine;

namespace TapTapTap.Inventory
{
    [CreateAssetMenu(fileName = nameof(InventoryItemArchetype),
        menuName = "ScriptableObjects/Inventory/" + nameof(InventoryItemArchetype))]
    public class InventoryItemArchetype : ScriptableObject
    {
        public string InventoryItemName => inventoryItemName;

        public Sprite Sprite => sprite;
        
        [SerializeField]
        private string inventoryItemName;

        [SerializeField]
        private Sprite sprite;
    }
}