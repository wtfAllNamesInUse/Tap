namespace TapTapTap.Core
{
    public class ArchetypeProvider
    {
        private readonly EntityConfig entityConfig;

        public ArchetypeProvider(EntityConfig entityConfig)
        {
            this.entityConfig = entityConfig;
        }

        public EntityArchetype GetArchetype(string name)
        {
            return entityConfig.GetArchetype(name);
        }
    }
}