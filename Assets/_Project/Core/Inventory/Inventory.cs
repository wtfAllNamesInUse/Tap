using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace TapTapTap.Inventory
{
    public class Inventory : IInventory
    {
        public event Action<InventoryItemModel> ItemModified;

        private readonly List<InventoryItemModel> itemModels = new();

        public void Add(string itemId, int count)
        {
            var itemModel = InventoryItemModel.Pool.Spawn(itemId, count);
            InternalAdd(itemModel);
        }

        public void Add(InventoryItemModel itemModel)
        {
            InternalAdd(itemModel);
        }

        public void Remove(string itemId, int count)
        {
            using var itemModel = InventoryItemModel.Pool.Spawn(itemId, count);
            InternalRemove(itemModel);
        }

        public void Remove(InventoryItemModel itemModel)
        {
            InternalRemove(itemModel);
        }

        public int GetCount(string itemName)
        {
            var itemModel = GetItem(itemName);
            return itemModel?.Count ?? 0;
        }

        public InventoryItemModel GetItem(string itemName)
        {
            return itemModels.FirstOrDefault(itemModel => itemModel.ItemName.Equals(itemName));
        }

        public void Dispose()
        {
            itemModels.Clear();
            InventoryItemModel.Pool.Clear();
            InventoryItemModel.Pool.ClearActiveCount();
        }
        
        private void InternalAdd(InventoryItemModel itemModel)
        {
            var itemModelToAdd = GetItem(itemModel.ItemName);
            if (itemModelToAdd == null) {
                itemModels.Add(itemModel);
            }
            else {
                itemModelToAdd.Count += itemModel.Count;
                itemModel.Dispose();
            }
            
            ItemModified?.Invoke(itemModelToAdd);
        }

        private void InternalRemove(InventoryItemModel itemModel)
        {
            var itemModelToModify = GetItem(itemModel.ItemName);
            if (itemModelToModify == null) {
                return;
            }

            itemModelToModify.Count -= itemModel.Count;

            if (itemModelToModify.Count != 0) {
                return;
            }

            itemModels.Remove(itemModelToModify);
            ItemModified?.Invoke(itemModelToModify);
            itemModelToModify.Dispose();
        }

        public IEnumerator<InventoryItemModel> GetEnumerator()
        {
            return itemModels.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}