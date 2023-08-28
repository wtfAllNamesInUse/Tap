namespace TapTapTap.Core
{
    public class CollectibleArchetypeProvider : IArchetypeProvider<CollectibleArchetype>
    {
        private readonly CollectibleConfig config;

        public CollectibleArchetypeProvider(CollectibleConfig config)
        {
            this.config = config;
        }

        public CollectibleArchetype GetArchetype(string name)
        {
            return config.GetArchetype(name);
        }
    }
}