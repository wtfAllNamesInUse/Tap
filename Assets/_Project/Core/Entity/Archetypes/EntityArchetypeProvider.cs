namespace TapTapTap.Core
{
    public class EntityArchetypeProvider : IArchetypeProvider<EntityArchetype>
    {
        private readonly EntityConfig config;

        public EntityArchetypeProvider(EntityConfig config)
        {
            this.config = config;
        }

        public EntityArchetype GetArchetype(string name)
        {
            return config.GetArchetype(name);
        }
    }
}