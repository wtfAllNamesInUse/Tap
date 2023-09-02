using TapTapTap.Ui;
using UnityEngine;
using UnityEngine.UI;

namespace TapTapTap.Inventory.Backpack.Views
{
    public class BackpackButton : ConfigurableButton
    {
        [SerializeField]
        private Image image;

        [SerializeField]
        private Badge badge;

        public void Initialize(
            InventoryItemModel inventoryItemModel,
            InventoryItemArchetype inventoryItemArchetype)
        {
            Initialize(inventoryItemModel.ItemName, null);
            image.sprite = inventoryItemArchetype.Sprite;
            badge.SetText(inventoryItemModel.Count.ToString());
        }
    }
}