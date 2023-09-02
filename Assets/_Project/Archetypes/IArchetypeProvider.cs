namespace TapTapTap.Archetypes
{
    public interface IArchetypeProvider<TArchetype>
    {
        TArchetype GetArchetype(string name);
    }
}
