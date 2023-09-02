using System.Collections.Generic;
using TapTapTap.Archetypes;
using UnityEngine;
using Zenject;
using Screen = TapTapTap.Ui.Screen;

namespace TapTapTap.Inventory.Backpack.Views
{
    public class BackpackScreen : Screen
    {
        private IInventory inventory;
        private IArchetypeProvider<InventoryItemArchetype> inventoryItemArchetypeProvider;

        [SerializeField]
        private List<BackpackButton> buttons;

        [Inject]
        public void Inject(
            IInventory inventory,
            IArchetypeProvider<InventoryItemArchetype> inventoryItemArchetypeProvider)
        {
            this.inventory = inventory;
            this.inventoryItemArchetypeProvider = inventoryItemArchetypeProvider;
        }

        public override void OnScreenInitialized()
        {
            inventory.ItemModified += OnItemModified;
            InitializeButtons();
        }

        private void OnItemModified(InventoryItemModel _)
        {
            InitializeButtons();
        }

        private void InitializeButtons()
        {
            var buttonIndex = 0;
            foreach (var inventoryItemModel in inventory) {
                var archetype = inventoryItemArchetypeProvider.GetArchetype(inventoryItemModel.ItemName);
                buttons[buttonIndex++].Initialize(inventoryItemModel, archetype);
            }
        }

        public override void OnDestroy()
        {
            inventory.ItemModified -= OnItemModified;
        }
    }
}