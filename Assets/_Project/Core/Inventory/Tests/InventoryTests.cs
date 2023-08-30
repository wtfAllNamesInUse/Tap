using Zenject;
using NUnit.Framework;
using TapTapTap.Inventory;

[TestFixture]
public class InventoryTests : ZenjectUnitTestFixture
{
    private readonly string dummyItemName = "dummyItem";
    private readonly string dummyItemName2 = "dummyItem2";
    private readonly string dummyItemName3 = "dummyItem3";

    private IInventory inventory;

    [SetUp]
    public void CommonInstall()
    {
        InventoryInstaller.Install(Container);
        inventory = Container.Resolve<IInventory>();
    }

    private void CleanUp()
    {
        inventory.Dispose();
    }

    [Test]
    public void TestAdd()
    {
        CleanUp();
        
        inventory.Add(dummyItemName, 4);
        Assert.IsTrue(inventory.GetCount(dummyItemName) == 4);

        inventory.Add(new InventoryItemModel(dummyItemName2, 8));
        Assert.IsTrue(inventory.GetCount(dummyItemName2) == 8);

        Assert.IsTrue(inventory.GetCount(dummyItemName3) == 0);
    }
    
    [Test]
    public void TestMultipleAdd()
    {
        CleanUp();
        
        inventory.Add(dummyItemName, 4);
        Assert.IsTrue(inventory.GetCount(dummyItemName) == 4);

        inventory.Add(dummyItemName, 4);       
        Assert.IsTrue(inventory.GetCount(dummyItemName) == 8);
    }

    [Test]
    public void TestRemove()
    {
        CleanUp();
        
        inventory.Add(dummyItemName, 4);
        inventory.Remove(dummyItemName, 2);
        Assert.IsTrue(inventory.GetCount(dummyItemName) == 2);

        inventory.Remove(dummyItemName, 8);
        Assert.IsTrue(inventory.GetCount(dummyItemName) == 0);
    }
    
    [Test]
    public void TestPooling()
    {
        CleanUp();
        
        inventory.Add(dummyItemName, 4);
        Assert.IsTrue(InventoryItemModel.Pool.NumActive == 1);
        
        inventory.Add(dummyItemName, 4);
        Assert.IsTrue(InventoryItemModel.Pool.NumActive == 1);
        Assert.IsTrue(InventoryItemModel.Pool.NumInactive == 1);
        
        inventory.Remove(dummyItemName, 8);
        Assert.IsTrue(InventoryItemModel.Pool.NumActive == 0);
        Assert.IsTrue(InventoryItemModel.Pool.NumInactive == 2);
    }
}