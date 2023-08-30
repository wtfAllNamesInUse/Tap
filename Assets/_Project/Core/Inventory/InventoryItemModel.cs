using System;
using UnityEngine;
using Zenject;

namespace TapTapTap.Inventory
{
    [Serializable]
    public class InventoryItemModel : IDisposable
    {
        public string ItemName {
            get => itemName;
            internal set => itemName = value;
        }

        public int Count {
            get => count;
            internal set => count = Mathf.Clamp(value, 0, int.MaxValue);
        }

        [SerializeField]
        private string itemName;
        [SerializeField]
        private int count;

        public static readonly StaticMemoryPool<string, int, InventoryItemModel> Pool =
            new StaticMemoryPool<string, int, InventoryItemModel>(OnSpawned);

        public InventoryItemModel()
        {
        }

        public InventoryItemModel(string itemName, int count)
        {
            this.itemName = itemName;
            this.count = count;
        }

        private static void OnSpawned(string itemName, int count, InventoryItemModel itemModel)
        {
            itemModel.ItemName = itemName;
            itemModel.Count = count;
        }

        public void Dispose()
        {
            Pool.Despawn(this);
        }
    }
}