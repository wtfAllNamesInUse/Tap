using System;
using TapTapTap.Inventory;
using Zenject;

public class InventoryInstaller : Installer<InventoryInstaller>
{
    public override void InstallBindings()
    {
        Container.Bind(typeof(IDisposable), typeof(IInventory)).To<Inventory>().AsSingle();
    }
}