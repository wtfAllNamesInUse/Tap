using System;
using TapTapTap.Archetypes;
using Zenject;

namespace TapTapTap.Inventory
{
    public class InventoryInstaller : Installer<InventoryInstaller>
    {
        public override void InstallBindings()
        {
            Container.Bind(typeof(IDisposable), typeof(IInventory)).To<Inventory>().AsSingle();
            Container.Bind<IArchetypeProvider<InventoryItemArchetype>>().To<InventoryItemArchetypeProvider>().AsSingle();
        }
    }
}