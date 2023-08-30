using TMPro;
using UnityEngine;
using Zenject;

namespace TapTapTap.Inventory.Views
{
    public class CoinsViewCounter : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI text;

        private IInventory inventory;

        private int lastSetValue;

        [Inject]
        public void Inject(IInventory inventory)
        {
            this.inventory = inventory;
        }

        public void Refresh()
        {
            var count = inventory.GetCount(InventoryItemIds.Coins);
            if (lastSetValue == count) {
                return;
            }
            
            lastSetValue = count;
            text.text = lastSetValue.ToString("00000");
        }
    }
}