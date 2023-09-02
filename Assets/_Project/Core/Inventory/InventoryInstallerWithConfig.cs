using UnityEngine;
using Zenject;

namespace TapTapTap.Inventory
{
    public class InventoryInstallerWithConfig : MonoInstaller
    {
        [SerializeField]
        private InventoryItemConfig inventoryItemConfig;
        
        public override void InstallBindings()
        {
            InventoryInstaller.Install(Container);

            Container.BindInstance(inventoryItemConfig);
        }
    }
}