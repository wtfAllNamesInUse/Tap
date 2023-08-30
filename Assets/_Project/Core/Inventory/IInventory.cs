using System;

namespace TapTapTap.Inventory
{
    public interface IInventory : IDisposable
    {
        event Action<InventoryItemModel> ItemModified; 

        void Add(string itemId, int count);
        void Add(InventoryItemModel itemModel);

        void Remove(string itemId, int count);
        void Remove(InventoryItemModel itemModel);
        
        int GetCount(string itemName);
        InventoryItemModel GetItem(string itemName);
    }
}