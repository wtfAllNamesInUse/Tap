using TapTapTap.Archetypes;

namespace TapTapTap.Inventory
{
    public class InventoryItemArchetypeProvider : IArchetypeProvider<InventoryItemArchetype>
    {
        private readonly InventoryItemConfig config;

        public InventoryItemArchetypeProvider(InventoryItemConfig config)
        {
            this.config = config;
        }

        public InventoryItemArchetype GetArchetype(string name)
        {
            return config.GetArchetype(name);
        }
    }
}